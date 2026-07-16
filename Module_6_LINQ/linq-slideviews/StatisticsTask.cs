using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace linq_slideviews;

public class StatisticsTask
{
    public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
    {
        var inCorrectSlides = visits.Where(s => s.SlideType == slideType).ToList();
        if (inCorrectSlides.Count == 0) return 0;

        var userGroup = visits
            .OrderBy(v => v.DateTime)
            .GroupBy(v => v.UserId)
            .Select(group => group
                .Bigrams()
                .Where(v => v.First.SlideType == slideType)
                .Select(v => (v.Second.DateTime - v.First.DateTime).TotalMinutes)
                    .Where(x => x >= 1 && x <= 120))
            .SelectMany(x => x);

        if(!userGroup.Any()) return 0;
        return userGroup.Median();
    }
}