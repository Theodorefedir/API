using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGames.BLL.Dtos;

namespace VideoGames.BLL.Validators
{
    public class CreateDeveloperValidator : AbstractValidator<CreateDeveloperDto>
    {
        public CreateDeveloperValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ім'я розробника обов'язкове")
                .MaximumLength(255).WithMessage("Ім'я не може бути довше 255 символів");

            RuleFor(x => x.Country)
                .MaximumLength(100).WithMessage("Країна не може бути довшою за 100 символів");

            RuleFor(x => x.Year)
                .GreaterThan(1900).WithMessage("Рік заснування має бути більшим за 1900")
                .LessThanOrEqualTo(DateTime.UtcNow.Year)
                .WithMessage("Рік не може бути в майбутньому");
        }
    }
}
