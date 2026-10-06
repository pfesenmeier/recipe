using Microsoft.AspNetCore.Mvc;
using recipes.Services;

namespace recipes.View.Recipe;


public partial class Recipe
{
  public static RouteGroupBuilder MapRoutes(RouteGroupBuilder group)
  {
    group.MapGet("/{id}", GetRecipeHandler);
    group.MapGet("/{id}/edit", GetRecipeFormHandler);
    group.MapPut("/{id}", EditRecipeHandler);
    return group;
  }

  public static async Task<IResult> GetRecipeHandler(
      RecipeService service,
      int id,
      [FromHeader(Name = "HX-Request-Type")] string requestType = "full"
      )
  {
    var recipe = service.GetRecipe(id)!;
    ViewModel model = new(
        new(recipe.Id, recipe.Name, recipe.Content),
        requestType != "partial"
    );

    return model.RenderPage();
  }

  public static async Task<IResult> GetRecipeFormHandler(RecipeService service, int id)
  {
    // TODO - could get the data from the url instead of hitting the db
    var recipe = service.GetRecipe(id)!;
    ViewModel model = new(new(recipe.Id, recipe.Name, recipe.Content));

    // TODO - handlde non-partial requests
    return EditForm(model.Recipe).RenderFragment();
  }

  public static async Task<IResult> EditRecipeHandler([FromForm] RecipeInput form, int id, RecipeService service)
  {
    var recipe = service.UpdateRecipe(id, form);

    ViewModel model = new(new(recipe.Id, recipe.Name, recipe.Content));

    return Display(model.Recipe).RenderFragment();
  }
}
