using Avalonia.Animation;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace func_rocket;

public class LevelsTask
{
    static readonly Physics standardPhysics = new();

    public static IEnumerable<Level> CreateLevels()
    {
        var target = new Vector(600, 200);
        var anomaly = (new Vector(200, 500) + target) / 2;
        var rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);

        yield return new Level("Zero", rocket, target, (size, v) => Vector.Zero, standardPhysics);

        yield return new Level("Heavy", rocket, target, (size, v) => new Vector(0, 0.9), standardPhysics);

        yield return new Level("Up", rocket, new Vector(700, 500), (size, v) => new Vector(0, -300 / (size.Y - v.Y + 300.0)), standardPhysics);

        yield return new Level("WhiteHole", rocket, target, (size, v) => CalculateGravity(v, target, 140), standardPhysics);

        yield return new Level("BlackHole", rocket, target, (size, v) => CalculateGravity(anomaly, v, 300), standardPhysics);

        yield return new Level("BlackAndWhite", rocket, target, (size, v) => (CalculateGravity(v, target, 140) + CalculateGravity(anomaly, v, 300)) / 2, standardPhysics);
    }

    private static Vector CalculateGravity(Vector first, Vector second, double coeff)
    {
        var d = (first - second).Length;
        return (first - second).Normalize() * (coeff * d / (d * d + 1));
    }
}