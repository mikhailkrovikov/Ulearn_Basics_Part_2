using System;
using System.Collections.Generic;
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
        //var slideIds = ParseSlideRecords(lines);
        foreach (var line in visitedList)
        {
            var parsed = line.Split(';');
            var userId = int.Parse(parsed[0]);
            var slideId = int.Parse(parsed[1]);
            var datetime = DateTime.Parse(parsed[2]);
            var slideType = slides[int.Parse(parsed[1])].SlideType;
            result.Add(new VisitRecord(userId, slideId, datetime, slideType));
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