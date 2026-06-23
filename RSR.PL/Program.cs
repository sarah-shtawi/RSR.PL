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

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"]!))
                 };
             });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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

            app.MapGet("/", () =>
            {
                const string page = """
                    <!DOCTYPE html>
                    <html lang="en">
                    <head>
                        <meta charset="UTF-8" />
                        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
                        <title>RSR API</title>
                        <style>
                            :root {
                                --bg: #f5f7fb;
                                --card: #ffffff;
                                --text: #0f172a;
                                --muted: #475569;
                                --primary: #0ea5e9;
                                --primary-hover: #0284c7;
                            }
                            * { box-sizing: border-box; }
                            body {
                                margin: 0;
                                font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
                                background: linear-gradient(135deg, #f5f7fb 0%, #e2e8f0 100%);
                                color: var(--text);
                                min-height: 100vh;
                                display: grid;
                                place-items: center;
                                padding: 24px;
                            }
                            .card {
                                width: min(680px, 100%);
                                background: var(--card);
                                border-radius: 16px;
                                padding: 32px;
                                box-shadow: 0 20px 45px rgba(2, 8, 23, 0.12);
                                text-align: center;
                            }
                            h1 {
                                margin: 0 0 12px;
                                font-size: clamp(28px, 4vw, 40px);
                            }
                            p {
                                margin: 0 0 24px;
                                color: var(--muted);
                                font-size: 18px;
                                line-height: 1.5;
                            }
                            .btn {
                                display: inline-block;
                                text-decoration: none;
                                border: none;
                                border-radius: 10px;
                                padding: 12px 22px;
                                font-weight: 700;
                                background: var(--primary);
                                color: #fff;
                                transition: background .2s ease-in-out;
                            }
                            .btn:hover {
                                background: var(--primary-hover);
                            }
                        </style>
                    </head>
                    <body>
                        <main class="card">
                            <h1>RSR API is Running</h1>
                            <p>The application is working correctly. You can explore and test endpoints in Swagger.</p>
                            <a class="btn" href="/swagger">Open Swagger API Docs</a>
                        </main>
                    </body>
                    </html>
                    """;

                return Results.Content(page, "text/html");
            })
            .AllowAnonymous();

            app.Run();
        }
    }
}
