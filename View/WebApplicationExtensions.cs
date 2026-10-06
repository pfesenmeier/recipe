namespace recipes.View;

public static class WebApplicationExtensions
{
    extension(RouteGroupBuilder group)
    {
        public RouteGroupBuilder MapRecipeRoutes() => Recipe.Recipe.MapRoutes(group);
        public RouteGroupBuilder MapRecipesRoutes() => Recipes.Recipes.MapRoutes(group);
        public RouteGroupBuilder MapHomeRoutes() => Home.Home.MapRoutes(group);
    }

    extension(WebApplication app)
    {
        public WebApplication MapRoutes()
        {
          app.MapGroup("recipe").MapRecipeRoutes();
          app.MapGroup("recipes").MapRecipesRoutes();
          app.MapGroup("/").MapHomeRoutes();

          return app;
        }
    }
}
