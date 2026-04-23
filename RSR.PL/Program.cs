using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RSR.BLL.mapsterConfigration;
using RSR.DAL.Data;
using RSR.DAL.Models.User;
using RSR.DAL.Utils;
using System.Text;
using System.Threading.Tasks;

namespace RSR.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            // cors policy 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });


            // conecct with data base 
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Identity 
         
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true; // Email Unigue 
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();


            // for deploy file on server 
            builder.Services.AddHttpContextAccessor();


            // config 
            AppConfigrations.Config(builder.Services);

            var mapster = new mapsterConfig(builder.Configuration);
            mapster.MapterConfigRegiter();

            // jwt 
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
             {
                
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"]))
                 };
             });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");
            app.UseStaticFiles();


            //  app.UseHttpsRedirection();
            app.UseAuthorization();

            // Seed Data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeders = services.GetServices<ISeedData>();
                foreach (var seeder in seeders)
                {
                    await seeder.DataSeed();
                }
            }
            app.MapControllers();
            app.Run();
        }
    }
}
