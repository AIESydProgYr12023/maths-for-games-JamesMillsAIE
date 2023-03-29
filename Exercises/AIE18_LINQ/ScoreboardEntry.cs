namespace AIE18_LINQ
{
	public class ScoreboardEntry : IComparable<ScoreboardEntry>
	{
		public string levelName;
		public string playerName;
		public int score;
		public float levelTime;
		public int enemiesKilled;
		public bool levelCompleted;

		public ScoreboardEntry(string _levelName, string _playerName, int _score, 
		                       float _levelTime, int _enemiesKilled, bool _levelCompleted)
		{
			levelName = _levelName;
			playerName = _playerName;
			score = _score;
			levelTime = _levelTime;
			enemiesKilled = _enemiesKilled;
			levelCompleted = _levelCompleted;
		}

		public int CompareTo(ScoreboardEntry? other)
		{
			if(ReferenceEquals(this, other))
				return 0;
			if(ReferenceEquals(null, other))
				return 1;

			return score.CompareTo(other.score);
		}
	}
}