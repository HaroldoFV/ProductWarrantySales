using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductMicroservice.API.ApiModels.Product;
using ProductMicroservice.API.ApiModels.Response;
using ProductMicroservice.Application.UseCases.Product.Common;
using ProductMicroservice.Application.UseCases.Product.CreateProduct;
using ProductMicroservice.Application.UseCases.Product.DeleteProduct;
using ProductMicroservice.Application.UseCases.Product.GetProduct;
using ProductMicroservice.Application.UseCases.Product.ListProducts;
using ProductMicroservice.Application.UseCases.Product.UpdateProduct;
using ProductMicroservice.Domain.SeedWork.SearchableRepository;

namespace ProductMicroservice.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ProductModelOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductInput input,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(input, cancellationToken);
        return CreatedAtAction(
            nameof(Create),
            new { output.Id },
            new ApiResponse<ProductModelOutput>(output)
        );
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ProductModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateProductApiInput apiInput,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var input = new UpdateProductInput(
            id,
            apiInput.Name,
            apiInput.Price,
            apiInput.MinimumStock,
            apiInput.MaximumStock,
            apiInput.Stock,
            apiInput.Supplier,
            apiInput.HasWarranty
        );
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiResponse<ProductModelOutput>(output));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ProductModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(new GetProductInput(id), cancellationToken);
        return Ok(new ApiResponse<ProductModelOutput>(output));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(new DeleteProductInput(id), cancellationToken);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListProductsOutput), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        CancellationToken cancellationToken,
        [FromQuery] int? page = null,
        [FromQuery(Name = "per_page")] int? perPage = null,
        [FromQuery] string? search = null,
        [FromQuery] string? sort = null,
        [FromQuery] SearchOrder? dir = null
    )
    {
        var input = new ListProductsInput();
        if (page is not null) input.Page = page.Value;
        if (perPage is not null) input.PerPage = perPage.Value;
        if (!String.IsNullOrWhiteSpace(search)) input.Search = search;
        if (!String.IsNullOrWhiteSpace(sort)) input.Sort = sort;
        if (dir is not null) input.Dir = dir.Value;

        var output = await _mediator.Send(input, cancellationToken);
        return Ok(
            new ApiResponseList<ProductModelOutput>(output)
        );
    }
}