using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)//attribute
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                //eğer validator type fluentvalidationdan gelen ıvalidator değilse if blogunun ici calıssın.
                throw new System.Exception("This is not a validator class.");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            //29.satır bir reflection örneğidir. Çalışacağın validator'in bir instance ını oluşturmasını sağlar.
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //31.satırda doğrulama yapılacak olan sınıfın çalışma tipini bul diyor ve generic oldugu icin eleman numarasını istiyor.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //34.satırda bu buldugu sınıfın parametrelerini bulmasını sağlıyor.Çalışacağın classın methodunun parametresinin
            //entityType'a eşit olup olmadıgını kontrol ettiriyor ve bunun LINQ kullanarak yapıyor
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
                //birden fazla parametre olabilecegi için foreach ile dönüyoruz.
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
