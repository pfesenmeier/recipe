using Microsoft.AspNetCore.Mvc;
using recipes.Services;

namespace recipes.View.Recipes;

public partial class Recipes
{
  public static RouteGroupBuilder MapRoutes(RouteGroupBuilder group)
  {
    group.MapGet("/", GetRecipesHandler);
    group.MapPost("/", CreateRecipeHandler);
    group.MapPut("/{id}/edit", EditRecipeHandler);
    group.MapGet("/{id}/edit", GetEditFormHandler);
    group.MapGet("/{id}/delete", GetDeleteConfirmationHandler);
    group.MapDelete("/{id}/delete", DeleteRecipeHandler);

    return group;
  }

  public static async Task<IResult> GetRecipesHandler(
      RecipeService service,
      [FromHeader(Name = "HX-Request-Type")] string requestType
  )
  {
    var recipes = service.GetRecipes();
    var recipeViews = recipes.Select(r => new RecipeView(r.Id, r.Name));

    ViewModel model = new(recipeViews, requestType != "partial");

    return model.RenderPage();
  }

  public record EditFormValue(int Id, string Name, string Content);
  public static async Task<IResult> GetEditFormHandler(RecipeService service, int id)
  {
    var recipe = service.GetRecipe(id)!;
    EditFormValue form = new(recipe.Id, recipe.Name, recipe.Content);

    return EditForm(form).RenderFragment();
  }

  public static async Task<IResult> GetDeleteConfirmationHandler(RecipeService service, int id)
  {
    var recipe = service.GetRecipe(id)!;
    EditFormValue form = new(recipe.Id, recipe.Name, recipe.Content);

    return DeleteForm(form).RenderFragment();
  }


  public static async Task<IResult> CreateRecipeHandler(RecipeService service, [FromForm] RecipeInput form)
  {
    var recipe = service.CreateRecipe(form);
    RecipeView recipeView = new(recipe.Id, recipe.Name);

    return Recipe(recipeView).RenderFragment();
  }

  public static async Task<IResult> EditRecipeHandler(RecipeService service, int id, [FromForm] RecipeInput form)
  {
    var recipe = service.UpdateRecipe(id, form);
    RecipeView recipeView = new(recipe.Id, recipe.Name);

    return Recipe(recipeView).RenderFragment();
  }

  public static async Task<IResult> DeleteRecipeHandler(RecipeService service, int id)
  {
    service.DeleteRecipe(id);

    return Results.Ok();
  }
}
