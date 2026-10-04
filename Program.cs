using recipes.Services;
using recipes.View;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents();

builder.Services.AddScoped<RecipeService>();

var app = builder.Build();
app.MapRecipesRoutes();

app.Run();
