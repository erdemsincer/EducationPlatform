using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.CareerGoalDto;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CareerGoalController : ControllerBase
{
    private readonly ICareerGoalService _careerGoalService;
    private readonly IMapper _mapper;

    public CareerGoalController(ICareerGoalService careerGoalService, IMapper mapper)
    {
        _careerGoalService = careerGoalService;
        _mapper = mapper;
    }

    // 🔹 Kullanıcıya ait kariyer hedeflerini getir
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var careerGoal = await _careerGoalService.GetCareerGoalByUserIdAsync(userId);
        if (careerGoal == null)
        {
            return NotFound("Kariyer hedefi bulunamadı.");
        }

        var result = _mapper.Map<ResultCareerGoalDto>(careerGoal); // Dönüştürme işlemi
        return Ok(result);
    }

    // 🔹 Tüm kariyer hedeflerini getir
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var values = await _careerGoalService.TGetListAllAsync();
        var result = _mapper.Map<List<ResultCareerGoalDto>>(values);
        return Ok(result);
    }

    // 🔹 Tek bir kariyer hedefini getir (ID ile)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var value = await _careerGoalService.TGetByIdAsync(id);
        if (value == null)
            return NotFound("Kariyer hedefi bulunamadı.");

        var result = _mapper.Map<ResultCareerGoalDto>(value);
        return Ok(result);
    }

    // 🔹 Kariyer hedefi ekle
    [HttpPost]
    public async Task<IActionResult> Create(CreateCareerGoalDto dto)
    {
        var careerGoal = _mapper.Map<CareerGoal>(dto);
        await _careerGoalService.TAddAsync(careerGoal);
        return Ok("Kariyer hedefi eklendi.");
    }

    // 🔹 Kariyer hedefi güncelle
    [HttpPut]
    public async Task<IActionResult> Update(UpdateCareerGoalDto dto)
    {
        var careerGoal = _mapper.Map<CareerGoal>(dto);
        await _careerGoalService.TUpdateAsync(careerGoal);
        return Ok("Kariyer hedefi güncellendi.");
    }

    // 🔹 Kariyer hedefi sil
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var value = await _careerGoalService.TGetByIdAsync(id);
        if (value == null)
            return NotFound("Kariyer hedefi bulunamadı.");

        await _careerGoalService.TDeleteAsync(value);
        return Ok("Kariyer hedefi silindi.");
    }
}
