using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecondStageData : StageData
{
    protected abstract class fsPattern : PatternClass
    {
        protected SecondStageData stageData;

        public fsPattern(SecondStageData stage) {stageData = stage;}

    }

    protected class FadeInPattern : fsPattern
    {
        private bool fadein = true;
        private float tmpMinY = 0.0f;
        private float transparent = 1.0f;  
        private Image blackPanel;
        private PlayerController player;
        
        public FadeInPattern(SecondStageData stage) : base(stage) {}

        public override void Start()
        {
            base.Start();

            blackPanel = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Image>();
            blackPanel.color = new Color(0.0f,0.0f,0.0f,1.0f);
            blackPanel.gameObject.SetActive(true);

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.SetControlPlayer(false);
            tmpMinY = player.moveLimit.minY;
            player.moveLimit.minY = -30.0f;      

            player.transform.position = new Vector3(0.0f, -20.0f,0.0f);   
        }
        
        public override void Update()
        {
            if(fadein)
            {
                if(transparent >= 0.0f )
                {
                    transparent -= 1.0f * Time.deltaTime;
                    blackPanel.color = new Color(0.0f,0.0f,0.0f,transparent);

                    if(transparent <= 0.0f)
                    {
                        fadein = false;
                    }
                }
            }
            else
            {
                Vector3 target = new Vector3(0.0f,-6.0f,0.0f);
                player.transform.position = Vector3.MoveTowards(player.transform.position,target,player.moveSpeed * 0.5f * Time.deltaTime);

                if(target == player.transform.position)
                {
                    player.SetControlPlayer(true);
                    player.moveLimit.minX = tmpMinY;
                    Clear();
                }
            }
        }
    }
    protected class StageStartPattern : fsPattern
    {
        private bool toggle = false;
        private float transparent = 0.0f;
        private Text startText;
        public StageStartPattern(SecondStageData stage) : base(stage) {}

        public override void Start()
        {
            Debug.Log("Stage 2 Start");            

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

    protected class StageEndPattern : fsPattern
    {
        private bool move = true;
        private bool moveToggle = false;
        private float transparent = 0.0f;  
        private Image blackPanel;
        private PlayerController player;
        
        public StageEndPattern(SecondStageData stage) : base(stage) {}

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

                        SceneManager.LoadScene(2);
                    }
                }
            }
        }
    }

    public override void StartStage()
    {
        base.StartStage();

        patterns.AddPattern(new FadeInPattern(this));
        patterns.AddPattern(new StageStartPattern(this));
        patterns.AddPattern(new StageEndPattern(this));
    }

    public override void UpdatePattern()
    {
        base.UpdatePattern();
    }
}