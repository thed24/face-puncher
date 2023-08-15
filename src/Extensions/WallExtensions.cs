using System;
using FacePuncher.Models;

namespace FacePuncher.Extensions;

public static class WallExtensions
{
    public static double DistanceFromWall(this Wall wall, Position position)
    {
        return wall switch
        {
            Wall.Left => position.X,
            Wall.Right => 1 - position.X,
            Wall.Top => position.Y,
            Wall.Bottom => 1 - position.Y,
            _ => throw new ArgumentOutOfRangeException(nameof(wall), wall, null)
        };
    }
}