using System;

namespace func_rocket;

public class ControlTask
{
    public static Turn ControlRocket(Rocket rocket, Vector target)
    {
        var vector = target - rocket.Location;
        var angle = (rocket.Direction - vector.Angle) / 2 + (rocket.Velocity.Angle - vector.Angle);
        if (Math.Abs(angle) > 0)
            if (angle > 0) return Turn.Left;
            else return Turn.Right;
        return Turn.None;
    }
}