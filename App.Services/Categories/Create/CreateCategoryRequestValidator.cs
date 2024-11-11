using App.Services.Categories.Update;
using FluentValidation;

namespace App.Services.Products.Create;
public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
	public UpdateCategoryRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("category name is required")
			.Length(2, 50).WithMessage("category name must be between 2,50");

		
	}
}
