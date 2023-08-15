using System;

namespace FacePuncher.Models;

public readonly record struct Position(double X, double Y)
{
    public double DistanceTo(Position other)
    {
        return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
    }
}