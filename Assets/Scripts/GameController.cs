using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	public int stageCount;

	private bool successfulAllStage = false;
	private int curStage = 0;
	private StageData stage;

	private void Start()
	{
		int index = SceneManager.GetActiveScene().buildIndex;
		
		switch(index)
		{
			case 0:
				stage = new FirstStageData();			
				break;
			case 1:
				stage = new SecondStageData();			
				break;
			case 2:
				stage = new ThirdStageData();			
				break;
		}

		curStage = index + 1;
	}

	private void Update()
	{
		if(!successfulAllStage)
		{
			if(!stage.IsClear && !stage.IsRunning)
			{
				stage.StartStage();
			}

			if(stage.IsRunning)
			{
				stage.UpdatePattern();
			}
			else
			{
				stage.Drain();
				stage = null;
				if(stageCount == curStage)
				{
					successfulAllStage = true;
					Debug.Log("All Stage Clear!");
				}
				else
				{
					curStage++;
				}
			}
		}
	}
}