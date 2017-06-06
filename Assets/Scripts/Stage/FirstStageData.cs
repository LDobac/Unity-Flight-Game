using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstStageData : StageData
{
    protected abstract class fsPattern : PatternClass
    {
        protected FirstStageData stageData;

        public fsPattern(FirstStageData stage) {stageData = stage;}
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
            }
        }
    }

    protected class StageStartPattern : fsPattern
    {
        private bool toggle = false;
        private float transparent = 0.0f;
        private Text startText;
        public StageStartPattern(FirstStageData stage) : base(stage) {}

        public override void Start()
        {
            Debug.Log("Stage 1 Start");            

            base.Start();
            startText = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>();
            startText.gameObject.SetActive(true);
            startText.color = new Color(0.0f,0.0f,0.0f,0.0f);
        }

        public override void Update()
        {
            if(!toggle && transparent < 1.0f )
            {
                transparent += 1.0f * Time.deltaTime;
                startText.color = new Color(0.0f,0.0f,0.0f,transparent);

                if(transparent >= 1.0f)
                {
                    toggle = true;
                }
            }
            else if(toggle && transparent >= 0.0f)
            {
                transparent -= 1.0f * Time.deltaTime;
                startText.color = new Color(0.0f,0.0f,0.0f,transparent);

                if(transparent <= 0.0f)
                {
                    startText.gameObject.SetActive(false);
                    
                    Clear();       
                }
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

            for(int i = 0 ; i < monsterCount ; i++)
            {
                smallMonsterArr[i].gameObject.SetActive(false);
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

            for(int i = 0 ; i < monsterCount ; i++)
            {
                smallMonsterArr[i].gameObject.SetActive(false);
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

    protected class BossPattern : fsPattern
    {
        private float cosAngle = 0.0f;
        private FirstStageBoss bossMonster;
        public BossPattern(FirstStageData stage) : base(stage) {}

        public override void Start()
        {
            base.Start();

            bossMonster = stageData.objectPool.RequestObjectWithKey("first_stage_boss").GetComponent<FirstStageBoss>();
            bossMonster.Reset();
            bossMonster.transform.position = new Vector3(0.0f,13.0f,0.0f);

            bossMonster.AddPattern(new MMovePattern(bossMonster,new Vector3(0.0f,6.0f,0.0f),bossMonster.Speed,0.0f));
        }

        public override void Update()
        {
            cosAngle += Time.deltaTime;
            Vector3 pos = bossMonster.transform.position;
            pos.y += Mathf.Sin(cosAngle) * Time.deltaTime;
            bossMonster.transform.position = pos;

            if(bossMonster.IsDied)
            {
                Debug.Log("Killed First Stage Boss");
                Clear();
            }
        }
    }

    protected class StageEndPattern : fsPattern
    {
        private bool move = true;
        private bool moveToggle = false;
        private float transparent = 0.0f;  
        private Image blackPanel;
        private PlayerController player;
        
        public StageEndPattern(FirstStageData stage) : base(stage) {}

        public override void Start()
        {
            base.Start();

            blackPanel = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Image>();
            blackPanel.color = new Color(0.0f,0.0f,0.0f,0.0f);
            blackPanel.gameObject.SetActive(true);

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.SetControlPlayer(false);
            player.moveLimit.maxY = 15.0f;
        }
        
        public override void Update()
        {
            if(move)
            {
                if(!moveToggle)
                {
                    Vector3 target = new Vector3(0.0f,-4.0f,0.0f);
                    player.transform.position = Vector3.MoveTowards(player.transform.position,target,player.moveSpeed * 0.5f * Time.deltaTime);

                    if(target == player.transform.position)
                    {
                        moveToggle = true;
                    }
                }
                else
                {
                    Vector3 target = new Vector3(0.0f,12.0f,0.0f);
                    player.transform.position = Vector3.MoveTowards(player.transform.position,target,player.moveSpeed * Time.deltaTime);

                    if(target == player.transform.position)
                    {
                        move = false;
                    }
                }   
            }
            else
            {
                if(transparent <= 1.0f )
                {
                    transparent += 1.0f * Time.deltaTime;
                    blackPanel.color = new Color(0.0f,0.0f,0.0f,transparent);

                    if(transparent >= 1.0f)
                    {
                        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                        player.SetControlPlayer(false);

                        Clear();

                        SceneManager.LoadScene(1);
                    }
                }
            }
        }
    }

    public FirstStageData()
        :base()
    {
        objectPool.AddPrefabFromResources("small_monster_1");
        objectPool.AddPrefabFromResources("small_monster_2");
        objectPool.AddPrefabFromResources("first_stage_boss");
    }

    public override void StartStage()
    {
        base.StartStage();

        patterns.AddPattern(new StageStartPattern(this));
        patterns.AddPattern(new Pattern1(this));
        patterns.AddPattern(new Pattern2(this));
        patterns.AddPattern(new BossPattern(this));
        patterns.AddPattern(new StageEndPattern(this));
    }

    public override void UpdatePattern()
    {
        base.UpdatePattern();
    }
}