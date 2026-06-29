using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace yield;

public static class MovingMaxTask
{
    public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
    {
        DataPoint maxPoint = null;
        Queue<double> queue = new Queue<double>();
        LinkedList<double> linkedList = new LinkedList<double>();
        foreach (DataPoint point in data)
        {
            while (linkedList.Count > 0 && linkedList.Last.Value < point.OriginalY)
            {
                linkedList.RemoveLast();
            }
            linkedList.AddLast(point.OriginalY);
            queue.Enqueue(point.OriginalY);
            if (queue.Count > windowWidth)
            {
                if (linkedList.First.Value == queue.Dequeue())
                    linkedList.RemoveFirst();
            }
            maxPoint = point.WithMaxY(linkedList.First.Value);
            yield return maxPoint;
        }
    }
}