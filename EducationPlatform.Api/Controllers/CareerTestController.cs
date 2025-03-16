using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Dto.CareerTest;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EducationPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/career-test")]
    public class CareerTestController : ControllerBase
    {
        private readonly ICareerTestQuestionService _questionService;
        private readonly ICareerTestAnswerService _answerService;
        private readonly IChatbotService _chatbotService;
        private readonly IMapper _mapper;

        public CareerTestController(ICareerTestQuestionService questionService,
                                    ICareerTestAnswerService answerService,
                                    IChatbotService chatbotService,
                                    IMapper mapper)
        {
            _questionService = questionService;
            _answerService = answerService;
            _chatbotService = chatbotService;
            _mapper = mapper;
        }

        // 📌 **Kariyer testi sorularını getir**
        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            // Kariyer testi sorularını almak
            var questions = await _questionService.GetAllQuestionsAsync();

            // Soruları DTO'ya dönüştürmek
            var questionDtos = _mapper.Map<List<CareerTestQuestionDto>>(questions);

            // Soruları geri döndürmek
            return Ok(questionDtos);
        }


        // 📌 **Kullanıcının verdiği cevapları kaydet**
        [HttpPost("submit-answers")]
        public async Task<IActionResult> SubmitAnswers([FromBody] CareerTestAnswersDto dto)
        {
            if (dto == null || dto.Answers == null || dto.Answers.Count == 0)
            {
                return BadRequest(new { message = "Cevaplar eksik!" });
            }

            // Cevapları kaydet
            await _answerService.SaveUserAnswersAsync(dto.UserId, dto.Answers);
            return Ok(new { message = "Cevaplar başarıyla kaydedildi." });
        }

        // 📌 **Kullanıcının test sonuçlarına göre AI önerisi al**
        [HttpPost("career-advice-from-test")]
        public async Task<IActionResult> GetCareerAdviceFromTest([FromBody] CareerRequest request)
        {
            if (request == null || request.UserId <= 0)
            {
                return BadRequest(new { message = "Geçersiz kullanıcı ID." });
            }

            var userAnswers = await _answerService.GetUserAnswersAsync(request.UserId);

            if (userAnswers == null || !userAnswers.Any())
            {
                return BadRequest(new { message = "Kariyer testi cevapları alınamadı." });
            }

            string formattedAnswers = string.Join(", ", userAnswers.Select(a => $"Soru {a.QuestionId}: {a.SelectedAnswer}"));

            var result = await _chatbotService.GetCareerAdviceFromTestAsync(request.UserId, formattedAnswers);

            if (string.IsNullOrEmpty(result))
            {
                return BadRequest(new { message = "AI'dan yanıt alınamadı." });
            }

            var careerAdviceResponse = new CareerAdviceResponseDto
            {
                CareerAdvice = result
            };

            return Ok(careerAdviceResponse);
        }

    }
}
