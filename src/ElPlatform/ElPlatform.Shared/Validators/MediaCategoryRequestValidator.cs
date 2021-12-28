using ElPlatform.Shared.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElPlatform.Shared.Validators
{
    public class MediaCategoryRequestValidator : AbstractValidator<MediaCategoryRequestVM>
    {
        public MediaCategoryRequestValidator()
        {
            RuleFor(p => p.NameEn)
               .NotEmpty().
               WithMessage("Name En Is Requierd");
            RuleFor(p => p.NameAr)
               .NotEmpty().
               WithMessage("Name Ar Is Requierd");
            RuleFor(p => p.IsActive)
              .NotEqual(false).
              WithMessage("Item must be Active");
        }

    }
}
