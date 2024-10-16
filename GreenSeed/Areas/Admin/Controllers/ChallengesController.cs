using GreenSeed.Models;
using GreenSeed.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using GreenSeed.ViewModels;

namespace GreenSeed.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ChallengesController : Controller
    {
        private readonly IRepository<Challenge> _challengeRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ChallengesController> _logger;

        public ChallengesController(
            IRepository<Challenge> challengeRepository,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment,
            ApplicationDbContext context,
            ILogger<ChallengesController> logger)
        {
            _challengeRepository = challengeRepository;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _logger = logger;
        }

        // GET: Admin/Challenges
        public async Task<IActionResult> Index()
        {
            var challenges = await _challengeRepository.GetAllAsync(new QueryOptions<Challenge>
            {
                Where = c => !c.IsArchived,
                OrderBy = c => c.CreatedAt,
                OrderByDirection = "DESC"
            });

            return View(challenges);
        }

        // GET: Admin/Challenges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Challenges/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateChallengeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var challenge = new Challenge
                {
                    Option1 = model.Option1,
                    Option2 = model.Option2,
                    Option3 = model.Option3,
                    Option4 = model.Option4,
                    CorrectOption = model.CorrectOption
                    // ImagePath será definido após o upload
                };

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    try
                    {
                        var extension = Path.GetExtension(model.ImageFile.FileName).ToLower();
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        if (!allowedExtensions.Contains(extension))
                        {
                            ModelState.AddModelError("ImageFile", "Formato de imagem inválido.");
                            return View(model);
                        }

                        var fileName = Path.GetRandomFileName() + extension;
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "desafios");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }

                        challenge.ImagePath = Path.Combine("images", "desafios", fileName).Replace("\\", "/");

                        _logger.LogInformation("Imagem salva em: {ImagePath}", challenge.ImagePath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao salvar a imagem.");
                        ModelState.AddModelError("", "Ocorreu um erro ao salvar a imagem.");
                        return View(model);
                    }
                }

                await _challengeRepository.AddAsync(challenge);
                TempData["SuccessMessage"] = "Desafio criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Admin/Challenges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var challenge = await _challengeRepository.GetByIdAsync(id.Value);
            if (challenge == null)
                return NotFound();

            var viewModel = new EditChallengeViewModel
            {
                Id = challenge.Id,
                Option1 = challenge.Option1,
                Option2 = challenge.Option2,
                Option3 = challenge.Option3,
                Option4 = challenge.Option4,
                CorrectOption = challenge.CorrectOption,
                ExistingImagePath = challenge.ImagePath
            };

            return View(viewModel);
        }

        // POST: Admin/Challenges/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditChallengeViewModel model)
        {
            _logger.LogInformation("Iniciando a edição do desafio com ID {ChallengeId}.", id);

            if (id != model.Id)
            {
                _logger.LogWarning("ID do desafio não corresponde ao modelo. ID do parâmetro: {ParamId}, ID do modelo: {ModelId}.", id, model.Id);
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingChallenge = await _challengeRepository.GetByIdAsync(id);
                if (existingChallenge == null)
                {
                    _logger.LogWarning("Desafio com ID {ChallengeId} não encontrado.", id);
                    return NotFound();
                }

                // Atualizar as opções e a opção correta
                existingChallenge.Option1 = model.Option1;
                existingChallenge.Option2 = model.Option2;
                existingChallenge.Option3 = model.Option3;
                existingChallenge.Option4 = model.Option4;
                existingChallenge.CorrectOption = model.CorrectOption;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    _logger.LogInformation("Processando nova imagem para o desafio com ID {ChallengeId}.", id);
                    // Validar o tipo e tamanho do arquivo conforme necessário
                    var extension = Path.GetExtension(model.ImageFile.FileName).ToLower();
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("ImageFile", "Formato de imagem inválido.");
                        return View(model);
                    }

                    // Gerar um nome de arquivo único
                    var fileName = Path.GetRandomFileName() + extension;
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "desafios"); // Atualizado para 'images/desafios'
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }

                        // Deletar a imagem antiga do servidor, se existir
                        if (!string.IsNullOrEmpty(existingChallenge.ImagePath))
                        {
                            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingChallenge.ImagePath);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Atualizar o caminho da nova imagem
                        existingChallenge.ImagePath = Path.Combine("images", "desafios", fileName).Replace("\\", "/");
                        _logger.LogInformation("Nova imagem salva em: {ImagePath}", existingChallenge.ImagePath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao salvar a nova imagem para o desafio com ID {ChallengeId}.", id);
                        ModelState.AddModelError("", "Ocorreu um erro ao salvar a nova imagem.");
                        return View(model);
                    }
                }

                try
                {
                    await _challengeRepository.UpdateAsync(existingChallenge);
                    _logger.LogInformation("Desafio com ID {ChallengeId} atualizado com sucesso.", id);
                    TempData["SuccessMessage"] = "Desafio atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar o desafio com ID {ChallengeId}.", id);
                    ModelState.AddModelError("", "Ocorreu um erro ao atualizar o desafio.");
                }
            }
            else
            {
                _logger.LogWarning("ModelState inválido ao tentar editar o desafio com ID {ChallengeId}.", id);
                // Log ModelState errors
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        _logger.LogError("Erro no campo {Field}: {ErrorMessage}", state.Key, error.ErrorMessage);
                    }
                }
            }

            return View(model);
        }


        // GET: Admin/Challenges/Archive/5
        public async Task<IActionResult> Archive(int? id)
        {
            if (id == null)
                return NotFound();

            var challenge = await _challengeRepository.GetByIdAsync(id.Value);
            if (challenge == null)
                return NotFound();

            // Arquivar o desafio
            challenge.IsArchived = true;
            await _challengeRepository.UpdateAsync(challenge);
            TempData["SuccessMessage"] = "Desafio arquivado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Challenges/ArchivedChallenges
        public async Task<IActionResult> ArchivedChallenges()
        {
            var challenges = await _challengeRepository.GetAllAsync(new QueryOptions<Challenge>
            {
                Where = c => c.IsArchived,
                OrderBy = c => c.CreatedAt,
                OrderByDirection = "DESC"
            });

            return View(challenges);
        }

        // GET: Admin/Challenges/Ranking
        public async Task<IActionResult> Ranking()
        {
            // Calcular a pontuação total por usuário
            var ranking = await _context.ChallengeResponses
                .Where(cr => cr.IsCorrect)
                .GroupBy(cr => cr.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    TotalPoints = g.Sum(cr => cr.PointsAwarded)
                })
                .OrderByDescending(x => x.TotalPoints)
                .ToListAsync();

            var userRanking = ranking.Select(x => new UserRankingViewModel
            {
                UserId = x.UserId,
                UserEmail = _context.Users.FirstOrDefault(u => u.Id == x.UserId)?.Email,
                TotalPoints = x.TotalPoints
            });

            return View(userRanking);
        }
    }
}
