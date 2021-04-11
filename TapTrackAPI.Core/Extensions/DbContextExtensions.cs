using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace TapTrackAPI.Core.Extensions
{
    public static class DbContextExtensions
    {
        public static IQueryable Set(this DbContext context, Type T)
        {
            MethodInfo method =
                typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);

            method = method.MakeGenericMethod(T);

            return method.Invoke(context, null) as IQueryable;
        }
    }
}