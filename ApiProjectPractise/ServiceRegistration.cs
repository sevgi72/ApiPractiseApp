using System.Text;
using ApiProjectPractise.Data;
using ApiProjectPractise.Dtos.UserDtos;
using ApiProjectPractise.Models;
using ApiProjectPractise.Profiles;
using ApiProjectPractise.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

namespace ApiProjectPractise
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(opt=>opt.AddProfile(new MapperProfile(new HttpContextAccessor())));
            services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
            services.AddScoped<JwtService>();

           services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });







            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
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
            Array.Empty<string>()
        }
    });
            });

            services.AddIdentity<AppUser, IdentityRole>(
                opt =>
                {
                    //burda bezi congigurationlar yazilir.
                    //Mesele passwordun minimum uzunlugu, reqem, boyuk herf,
                    //kicik herf olub olmamasi kimi.
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequireNonAlphanumeric = false;
                })
                //hansi database e tetbiq olunur onu gosterir
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            //authentication ucun JWT Bearer token istifade edirik
            services.AddAuthentication(x =>
            {
                //default verirki teleb elemesin
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            )
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        //gelen tokenden ne teleb edirikse onu bura yaziriq
                        ValidateIssuer = true,
                        //false elemek ignore ele demedkir, true elemek validate ele demekdir
                        ValidateAudience = true,
                        ValidateLifetime = true, //expire olub olmadigini yoxlayir
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
                    };
                });

        }
    }
}
