using System;

namespace FacePuncher.Models;

public readonly record struct Blip(DateTime ScanTime, MovingTarget Target);