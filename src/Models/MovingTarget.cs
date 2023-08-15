using System;
using Robocode.TankRoyale.BotApi.Events;

namespace FacePuncher.Models;

public readonly record struct MovingTarget(Position Position, double Direction, double Speed)
{
    public double X => Position.X;
    public double Y => Position.Y;

    public static MovingTarget From(ScannedBotEvent evt)
    {
        return new MovingTarget(new Position(evt.X, evt.Y), evt.Direction, evt.Speed);
    }
    
    public double PredictedX()
    {
        return X + Speed * Math.Sin(Direction);
    }
    
    public double PredictedY()
    {
        return Y + Speed * Math.Cos(Direction);
    }
    
    public double DistanceTo(double x, double y)
    {
        return Math.Sqrt(Math.Pow(X - x, 2) + Math.Pow(Y - y, 2));
    }
}