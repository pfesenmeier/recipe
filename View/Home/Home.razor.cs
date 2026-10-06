using Microsoft.AspNetCore.Mvc;

namespace recipes.View.Home;

public partial class Home
{
    public static RouteGroupBuilder MapRoutes(RouteGroupBuilder group)
    {
       group.MapGet("/", GetHomeHandler);

       return group;
    }

    public static IResult GetHomeHandler(
        [FromHeader(Name="HX-Request-Type")] string requestType = "full"
    )
    {
      ViewModel model = new(requestType != "partial");
      return model.RenderPage();
    }
}
