using System.Collections.Generic;
namespace yield;

public static class MovingAverageTask
{
    public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
    {
        DataPoint avGpoint = null;
        var queueForAvg = new Queue<double>();
        double sum = 0;
        double avG = 0;
        foreach (DataPoint point in data)
        {
            queueForAvg.Enqueue(point.OriginalY);
            if (queueForAvg.Count > windowWidth)
                sum -= queueForAvg.Dequeue();
            sum += point.OriginalY;
            avG = sum / queueForAvg.Count;
            avGpoint = point.WithAvgSmoothedY(avG);
            yield return avGpoint;
        }
    }
}