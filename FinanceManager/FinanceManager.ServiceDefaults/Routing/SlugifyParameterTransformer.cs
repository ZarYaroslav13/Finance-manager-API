﻿using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace FinanceManager.ServiceDefaults.Routing;

public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value == null ? null :
            Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2")
            .ToLower();
    }
}
