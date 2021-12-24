using AutoMapper;
using ElPlatform.BAL.IServices;
using ElPlatform.BAL.Profiles;
using ElPlatform.BAL.Services;
using ElPlatform.DAL;
using ElPlatform.DAL.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Security.Claims;
using System.Text;

namespace ElPlatform.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Configure Entityframecore with SQL SErver
            services.AddDbContext<ElPlatformDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                //options.UseSqlServer(Configuration.GetConnectionString("ConnStr"));
                options.UseSqlServer(Configuration.GetConnectionString("TestDB"));
                
            });

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<ElPlatformDbContext>().AddDefaultTokenProviders();


            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:Audience"],
                    ValidIssuer = Configuration["JWT:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                    ValidateIssuerSigningKey = true
                };
            });
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMediaService, MediaService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins("http://joocode-001-site1.itempurl.com").AllowAnyHeader().AllowAnyMethod();
                    //policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationUserProfile());
                mc.AddProfile(new MediaItemProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ElPlatform.API", Version = "v1" });
            });

            // Configure the identity options for the logged in user 
            services.AddScoped(sp =>
            {
                var identityOptions = new BAL.Options.IdentityOptions();
                var httpContext = sp.GetService<IHttpContextAccessor>().HttpContext;
                if (httpContext.User.Identity.IsAuthenticated)
                {
                    identityOptions.UserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    identityOptions.FirstName = httpContext.User.FindFirst(ClaimTypes.GivenName).Value;
                    identityOptions.LastName = httpContext.User.FindFirst(ClaimTypes.Surname).Value;
                }
                return identityOptions;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ElPlatform.API v1"));
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
