﻿using Microsoft.Extensions.DependencyInjection;

namespace SportBuddy.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}