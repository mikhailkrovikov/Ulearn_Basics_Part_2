using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Documentation;

public class Specifier<T> : ISpecifier
{
    public string GetApiDescription()
    {
        var attr = typeof(T).GetCustomAttribute<ApiDescriptionAttribute>();
        if (attr == null) return null;
        var description = attr.Description;
        return description;
    }

    public string[] GetApiMethodNames()
    {
        return typeof(T).GetMethods()
            .Where(m => m.GetCustomAttribute<ApiMethodAttribute>() != null)
            .Select(m => m.Name)
            .ToArray();
    }

    public string GetApiMethodDescription(string methodName)
    {
        var a = typeof(T).GetMethods()
            .Where(m => m.GetCustomAttribute<ApiDescriptionAttribute>() != null)
            .FirstOrDefault(m => m.Name.Equals(methodName));
        if (a == null) return null;
        return a.GetCustomAttribute<ApiDescriptionAttribute>().Description;
    }

    public string[] GetApiMethodParamNames(string methodName)
    {
        var a = typeof(T).GetMethods()
            .FirstOrDefault(m => m.Name.Equals(methodName));
        if (a == null) return null;
        return a.GetParameters()
            .Select(p => p.Name)
            .ToArray();
    }

    public string GetApiMethodParamDescription(string methodName, string paramName)
    {
        var a = typeof(T).GetMethods()
           .FirstOrDefault(m => m.Name.Equals(methodName));
        if (a == null) return null;
        var attr = a.GetParameters()
            .Where(p => p.GetCustomAttribute<ApiDescriptionAttribute>() != null)
            .FirstOrDefault(s => s.Name.Equals(paramName));
        if (attr == null) return null;
        return attr.GetCustomAttribute<ApiDescriptionAttribute>().Description;
    }

    public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
    {
        var a = typeof(T).GetMethods()
           .FirstOrDefault(m => m.Name.Equals(methodName));
        var param = a?.GetParameters()
            .FirstOrDefault(s => s.Name.Equals(paramName));
        var description = new ApiParamDescription
        {
            ParamDescription = new CommonDescription
            {
                Name = paramName,
                Description = param?.GetCustomAttribute<ApiDescriptionAttribute>()?.Description
            },
            Required = param?.GetCustomAttribute<ApiRequiredAttribute>()?.Required ?? false,
            MinValue = param?.GetCustomAttribute<ApiIntValidationAttribute>()?.MinValue,
            MaxValue = param?.GetCustomAttribute<ApiIntValidationAttribute>()?.MaxValue
        };
        return description;
    }

    public ApiMethodDescription GetApiMethodFullDescription(string methodName)
    {
        var a = typeof(T).GetMethods()
            .Where(m => m.GetCustomAttribute<ApiMethodAttribute>() != null)
            .FirstOrDefault(m => m.Name.Equals(methodName));
        if (a == null) return null;
        return new ApiMethodDescription
        {
            MethodDescription = new CommonDescription
            {
                Name = methodName,
                Description = a.GetCustomAttribute<ApiDescriptionAttribute>()?.Description
            },
            ParamDescriptions = a.GetParameters()
                .Select(p => GetApiMethodParamFullDescription(methodName, p.Name)).ToArray(),

            ReturnDescription = a.ReturnType == typeof(void)
            ? null
            : new ApiParamDescription
            {
                MaxValue = a.ReturnParameter?.GetCustomAttribute<ApiIntValidationAttribute>()?.MaxValue,
                MinValue = a.ReturnParameter?.GetCustomAttribute<ApiIntValidationAttribute>()?.MinValue,
                Required = a.ReturnParameter?.GetCustomAttribute<ApiRequiredAttribute>()?.Required ?? false,
                ParamDescription = new CommonDescription
                {
                    Name = a.ReturnParameter?.Name == "" ? null : a.ReturnParameter?.Name,
                    Description = a.ReturnParameter.GetCustomAttribute<ApiDescriptionAttribute>()?.Description,
                }
            },
        };
    }
}