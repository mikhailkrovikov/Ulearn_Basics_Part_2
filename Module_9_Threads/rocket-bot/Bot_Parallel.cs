using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot;

public partial class Bot
{
	public Rocket GetNextMove(Rocket rocket)
    {
        var results = Task.WhenAll(CreateTasks(rocket)).GetAwaiter().GetResult();
        var (turn, score) = results.MaxBy(x => x.Score);
        return rocket.Move(turn, level);
    }

    public List<Task<(Turn Turn, double Score)>> CreateTasks(Rocket rocket)
    {
        var tasks = new List<Task<(Turn Turn, double Score)>>();
        for (int i = 0; i < threadsCount; i++)
            tasks.Add(Task.Run(() => SearchBestMove(rocket, new Random(random.Next()), iterationsCount)));
        return tasks;
    }
}