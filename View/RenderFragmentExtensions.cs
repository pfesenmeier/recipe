using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;

namespace recipes.View;

public static class RenderFragmentExtensions
{
    public static RazorComponentResult<Fragment> RenderFragment(this RenderFragment fragment)
    {
      return new(new { ChildContent = fragment });
    }

}
