using recipes.Db;

namespace recipes.Services;

public record RecipeInput(string Name, string Content);

public class RecipeService
{
  private readonly List<Recipe> recipes = [new(1, "Cookies", "..."), new(2, "Polenta", "...")];

  public IEnumerable<Recipe> GetRecipes() => recipes;

  public Recipe? GetRecipe(int id)
  {
    return recipes.Find(r => r.Id == id);
  }

  public Recipe CreateRecipe(RecipeInput input)
  {
    var nextId = recipes.Max(r => r.Id) + 1;

    Recipe recipe = new(nextId, input.Name, input.Content);
    recipes.Add(recipe);

    return recipe;
  }

  public Recipe UpdateRecipe(int id, RecipeInput input)
  {
    var toUpdate = recipes.FindIndex(r => r.Id == id);

    if (toUpdate != -1)
    {
      recipes[toUpdate] = (recipes[toUpdate] with
      {
        Name = input.Name,
        Content = input.Content,
      });
    }

    return recipes[toUpdate];
  }

  public void DeleteRecipe(int id)
  {
    recipes.RemoveAll(r => r.Id == id);
  }
}
