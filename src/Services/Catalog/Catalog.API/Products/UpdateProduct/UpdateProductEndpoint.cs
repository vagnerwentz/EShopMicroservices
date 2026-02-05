namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResponse(bool Success);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id}", async (UpdateProductRequest request, ISender sender, Guid id) =>
        {
            var command = request.Adapt<UpdateProductCommand>() with { Id = id };
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update product by id")
        .WithDescription("Update product by id");
    }
}