using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace linq_slideviews;

public static class ExtensionsTask
{
    /// <summary>
    /// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
    /// Медиана списка из четного количества элементов — это среднее арифметическое 
    /// двух серединных элементов списка после сортировки.
    /// </summary>
    /// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
    public static double Median(this IEnumerable<double> items)
    {
        var sorted = items.OrderBy(x => x).ToList();
        var index = sorted.Count / 2;
        if(sorted.Count == 0) throw new InvalidOperationException();
        if (sorted.Count % 2 != 0)
            return sorted[index];
        else return (sorted[index - 1] + sorted[index]) / 2;
    }

    /// <returns>
    /// Возвращает последовательность, состоящую из пар соседних элементов.
    /// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
    /// </returns>
    public static IEnumerable<(T First, T Second)> Bigrams<T>(this IEnumerable<T> items)
    {
        T previous = default;
        bool isFirst = true;
        foreach (var item in items)
        {         
            if (isFirst)
            {
                previous = item;
                isFirst = false;
                continue;
            }
            yield return (previous, item);
            previous = item;
        }
    }
}