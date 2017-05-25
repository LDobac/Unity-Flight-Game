using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	private int curStage = 0;
	private StageData[] stages;

	private void Awake()
	{
		stages = new StageData[3];
	}

	private void Start()
	{
		stages[0] = new FirstStageData();

		curStage = 1;
	}

	private void Update()
	{
		if(!stages[curStage - 1].CheckPassAllPattern())
		{
			stages[curStage - 1].UpdatePattern();
		}
		else
		{
			if(stages.Length == curStage)
			{

			}
			else
			{
				curStage++;
			}
		}
	}
}
