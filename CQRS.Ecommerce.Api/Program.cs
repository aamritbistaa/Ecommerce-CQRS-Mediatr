using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using CQRS.Ecommerce.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


// Register MediatR with the correct assembly
// builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof().Assembly));

// Register application services
builder.Services.AddInfrastrcucture(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting(); // Ensure routing is used
app.UseAuthorization(); // Ensure authorization is used
app.MapControllers(); // Map controllers to routes

app.Run();
