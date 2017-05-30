using System.Collections;
using UnityEngine;

public class FirstStageData : StageData
{
    private class Pattern1 : PatternClass
    {
        private int count = 5;
        private float spawnDelay = 1.5f;
        private float timer = 0.0f;
        private GameObject smallMonsterPrefab;
        public override void Start()
        {
            base.Start();

            smallMonsterPrefab = Resources.Load("Prefab/small_monster_1",typeof(GameObject)) as GameObject;
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if(timer >= spawnDelay && count > 0)
            {
                SpawnSmallMonster();
                count--;
                timer = 0.0f;
            }

            if(count == 0)
            {
                Clear();
            }
            
        }

        private void SpawnSmallMonster()
        {
            SmallMonster monster = Object.Instantiate(smallMonsterPrefab,new Vector3(-4.0f + count * 0.5f,6.0f,0.0f),Quaternion.identity).GetComponent<SmallMonster>();

            
        }
    }

    public override void StartStage()
    {
        base.StartStage();

        patterns.AddPattern(new Pattern1());
    }

    public override void UpdatePattern()
    {
        base.UpdatePattern();
    }
}