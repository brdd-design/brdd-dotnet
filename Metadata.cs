using System;
using System.Linq;
using System.Reflection;
using Brdd.Design.Core.Attributes;

namespace Brdd.Design.Core {

    public abstract class MetadataValidator<E> {
        public void ValidateAll(ValidationContext context, E data) {
            var methods = this.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.GetCustomAttributes(typeof(BrddRuleAttribute), false).Any());

            foreach (var method in methods) {
                var attr = (BrddRuleAttribute)method.GetCustomAttribute(typeof(BrddRuleAttribute));
                var result = method.Invoke(this, new object[] { data });

                if (result is bool success && !success) {
                    context.AddError(attr.Id, attr.Message);
                }
            }
        }
    }

    public static class UseCaseExtensions {
        public static string GetBrddUseCaseId(this object useCase) {
            var attr = useCase.GetType().GetCustomAttribute<BrddUseCaseAttribute>();
            return attr?.Id ?? "UNKNOWN";
        }
    }
}
