using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Clones;

public partial class CloneVersionSystem : ICloneVersionSystem
{
    private readonly List<Clone> clones = new List<Clone>();
    public CloneVersionSystem()
    {
        Clone clone = new Clone();
        clones.Add(clone);
    }

    public string Execute(string query)
    {
        string command = query.Split(' ')[0];
        int cloneNumber = int.Parse(query.Split(' ')[1]);

        switch (command)
        {
            case "learn":
                string programNumber = query.Split(' ')[2];
                clones[cloneNumber - 1].LearnCommand(programNumber);
                return null;
            case "rollback":
                clones[cloneNumber - 1].RollbackCommand();
                return null;
            case "relearn":
                clones[cloneNumber - 1].RelearnCommand();
                return null;
            case "clone":
                clones.Add(clones[cloneNumber - 1].CloneCommand());
                return null;
            case "check":
                return clones[cloneNumber - 1].CheckCommand();
            default: return null;
        }
    }
}