
public class StageData
{
	protected bool isRunning = false;
	protected bool isClear = false;
	protected int curPatternIndex = -1;
	protected PatternClass[] patterns;

	public void StartStage()
	{
		isRunning = true;

		curPatternIndex = 0;
	}

	public void UpdatePattern()
	{
		if(isRunning)
		{
			if(!patterns[curPatternIndex].IsComplete)
			{
				patterns[curPatternIndex].Update();
			}
			else
			{
				if(CheckPassAllPattern())
				{
					isRunning = false;
				}
				else
				{
					curPatternIndex++;
				}
			}
		}
	}

	public bool CheckPassAllPattern()
	{
		if(curPatternIndex == patterns.Length)
		{
			return true;
		}

		return false;
	}
}
