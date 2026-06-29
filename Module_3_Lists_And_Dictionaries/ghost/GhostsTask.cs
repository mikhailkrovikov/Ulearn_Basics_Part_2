using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace hashes;

public class GhostsTask :
    IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
    IMagic
{
    private readonly Document _document;
    private readonly Segment _segment;
    private readonly Robot _robot;
    private readonly Vector _vector;
    private readonly Cat _cat;

    public GhostsTask()
    {
        _vector = new Vector(1, 1);
        _segment = new Segment(_vector, new Vector(2, 2));
        _cat = new Cat("ctorName", "ctorBreed", DateTime.Today);
        _robot = new Robot("ctorID:01010");
        _document = new Document("ctorDOC", Encoding.UTF8, Encoding.UTF8.GetBytes("bytes"));
    }

    public void DoMagic()
    {
        Random rnd = new Random();
        Robot.BatteryCapacity = rnd.Next(0, 20);
        _vector.Add(new Vector(10, 1));
        _cat.Rename("newName");
        var info = typeof(Document).GetField("content", BindingFlags.NonPublic | BindingFlags.Instance);
        var encodingString = Encoding.UTF8.GetBytes("12");
        info.SetValue(_document, encodingString);
    }

    Vector IFactory<Vector>.Create() => _vector;

    Segment IFactory<Segment>.Create()
    {
        return _segment;
    }

    Document IFactory<Document>.Create()
    {
        return _document;
    }

    Cat IFactory<Cat>.Create()
    {
        return _cat;
    }

    Robot IFactory<Robot>.Create()
    {
        return _robot;
    }
}