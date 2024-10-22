using GreenSeed.Models;
using GreenSeed.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GreenSeed.Controllers
{
    [Authorize]
    public class CommunityPhotoUploadController : Controller
    {
        private readonly IRepository<CommunityPhotoUpload> _photoUploadRepository;
        private readonly IRepository<CommunityPhotoComment> _photoCommentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BlobContainerClient _blobContainerClient;

        public CommunityPhotoUploadController(
            IRepository<CommunityPhotoUpload> photoUploadRepository,
            IRepository<CommunityPhotoComment> photoCommentRepository,
            UserManager<ApplicationUser> userManager,
            BlobContainerClient blobContainerClient) // Injeção de dependência
        {
            _photoUploadRepository = photoUploadRepository;
            _photoCommentRepository = photoCommentRepository;
            _userManager = userManager;
            _blobContainerClient = blobContainerClient;
        }

        // GET: CommunityPhotoUpload
        public async Task<IActionResult> Index()
        {
            var options = new QueryOptions<CommunityPhotoUpload>
            {
                Includes = "User,Comments,Comments.User",
                OrderBy = u => u.UploadDate,
                OrderByDirection = "DESC"
            };

            var uploads = await _photoUploadRepository.GetAllAsync(options);

            return View(uploads);
        }

        // POST: CommunityPhotoUpload/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommunityPhotoUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                // Processar upload da foto para o Azure Blob Storage
                string photoUrl = await UploadPhotoAsync(model.Photo);

                var upload = new CommunityPhotoUpload
                {
                    UserId = user.Id,
                    Description = model.Description,
                    PhotoUrl = photoUrl,
                    UploadDate = DateTime.Now
                };

                await _photoUploadRepository.AddAsync(upload);

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // Método para processar o upload da foto para o Azure Blob Storage
        private async Task<string> UploadPhotoAsync(IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                // Gerar um nome único para o blob
                string blobName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

                // Obter referência ao blob
                BlobClient blobClient = _blobContainerClient.GetBlobClient(blobName);

                // Upload do arquivo
                using (var stream = photo.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = photo.ContentType });
                }

                // Retornar a URL do blob
                return blobClient.Uri.ToString();
            }
            return null;
        }

        // Método para apagar a foto do Azure Blob Storage
        private async Task DeletePhotoFromAzureAsync(string photoUrl)
        {
            if (!string.IsNullOrEmpty(photoUrl))
            {
                // Extrair o nome do blob a partir da URL
                var uri = new Uri(photoUrl);
                string blobName = Path.GetFileName(uri.LocalPath);

                BlobClient blobClient = _blobContainerClient.GetBlobClient(blobName);

                // Apagar o blob
                await blobClient.DeleteIfExistsAsync();
            }
        }

        // POST: CommunityPhotoUpload/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            // Obter a publicação
            var options = new QueryOptions<CommunityPhotoUpload>
            {
                Where = u => u.CommunityPhotoUploadId == id,
                Includes = "Comments"
            };

            var upload = (await _photoUploadRepository.GetAllAsync(options)).FirstOrDefault();

            if (upload == null)
            {
                return NotFound();
            }

            // Verificar se o utilizador é o dono da publicação
            if (upload.UserId != user.Id)
            {
                return Forbid();
            }

            // Apagar os comentários associados
            foreach (var comment in upload.Comments)
            {
                await _photoCommentRepository.DeleteAsync(comment.CommunityPhotoCommentId);
            }

            // Apagar a publicação
            await _photoUploadRepository.DeleteAsync(upload.CommunityPhotoUploadId);

            // Apagar a foto do Azure Blob Storage
            await DeletePhotoFromAzureAsync(upload.PhotoUrl);

            return RedirectToAction(nameof(Index));
        }

        // POST: CommunityPhotoUpload/AddComment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int id, string commentText)
        {
            if (!string.IsNullOrEmpty(commentText))
            {
                var user = await _userManager.GetUserAsync(User);

                var comment = new CommunityPhotoComment
                {
                    CommunityPhotoUploadId = id,
                    UserId = user.Id,
                    CommentText = commentText,
                    CommentDate = DateTime.Now
                };

                await _photoCommentRepository.AddAsync(comment);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: CommunityPhotoUpload/DeleteComment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            // Obter o comentário
            var options = new QueryOptions<CommunityPhotoComment>
            {
                Where = c => c.CommunityPhotoCommentId == id,
                Includes = "CommunityPhotoUpload"
            };

            var comment = (await _photoCommentRepository.GetAllAsync(options)).FirstOrDefault();

            if (comment == null)
            {
                return NotFound();
            }

            // Verificar se o utilizador é o dono do comentário ou dono da publicação
            if (comment.UserId != user.Id && comment.CommunityPhotoUpload.UserId != user.Id)
            {
                return Forbid();
            }

            // Apagar o comentário
            await _photoCommentRepository.DeleteAsync(comment.CommunityPhotoCommentId);

            return RedirectToAction(nameof(Index));
        }
    }
}
