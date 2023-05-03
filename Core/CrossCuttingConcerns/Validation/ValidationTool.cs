using Core.Utilities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            //16.satırdan itibaren şöyle gerçekleşiyor;
            //context adında bir değişken adı verip ValidationContextin çalışma türüne eşitliyoruz.
            //result adında bir değişken oluşturup context'i validate ettiriyoruz.
            //result is NOT valid ise result.errors methodunu çalıştırıyoruz.
        }
    }
}

 