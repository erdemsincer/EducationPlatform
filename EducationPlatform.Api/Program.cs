using EducationPlatform.Application.Abstract;
using EducationPlatform.Application.Concrete;
using EducationPlatform.Application.Security;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// **Veritabaný Baðlantýsý**
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// **CORS Politikasý**
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// **Environment Variable’dan OpenAI API Key Okuma**
var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User)
           ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.Machine);

if (string.IsNullOrEmpty(apiKey))
{
    throw new Exception("OpenAI API key bulunamadý! Lütfen 'OPENAI_API_KEY' environment variable'ýný ayarla.");
}

// **OpenAiService için API key’i Singleton olarak DI'a ekleyelim**
builder.Services.AddSingleton<OpenAiService>(provider => new OpenAiService(apiKey));

// **Dependency Injection - Servisler ve Repository'ler**
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserDal, EFUserDal>();
builder.Services.AddScoped<ICommentService, CommentManager>();
builder.Services.AddScoped<ICommentDal, EFCommentDal>();
builder.Services.AddScoped<IFavoriteService, FavoriteManager>();
builder.Services.AddScoped<IFavoriteDal, EFFavoriteDal>();
builder.Services.AddScoped<IResourceService, ResourceManager>();
builder.Services.AddScoped<IResourceDal, EFResourceDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EFCategoryDal>();
builder.Services.AddScoped<TokenGenerator>();
builder.Services.AddScoped<IDiscussionService, DiscussionManager>();
builder.Services.AddScoped<IDiscussionDal, EFDiscussionDal>();
builder.Services.AddScoped<IRoleService, RoleManager>();
builder.Services.AddScoped<IUserRoleDal, EFUserRoleDal>();
builder.Services.AddScoped<IUserRoleService, UserRoleManager>();
builder.Services.AddScoped<ITestimonaiService, TestimonialManager>();
builder.Services.AddScoped<ITestimonialDal, EFTestimonialDal>();
builder.Services.AddScoped<IAboutService, AboutManager>();
builder.Services.AddScoped<IAboutDal, EFAboutDal>();
builder.Services.AddScoped<IBannerService, BannerManager>();
builder.Services.AddScoped<IBannerDal, EFBannerDal>();
builder.Services.AddScoped<IContactService, ContactManager>();
builder.Services.AddScoped<IContactDal, EFContactDal>();
builder.Services.AddScoped<IMessageService, MessageManager>();
builder.Services.AddScoped<IMessageDal, EFMessageDal>();
builder.Services.AddScoped<ISocialMediaService, SocialMediaManager>();
builder.Services.AddScoped<ISocialMediaDal, EFSocialMediaDal>();
builder.Services.AddScoped<ISubscriberService, SubscriberManager>();
builder.Services.AddScoped<ISubscriberDal, EFSubscriberDal>();
builder.Services.AddScoped<IRoleDal, EFRoleDal>();
builder.Services.AddScoped<IDiscussionReplyService, DiscussionReplyManager>();
builder.Services.AddScoped<IDiscussionReplyDal, EFDiscussionReplyDal>();
builder.Services.AddScoped<IInterestService, InterestManager>();
builder.Services.AddScoped<IInterestDal, EFInterestDal>();
builder.Services.AddScoped<ISkillService, SkillManager>();
builder.Services.AddScoped<ISkillDal, EFSkillDal>();
builder.Services.AddScoped<ICareerGoalDal, EFCareerGoal>();
builder.Services.AddScoped<ICareerGoalService, CareerGoalManager>();
builder.Services.AddScoped<IReviewDal, EFReviewDal>();
builder.Services.AddScoped<IReviewService, ReviewManager>();
builder.Services.AddScoped<IInstructorDal, EFInstructorDal>();
builder.Services.AddScoped<IInstructorService, InstructorManager>();
builder.Services.AddScoped<IChatbotService, ChatbotManager>();
builder.Services.AddScoped<IChatbotDal, EFChatbotDal>();

builder.Services.AddScoped<ICareerTestDal, EFCareerTestDal>();
builder.Services.AddScoped<ICareerTestQuestionDal, EFCareerTestQuestionDal>();
builder.Services.AddScoped<ICareerTestAnswerDal, EFCareerTestAnswerDal>();
builder.Services.AddScoped<ICareerTestQuestionService, CareerTestQuestionManager>();
builder.Services.AddScoped<ICareerTestAnswerService, CareerTestAnswerManager>();






// **JWT Authentication Ayarlarý**
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true
    };
});

// **Newtonsoft.Json Desteði**
builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// **Session Kullanýmý**
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// **Swagger JWT Token Desteði**
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Bearer token'ýnýzý giriniz: (Örnek: Bearer {token})",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// **Swagger UI**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
