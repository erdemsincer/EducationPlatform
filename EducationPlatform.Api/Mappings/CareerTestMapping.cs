using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.CareerTest;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CareerTestProfile : Profile
{
    public CareerTestProfile()
    {
        // CareerTestAnswersDto -> CareerTestAnswer (Kullanıcının cevaplarını string olarak kaydetmek için)
        CreateMap<CareerTestAnswersDto, CareerTestAnswer>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.SelectedAnswer, opt => opt.MapFrom(src =>
                string.Join(";", src.Answers.Select(a => $"{a.Key}:{a.Value}")))); // Cevapları string olarak birleştiriyoruz

        // CareerTestAnswer -> CareerTestAnswersDto (Veritabanındaki cevapları DTO'ya dönüştürmek için)
        CreateMap<CareerTestAnswer, CareerTestAnswersDto>()
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src =>
                ConvertToAnswers(src.SelectedAnswer))); // Burada Split işlemi yapıyoruz

        // CareerTestQuestion -> CareerTestQuestionDto (Soru ve seçenekleri DTO'ya dönüştürmek için)
        CreateMap<CareerTestQuestion, CareerTestQuestionDto>()
            .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.Question))
            .ForMember(dest => dest.Options, opt => opt.MapFrom(src =>
                src.Options != null ? DeserializeOptions(src.Options) : new List<string>()));
    }

    // Yardımcı metod: 'SelectedAnswer' string'ini 'KeyValuePair' listesine dönüştür
    private static List<KeyValuePair<int, string>> ConvertToAnswers(string selectedAnswer)
    {
        if (string.IsNullOrEmpty(selectedAnswer))
            return new List<KeyValuePair<int, string>>();

        return selectedAnswer.Split(';')
                             .Select(a =>
                             {
                                 var parts = a.Split(':');
                                 return new KeyValuePair<int, string>(int.Parse(parts[0]), parts[1]);
                             })
                             .ToList();
    }

    // JSON stringlerini deserialize ederek List<string> tipine dönüştüren yardımcı metod
    private static List<string> DeserializeOptions(List<string> options)
    {
        var result = new List<string>();

        foreach (var option in options)
        {
            try
            {
                // JSON stringini deseralize ediyoruz
                var deserializedOptions = JsonConvert.DeserializeObject<List<string>>(option);
                result.AddRange(deserializedOptions); // Her bir JSON'u çözüp listeye ekliyoruz
            }
            catch (JsonReaderException ex)
            {
                // JSON hatasını düzgün bir şekilde ele alabilirsiniz
                Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
            }
        }

        return result;
    }
}
