using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	private bool successfulAllStage = false;
	private int curStage = 0;
	private StageData[] stages;

	private void Awake()
	{
		stages = new StageData[1];
	}

	private void Start()
	{
		stages[0] = new FirstStageData();

		curStage = 1;
	}

	private void Update()
	{
		if(!successfulAllStage)
		{
			if(!stages[curStage - 1].IsClear && !stages[curStage - 1].IsRunning)
			{
				stages[curStage - 1].StartStage();
			}

			if(stages[curStage - 1].IsRunning)
			{
				stages[curStage - 1].UpdatePattern();
			}
			else
			{
				if(stages.Length == curStage)
				{
					successfulAllStage = true;
				}
				else
				{
					curStage++;
				}
			}
		}
	}
}
