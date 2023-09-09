using BookApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BookApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly StackOverflowService _stackOverflowService;

        public QuestionsController()
        {
            _stackOverflowService = new StackOverflowService();
        }

        public async Task<IActionResult> Index()
        {
          

             List<Question> questions = await _stackOverflowService.GetQuestionsAsync();
            return View(questions);
        }

        public async Task<IActionResult> Details(int id)
        {
            var question = await _stackOverflowService.GetQuestionByIdAsync(id);
            return View(question);
        }
    }
}
