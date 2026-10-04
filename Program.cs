using recipes.Services;
using recipes.View;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents();

builder.Services.AddSingleton<RecipeService>();

var app = builder.Build();
app.MapRecipesRoutes();

app.Run();
