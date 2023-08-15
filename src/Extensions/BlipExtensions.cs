using System;
using System.Diagnostics.CodeAnalysis;
using FacePuncher.Models;

namespace FacePuncher.Extensions;

public static class BlipExtensions
{
    private const int AttackThreshold = 1;

    public static bool IsInAttackThreshold([NotNullWhen(true)] this Blip? blip, DateTime now)
    {
        return blip.HasValue && blip.Value.ScanTime.AddSeconds(AttackThreshold) > now;
    }
}