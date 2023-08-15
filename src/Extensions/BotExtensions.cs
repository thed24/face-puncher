using System;
using FacePuncher.Models;
using Robocode.TankRoyale.BotApi;

namespace FacePuncher.Extensions;

public static class BotExtensions
{
    public static void Attack(this Bot bot, double x, double y)
    {
        double bearingToTarget = bot.GunBearingTo(x, y);
        bot.TurnGunLeft(bearingToTarget);
        
        var distanceToTarget = bot.DistanceTo(x, y);
        if (bot.GunHeat >= 1 || distanceToTarget > 500)
        {
            return;
        }
        
        bot.Fire(1);
        
        if (bearingToTarget > 1)
        {
            bot.Rescan();
        }
    }
    
    public static Wall ClosestWall(this Bot bot)
    {
        var arenaWidth = bot.ArenaWidth;
        var arenaHeight = bot.ArenaHeight;
        
        var x = bot.X;
        var y = bot.Y;
        
        var top = y;
        var bottom = arenaHeight - y;
        var left = x;
        var right = arenaWidth - x;
        
        var min = Math.Min(Math.Min(top, bottom), Math.Min(left, right));
        
        if (Math.Abs(min - top) < 2)
        {
            return Wall.Top;
        }
        
        if (Math.Abs(min - bottom) < 2)
        {
            return Wall.Bottom;
        }
        
        if (Math.Abs(min - left) < 2)
        {
            return Wall.Left;
        }
        
        return Wall.Right;
    }
}