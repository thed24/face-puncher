using System;
using FacePuncher.Extensions;
using FacePuncher.Models;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

namespace FacePuncher;

public class FacePuncher : Bot
{
    private Blip? LastScan { get; set; }
    private double EnemyDistance => LastScan?.Target.DistanceTo(X, Y) ?? double.MaxValue;

    FacePuncher() : base(BotInfo.FromFile($"{nameof(FacePuncher)}.json"))
    {
    }

    static void Main(string[] args)
    {
        new FacePuncher().Start();
    }

    public override void Run()
    {
        Initialize();

        while (IsRunning)
        {
            DateTime now = DateTime.Now;

            Attack(now);
            Move();
        }
    }

    public override void OnScannedBot(ScannedBotEvent evt)
    {
        MovingTarget target = MovingTarget.From(evt);
        LastScan = new Blip(DateTime.Now, target);
        
        var rammed = TryRam();
        if (!rammed)
        {
            this.Attack(target.X, target.Y);
        }
    }

    private void Attack(DateTime now)
    {
        if (LastScan.IsInAttackThreshold(now))
        {
            MovingTarget target = LastScan.Value.Target;
            var rammed = TryRam();
            if (!rammed)
            {
                this.Attack(target.PredictedX(), target.PredictedY());
            }
        }
    }
    
    private void Move()
    {
        var rammed = TryRam();
        if (!rammed)
        {
            var closestWall = this.ClosestWall();
            var distanceFromWall = closestWall.DistanceFromWall(new Position(X, Y));
            
            if (distanceFromWall < 50)
            {
                TurnLeft(90);
            }
            
            Forward(50);
        }
        
        TurnGunLeft(90);
    }

    private bool TryRam()
    {
        if (!(EnemyDistance < 250) || !LastScan.IsInAttackThreshold(DateTime.Now))
        {
            return false;
        }

        var x = LastScan.Value.Target.PredictedX();
        var y = LastScan.Value.Target.PredictedY();
        var bearingToTarget = BearingTo(x, y);
        TurnLeft(bearingToTarget);

        Forward(200);
        
        return true;
    }
    
    private void Initialize()
    {
        Color hotPink = new(255, 105, 180);

        BodyColor = hotPink;
        TurretColor = hotPink;
        RadarColor = hotPink;
        ScanColor = hotPink;
        BulletColor = hotPink;
    }
}