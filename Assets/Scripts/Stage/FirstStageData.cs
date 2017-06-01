using System.Collections;
using UnityEngine;

public class FirstStageData : StageData
{
    protected abstract class fsPattern : PatternClass
    {
        protected FirstStageData stageData;

        public fsPattern(FirstStageData stage) {stageData = stage;}
    }
    protected class Pattern1 : fsPattern
    {

        private int count = 100;
        private float spawnDelay = 0.5f;
        private float timer = 0.0f;

        public Pattern1(FirstStageData stage) : base(stage) {}
        
        public override void Start()
        {
            base.Start();
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
            GameObject monster = stageData.objectPool.RequestObjectWithKey("small_monster_1");
            monster.SetActive(true);
            monster.transform.position = Vector3.zero;
        }
    }

    public FirstStageData()
        :base()
    {
        objectPool.AddPrefabFromResources("small_monster_1");
    }

    public override void StartStage()
    {
        base.StartStage();

        patterns.AddPattern(new Pattern1(this));
    }

    public override void UpdatePattern()
    {
        base.UpdatePattern();
    }
}