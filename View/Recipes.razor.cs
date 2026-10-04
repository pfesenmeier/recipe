
using recipes.Services;

namespace recipes.View;

public static class WebApplicationExtension
{
  public static WebApplication MapRecipesRoutes(this WebApplication app)
  {
    return Recipes.MapRecipesRoutes(app);
  }
}

public partial class Recipes
{
  public static WebApplication MapRecipesRoutes(WebApplication app)
  {
    app.MapGet("/", GetRecipesHandler);
    return app;
  }

  public static async Task<IResult> GetRecipesHandler(RecipeService service)
  {
    var recipes = service.GetRecipes();
    var recipeViews = recipes.Select(r => new RecipeView(r.Id, r.Name));
    ViewModel model = new(recipeViews);

    return model.Render();
  }
}
