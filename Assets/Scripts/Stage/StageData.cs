using UnityEngine;
using System.Collections.Generic;

public abstract class StageData
{
	protected class StageObjectPool : ObjectPool<GameObject>
	{
		private List<ObjectData> prefabs;

		public StageObjectPool()
			: base(150)
		{
			prefabs = new List<ObjectData>(10);
		}

		public override GameObject RequestObject() {return null;}

		//키 값과 같은 오브젝트가 있다면 그 오브젝트를 반환
		//남는 오브젝트가 없다면 키값과 같은 프리팹을 찾아 생성후 반환
		//다 없다면 null 반환
		public override GameObject RequestObjectWithKey(string key)
		{
			for(int i = 0 ; i < objectList.Count ; i++)
			{
				if(!objectList[i].Object.activeSelf && objectList[i].Key == key)
				{
					Debug.Log("Recycle Idle Data .... StageData:RequestObjectWithKey");
					return objectList[i].Object;
				}
			}

			Debug.Log("Create new Object .... StageData:RequestObjectWithKey");			
			GameObject newObj = GetObjectFromPrefab(key);
			objectList.Add(new ObjectData(newObj,key));

			return newObj;
		}

		public override void Drain()
		{
			for(int i = 0 ; i < objectList.Count ; i++)
			{
				GameObject.Destroy(objectList[i].Object);
			}

			base.Drain();
		}

		public bool AddPrefabFromResources(string resourcePath)
		{
			GameObject newPrefab = Resources.Load("Prefab/" + resourcePath,typeof(GameObject)) as GameObject;
			if(newPrefab == null)
			{
				Debug.Log("Load Prefab Failed! .... StageData : AddPrefabFromResources");
				return false;
			}
			else
			{
				Debug.Log("Load Prefab Successful! .... StageData : AddPrefabFromResources");
				prefabs.Add(new ObjectData(newPrefab,resourcePath));

				return true;
			}
		}

		private GameObject GetObjectFromPrefab(string key)
		{
			for(int i = 0 ; i < prefabs.Count ; i++)
			{
				if(prefabs[i].Key == key)
				{
					GameObject obj = GameObject.Instantiate(prefabs[i].Object);

					return obj;
				}
			}

			return null;
		}
	}

	protected bool isRunning = false;
	protected bool isClear = false;
	protected int curPatternIndex = -1;
	protected PatternList patterns;
	protected StageObjectPool objectPool;

	public StageData()
	{
		patterns = new PatternList();
		objectPool = new StageObjectPool();
	}

	public virtual void StartStage()
	{
		isRunning = true;

		curPatternIndex = 0;
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

	public PatternList Patterns
	{
		get
		{
			return patterns;
		}
	}
}