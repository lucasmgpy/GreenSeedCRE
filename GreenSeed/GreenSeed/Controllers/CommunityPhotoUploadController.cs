using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GreenSeed.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using GreenSeed.ViewModels;

namespace GreenSeed.Controllers
{
    [Authorize]
    public class CommunityPhotoUploadController : Controller
    {
        private readonly IRepository<CommunityPhotoUpload> _photoUploadRepository;
        private readonly IRepository<CommunityPhotoComment> _photoCommentRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommunityPhotoUploadController(
            IRepository<CommunityPhotoUpload> photoUploadRepository,
            IRepository<CommunityPhotoComment> photoCommentRepository,
            UserManager<ApplicationUser> userManager)
        {
            _photoUploadRepository = photoUploadRepository;
            _photoCommentRepository = photoCommentRepository;
            _userManager = userManager;
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

                // Processar upload da foto
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

        // Método para processar o upload da foto
        private async Task<string> UploadPhotoAsync(IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                // Retornar o caminho relativo para ser usado na aplicação
                return "/uploads/" + fileName;
            }
            return null;
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

            // Opcional: Apagar a foto do servidor
            DeletePhotoFromServer(upload.PhotoUrl);

            return RedirectToAction(nameof(Index));
        }

        // Método para apagar a foto do servidor
        private void DeletePhotoFromServer(string photoUrl)
        {
            if (!string.IsNullOrEmpty(photoUrl))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoUrl.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

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
