using FluentValidation;

namespace App.Services.Products.Update;
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
	public UpdateProductRequestValidator()
	{
		RuleFor(x => x.Name)
				.NotEmpty().WithMessage("product name is required")
				.Length(2, 50).WithMessage("product name must be between 2,50");

		RuleFor(x => x.Price)
				.GreaterThan(0).WithMessage("price value must be greater than 0");

		RuleFor(x => x.Stock)
				.InclusiveBetween(0, 1000).WithMessage("stock value must be between 0,1000");
	}
}
