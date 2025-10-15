using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Configurations
{
    internal static class SrtringPropertyBuilder
    {
        public static PropertyBuilder<string> HasDefaultStringConfig(
            this PropertyBuilder<string> propertyBuilder,
            int maxLength,
            bool isRequired = true)
        {
            var configured = propertyBuilder.HasMaxLength(maxLength);
            return isRequired ? configured.IsRequired() : configured;
        }

        // отдельный метод из-за C#8
        // по большей степени бесполезен проще написать HasMaxLength, но да ладно
        public static PropertyBuilder<string?> HasDefaultStringConfigForNull(
            this PropertyBuilder<string?> propertyBuilder,
            int maxLength)
        {
            return propertyBuilder.HasMaxLength(maxLength);
        }
    }
}
