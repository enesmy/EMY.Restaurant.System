using System;

namespace EMY.Restaurant.Core.Domain.Common
{
    public static class Extantions
    {
        public static Guid ToGuid(this string value)
        {
            if (value == null) return Guid.Empty;
            if (Guid.TryParse(value, out Guid gd)) return gd;
            return Guid.Empty;
        }
    }
}
