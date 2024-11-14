using FluentValidation;

namespace App.Application.Features.Categories.Update;
public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
	public UpdateCategoryRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("category name is required")
			.Length(2, 50).WithMessage("category name must be between 2,50");
	}
}
