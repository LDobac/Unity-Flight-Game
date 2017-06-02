using System.Collections;
using UnityEngine;

public class FirstStageData : StageData
{
    protected abstract class fsPattern : PatternClass
    {
        protected FirstStageData stageData;

        public fsPattern(FirstStageData stage) {stageData = stage;}
    }

    protected class StageStartPattern : fsPattern
    {
        public StageStartPattern(FirstStageData stage) : base(stage) {}

        public override void Update()
        {
            Debug.Log("Stage 1 Start");
            Clear();
        }
    }
    protected class MMovePattern : Monster.MonsterPattern
    {
        private Vector3 targetPos;
        private float speed;
        private float waitTime;
        private float timer = 0.0f;

        public MMovePattern(Monster monster,Vector3 targetPos,float speed,float waitTime)
            : base(monster)
        {
            this.targetPos = targetPos;
            this.speed = speed;
            this.waitTime = waitTime;
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if(timer >= waitTime)
            {
                Vector3 mvMent;
                mvMent = Vector3.MoveTowards(targetMonster.transform.position,
                targetPos,
                speed * Time.deltaTime);

                targetMonster.transform.position = mvMent;
            }
            
            if(targetPos == targetMonster.transform.position)
            {
                Clear();
                targetMonster.Idle();
            }
        }
    }

    protected class Pattern1 : fsPattern
    {
        private GameObject[] smallMonsterArr;
        private const int monsterCount = 5;

        public Pattern1(FirstStageData stage) : base(stage) {}
        
        public override void Start()
        {
            smallMonsterArr = new GameObject[monsterCount];

            for(int i = 0 ; i < monsterCount ; i++)
            {
                SmallMonster monster;
                smallMonsterArr[i] = SpawnSmallMonster();
                monster = smallMonsterArr[i].GetComponent<SmallMonster>();

                smallMonsterArr[i].transform.position = new Vector3(-4.5f + (i * 2.2f) ,11.0f,0.0f);
                smallMonsterArr[i].transform.Rotate(0.0f,0.0f,180.0f);
                monster.PatternList.AddPattern(
                    new MMovePattern(
                        monster,
                        new Vector3(smallMonsterArr[i].transform.position.x,-11.0f,0.0f),
                        15.0f,
                        (float)(i + 1) / 3));
                
            }

            base.Start();
        }

        public override void Update()
        {
            for(int i = 0 ; i < monsterCount ; i++)
            {
                if(!smallMonsterArr[i].GetComponent<SmallMonster>().PatternList.AllPatternPass)
                {
                    return;
                }
            }

            Clear();
        }

        private GameObject SpawnSmallMonster()
        {
            GameObject monster = stageData.objectPool.RequestObjectWithKey("small_monster_1");
            monster.GetComponent<SmallMonster>().Reset();

            return monster;
        }
    }

    protected class Pattern2 : fsPattern
    {
        private GameObject[] smallMonsterArr;
        private const int monsterCount = 5;

        public Pattern2(FirstStageData stage) : base(stage) {}
        
        public override void Start()
        {
            smallMonsterArr = new GameObject[monsterCount];

            base.Start();

            for(int i = 0 ; i < monsterCount ; i++)
            {
                smallMonsterArr[i] = SpawnSmallMonster();
                SmallMonster monster = smallMonsterArr[i].GetComponent<SmallMonster>();
                Vector3 finish;

                //Left
                if(i % 2 == 0)
                {
                    finish = new Vector3(7.5f,4.5f + i,0.0f);
                    smallMonsterArr[i].transform.position = new Vector3(-7.5f,-4.5f + i,0.0f);

                    Vector3 delta = (finish - smallMonsterArr[i].transform.position).normalized;
                    float radian = -Mathf.Atan2(delta.y,delta.x);
                    smallMonsterArr[i].transform.rotation = Quaternion.Euler(0.0f,0.0f,radian * Mathf.Rad2Deg);
                }
                //Right
                else
                {
                    finish = new Vector3(-7.5f,4.5f + i,0.0f);
                    smallMonsterArr[i].transform.position = new Vector3(7.5f,-4.5f + i,0.0f);

                    Vector3 delta = (finish - smallMonsterArr[i].transform.position).normalized;
                    float radian = -Mathf.Atan2(delta.y,delta.x);
                    smallMonsterArr[i].transform.rotation = Quaternion.Euler(0.0f,0.0f,radian * Mathf.Rad2Deg);
                }

                monster.PatternList.AddPattern(
                    new MMovePattern(
                        monster,
                        finish,
                        15.0f,
                        (float)(i + 1) / 3));
            }
        }

        public override void Update()
        {
            for(int i = 0 ; i < monsterCount ; i++)
            {
                if(!smallMonsterArr[i].GetComponent<SmallMonster>().PatternList.AllPatternPass)
                {
                    return;
                }
            }

            Clear();
        }

        private GameObject SpawnSmallMonster()
        {
            GameObject monster = stageData.objectPool.RequestObjectWithKey("small_monster_2");
            monster.GetComponent<SmallMonster>().Reset();

            return monster;
        }
    }
    protected class StageEndPattern : fsPattern
    {
        public StageEndPattern(FirstStageData stage) : base(stage) {}

        public override void Update()
        {
            Debug.Log("Stage 1 Clear ! ");
            Clear();
        }
    }

    public FirstStageData()
        :base()
    {
        objectPool.AddPrefabFromResources("small_monster_1");
        objectPool.AddPrefabFromResources("small_monster_2");
    }

    public override void StartStage()
    {
        base.StartStage();

        patterns.AddPattern(new StageStartPattern(this));
        patterns.AddPattern(new Pattern1(this));
        patterns.AddPattern(new Pattern2(this));
        patterns.AddPattern(new StageEndPattern(this));
    }

    public override void UpdatePattern()
    {
        base.UpdatePattern();
    }
}