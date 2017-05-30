using UnityEngine;

public abstract class StageData
{
	protected bool isRunning = false;
	protected bool isClear = false;
	protected int curPatternIndex = -1;
	protected PatternList patterns;

	public virtual void StartStage()
	{
		isRunning = true;

		curPatternIndex = 0;

		patterns = new PatternList();
	}

	public virtual void UpdatePattern()
	{
		if(isRunning)
		{
			patterns.Update();

			if(patterns.AllPatternPass)
			{
				isRunning = false;
				isClear = true;

				Debug.Log("Pass All Pattern Stage");
			}
		}
	}

	public bool IsRunning
	{
		get
		{
			return isRunning;
		}
	}

	public bool IsClear
	{
		get
		{
			return isClear;
		}
	}
}
