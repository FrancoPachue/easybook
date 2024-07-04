using EasyBook.App.Middleware;
using EasyBook.Application.Behaviors;
using EasyBook.Domain.Repositories;
using EasyBook.Infrastructure.Configs;
using EasyBook.Infrastructure.Repository;
using EasyBook.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProtoBuf.Meta;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables();
//builder.Services.Configure<PulsarConfig>(builder.Configuration.GetSection("PulsarConfig"));
//builder.Services.AddSingleton(cfg => cfg.GetService<IOptions<PulsarConfig>>().Value);

builder
    .Services
    .Scan(
        selector => selector
            .FromAssemblies(
                EasyBook.Infrastructure.AssemblyReference.Assembly)
            .AddClasses(false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
var applicationAssembly = EasyBook.Application.AssemblyReference.Assembly;

builder.Services.AddLogging();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(applicationAssembly);
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(applicationAssembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddDbContext<InMemoryContext>(options =>
{
    options.UseInMemoryDatabase("FixtureGroupDb");
});
builder
    .Services
    .AddScoped<ISubscriptionRepository, SubscriptionRepository>()
    .AddScoped<IUnitOfWork>(
            factory => factory.GetRequiredService<InMemoryContext>())
    .AddScoped<PulsarService>()
    .AddControllers()
    .AddApplicationPart(EasyBook.Presentation.AssemblyReference.Assembly);

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();