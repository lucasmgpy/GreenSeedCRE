using GreenSeed.Models;
using GreenSeed.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.EntityFrameworkCore;

namespace GreenSeed.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class ChallengesController : Controller
    {
        private readonly IRepository<Challenge> _challengeRepository;
        private readonly IRepository<ChallengeResponse> _challengeResponseRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ChallengesController(
            IRepository<Challenge> challengeRepository,
            IRepository<ChallengeResponse> challengeResponseRepository,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _challengeRepository = challengeRepository;
            _challengeResponseRepository = challengeResponseRepository;
            _userManager = userManager;
            _context = context;
        }

        // GET: Challenges
        public async Task<IActionResult> Index()
        {
            // Obter o desafio ativo mais recente (desafios mais antigos ficam para baixo, mais recentes ficam em cima)
            var currentChallenge = (await _challengeRepository.GetAllAsync(new QueryOptions<Challenge>
            {
                Where = c => !c.IsArchived,
                OrderBy = c => c.CreatedAt,
                OrderByDirection = "DESC"
            })).FirstOrDefault();

            if (currentChallenge == null)
            {
                ViewBag.Message = "Nenhum desafio ativo no momento.";
                return View(new ChallengeViewModel());
            }

            // Verifica se o usuário já respondeu ao desafio
            var user = await _userManager.GetUserAsync(User);
            var userResponse = (await _challengeResponseRepository.GetAllAsync(new QueryOptions<ChallengeResponse>
            {
                Where = cr => cr.ChallengeId == currentChallenge.Id && cr.UserId == user.Id
            })).FirstOrDefault();

            bool hasResponded = userResponse != null;

            var challengeViewModel = new ChallengeViewModel
            {
                Challenge = currentChallenge,
                HasResponded = hasResponded,
                UserResponse = userResponse
            };

            return View(challengeViewModel);
        }

        // POST: Challenges/Respond
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Respond(int challengeId, int selectedOption)
        {
            var challenge = await _challengeRepository.GetByIdAsync(challengeId);
            if (challenge == null || challenge.IsArchived)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            // Verifica se o usuário já respondeu ao desafio
            var existingResponse = (await _challengeResponseRepository.GetAllAsync(new QueryOptions<ChallengeResponse>
            {
                Where = cr => cr.ChallengeId == challengeId && cr.UserId == user.Id
            })).FirstOrDefault();

            if (existingResponse != null)
            {
                TempData["ErrorMessage"] = "Você já respondeu a este desafio.";
                return RedirectToAction(nameof(Index));
            }

            // Determina se a resposta está correta
            bool isCorrect = selectedOption == challenge.CorrectOption;

            int pointsAwarded = 0;

            if (isCorrect)
            {
                // Determinar a ordem das respostas corretas
                var correctResponses = await _challengeResponseRepository.GetAllAsync(new QueryOptions<ChallengeResponse>
                {
                    Where = cr => cr.ChallengeId == challengeId && cr.IsCorrect,
                    OrderBy = cr => cr.RespondedAt,
                    OrderByDirection = "ASC"
                });

                int currentCorrectCount = correctResponses.Count();

                if (currentCorrectCount == 0)
                {
                    pointsAwarded = 7;
                }
                else if (currentCorrectCount == 1)
                {
                    pointsAwarded = 5;
                }
                else if (currentCorrectCount == 2)
                {
                    pointsAwarded = 3;
                }
                else
                {
                    pointsAwarded = 1;
                }
            }

            var response = new ChallengeResponse
            {
                ChallengeId = challengeId,
                UserId = user.Id,
                SelectedOption = selectedOption,
                IsCorrect = isCorrect,
                PointsAwarded = pointsAwarded
            };

            await _challengeResponseRepository.AddAsync(response);

            if (isCorrect)
            {
                TempData["SuccessMessage"] = $"Resposta correta! Você recebeu {pointsAwarded} pontos.";
            }
            else
            {
                TempData["ErrorMessage"] = "Resposta errada!";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Challenges/Ranking
        [AllowAnonymous]
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
