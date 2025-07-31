
using FurnitureManagement.Application.Interfaces;
using FurnitureManagement.Application.Services;
using FurnitureManagement.Domain.Entities;
using FurnitureManagement.Infrastructure.Data;
using FurnitureManagement.Infrastructure.Repositories;
using FurnitureManagement.Presentation.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FurnitureManagement.Presentation
{


    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var serverUrl = builder.Configuration["ServerConfig:URL"];
            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(op =>

                    op.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                );


                option.AddPolicy("corsPolicy", policy =>
                {
                    policy.WithOrigins(serverUrl)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                });

            });
            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Development")));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IService<Component>, ComponentService>();
            builder.Services.AddScoped<IService<Product>, ProductService>();
            builder.Services.AddScoped<IService<Subcomponent>, SubSubcomponentService>();



            builder.Services.AddControllers();
            builder.Services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }




            app.UseHttpsRedirection();

            app.UseCors("corsPolicy");
            app.UseExceptionHandler("/error");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
