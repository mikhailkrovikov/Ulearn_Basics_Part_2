using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace linq_slideviews;

public class ParsingTask
{
    /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
    /// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
    /// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
    public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
    {
        var dict = lines.Skip(1);
        var slideRecords = new Dictionary<int, SlideRecord>();
        foreach (var line in dict)
        {
            try
            {
                var parsed = line.Split(';');
                var id = int.Parse(parsed[0]);
                var type = ConvertStringToType(parsed[1]);
                var title = parsed[2];
                slideRecords.Add(id, new SlideRecord(id, type, title));
            }
            catch (Exception)
            {
                continue;
            }
        }
        return slideRecords;
    }



    /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
    /// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
    /// Такой словарь можно получить методом ParseSlideRecords</param>
    /// <returns>Список информации о посещениях</returns>
    /// <exception cref="FormatException">Если среди строк есть некорректные</exception>
    public static IEnumerable<VisitRecord> ParseVisitRecords(
        IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
    {
        var visitedList = lines.Skip(1);
        var result = new List<VisitRecord>();
        foreach (var line in visitedList)
        {
            var parsed = line.Split(';', StringSplitOptions.RemoveEmptyEntries);
            if(parsed.Length != 4)
                throw new FormatException($"Wrong line [{line}]");
            var userId = int.TryParse(parsed[0], out var userIdValue) ? 
                userIdValue : throw new FormatException($"Wrong line [{line}]");
            var slideId = int.TryParse(parsed[1], out var slideIdValue) ? 
                slideIdValue : throw new FormatException($"Wrong line [{line}]");
            if (!slides.ContainsKey(slideId))
                throw new FormatException($"Wrong line [{line}]");
            var slideType = slides[slideId].SlideType;
            var d = DateTime.TryParse(parsed[2], CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateValue) ? 
                dateValue : throw new FormatException($"Wrong line [{line}]");
            var t = DateTime.TryParse(parsed[3], CultureInfo.InvariantCulture, DateTimeStyles.None, out var timeValue) ? 
                timeValue : throw new FormatException($"Wrong line [{line}]");
            var date = DateTime.TryParse(parsed[2] + " " + parsed[3], CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateValue1) ? 
                dateValue1 : throw new FormatException($"Wrong line [{line}]");
            result.Add(new VisitRecord(userId, slideId, date, slideType));
        }
        return result;
    }

    private static SlideType ConvertStringToType(string input)
    {
        if (input == "theory")
            return SlideType.Theory;
        else if (input == "exercise")
            return SlideType.Exercise;
        else if (input == "quiz")
            return SlideType.Quiz;
        else throw new InvalidCastException("Unable to cast input string to slide type");
    }
}