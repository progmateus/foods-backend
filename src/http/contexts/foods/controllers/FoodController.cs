using Domain.Contexts.Foods.Handlers;
using Http.Contexts.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands;

namespace Http.Contexts.Foods.Controllers;

[Route("foods")]
public class FoodController : MainController
{
  [HttpGet]
  public async Task<IResult> List(
    [FromQuery] PaginationCommand command,
    [FromServices] ListFoodsHandler handler
  )
  {
    var result = await handler.Handle(command);
    if (!result.IsSuccess)
      return Results.Json(result, statusCode: result.Status);

    if (result.Data is null)
      return Results.Json(result, statusCode: 500);

    return Results.Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<IResult> GetProfile(
  [FromRoute] Guid id,
  [FromServices] GetFoodProfileHandler handler
  )
  {
    var result = await handler.Handle(id);
    if (!result.IsSuccess)
      return Results.Json(result, statusCode: result.Status);

    if (result.Data is null)
      return Results.Json(result, statusCode: 500);

    return Results.Ok(result);
  }
}