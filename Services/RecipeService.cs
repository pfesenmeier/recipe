using recipes.Db;

namespace recipes.Services;

public record RecipeCreateInput(string Name, string Content);
public record RecipeUpdateInput(int Id, string Name, string Content);
public record RecipeDeleteInput(int Id);

public class RecipeService
{
  private readonly List<Recipe> recipes = [new(1, "Cookies", "..."), new(2, "Polenta", "...")];

  public IEnumerable<Recipe> GetRecipes() => recipes;

  public void CreateRecipe(RecipeCreateInput input)
  {
    var nextId = recipes.Max(r => r.Id) + 1;

    Recipe recipe = new(nextId, input.Name, input.Content);
    recipes.Add(recipe);
  }

  public void UpdateRecipe(RecipeUpdateInput input)
  {
    var toUpdate = recipes.FindIndex(r => r.Id == input.Id);

    if (toUpdate != -1)
    {
      recipes[toUpdate] = (recipes[toUpdate] with
      {
        Name = input.Name,
        Content = input.Content,
      });
    }
  }

  public void DeleteRecipe(RecipeDeleteInput input)
  {
    recipes.RemoveAll(r => r.Id == input.Id);
  }
}
