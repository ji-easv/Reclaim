using Reclaim.Application.Extensions;
using Reclaim.Presentation.Apis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDomainEventBus();
builder.Services.AddDbContexts(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddCommandAndQueryHandlers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.AddOrderApi();
app.AddListingApi();
app.AddReviewApi();
app.AddUserApi();

app.Run();