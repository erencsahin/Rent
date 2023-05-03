using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(c => c.ColorName).NotEmpty();
            RuleFor(c => c.ColorName).MinimumLength(1);
            //ikisini de aynı yerde yazmamamın sebebi(satır 18) Solid tekniginin single responsibility prensibinden dolayı
            //RuleFor(c=>c.ColorName).MaximumLength(1).NotEmpty();
        }
    }
}
