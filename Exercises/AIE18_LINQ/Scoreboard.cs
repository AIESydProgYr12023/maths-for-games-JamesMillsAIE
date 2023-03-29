using System.Text;

namespace AIE18_LINQ
{
	public class Scoreboard
	{
		public List<ScoreboardEntry> entries = new();

		public Scoreboard()
		{
			entries.Add(new ScoreboardEntry("Level 1", "Bobbert", 69, 420f, -50, true));
			entries.Add(new ScoreboardEntry("Level 2", "Joshua", -8, 16000f, 1, false));
			entries.Add(new ScoreboardEntry("Level 1", "Ryley", 8, 0.5f, 2, true));
			entries.Add(new ScoreboardEntry("Level 9", "Attila", 1903, 5.4f, 18, true));
			entries.Add(new ScoreboardEntry("Level 3", "Child", -9000, 16f, 6, false));
		}

		public override string ToString()
		{
			StringBuilder builder = new();
			builder.AppendLine("Level Name\tPlayer Name\tScore");

			entries.Where(_entry => _entry.score >= 0)
			       .OrderByDescending(_entry => _entry.score)
			       .Select(_entry => $"{_entry.levelName}\t\t{_entry.playerName}\t\t{_entry.score}")
			       .ToList()
			       .ForEach(_entry => builder.AppendLine(_entry));

			return builder.ToString();
		}
	}
}