using FluentValidation;

namespace App.Application.Features.Categories.Create;
public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
	public CreateCategoryRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("category name is required")
			.Length(2, 50).WithMessage("category name must be between 2,50");

		
	}
}
