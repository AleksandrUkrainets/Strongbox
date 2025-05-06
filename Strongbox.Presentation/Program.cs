
using Microsoft.EntityFrameworkCore;
using Strongbox.Application.Interfaces;
using Strongbox.Application.Services;
using Strongbox.Domain.Interfaces;
using Strongbox.Persistance;
using Strongbox.Persistance.Repositories;

namespace Strongbox.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            });
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IAccessRequestRepository, AccessRequestRepository>();
            builder.Services.AddScoped<IDecisionRepository, DecisionRepository>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAccessRequestService, AccessRequestService>();
            builder.Services.AddScoped<IDecisionService, DecisionService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();

            var app = builder.Build();

            ApplyMigrations(app);

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapSwagger();
                app.UseSwagger();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ApplyMigrations(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
