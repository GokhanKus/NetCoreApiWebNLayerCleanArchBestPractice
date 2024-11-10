using FluentValidation;
using Microsoft.Identity.Client;

namespace App.Services.Products;
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("product name cannot be null")
            .NotEmpty().WithMessage("product name is required")
            .Length(2, 50).WithMessage("product name must be between 2,50");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("price value must be greater than 0");

        RuleFor(x => x.Stock)
            .InclusiveBetween(0, 1000).WithMessage("stock value must be between 0,1000");
    }
}
