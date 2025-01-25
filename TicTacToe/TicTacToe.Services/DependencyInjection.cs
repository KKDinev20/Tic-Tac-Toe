// <copyright file="DependencyInjection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Services.Game;
using TicTacToe.Services.Game.Contracts;
using TicTacToe.Services.Player;
using TicTacToe.Services.Player.Contracts;

namespace TicTacToe.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<IGameService, GameService>();

        return services;
    }
}