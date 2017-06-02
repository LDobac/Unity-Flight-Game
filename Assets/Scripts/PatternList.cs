using System.Collections.Generic;
using UnityEngine;

public abstract class PatternClass
{
    private bool isRunning = false;
    private bool isComplete = false;

    public virtual void Start()
    {
        isRunning = true;
        isComplete = false;
    }
    
    public abstract void Update();

    public virtual void Clear()
    {
        isRunning = false;
        isComplete = true;
    }

    public bool IsComplete
    {
        get
        {
            return isComplete;
        }
    }

    public bool IsRunning
    {
        get
        {
            return isRunning;
        }
    }
}

public class PatternList
{
    private bool pause = false;
    private bool loop = false;
    private bool allPatternPass = true;
    private int curPattern = 0;
    private List<PatternClass> patterns;

    public PatternList()
    {
        patterns = new List<PatternClass>();        
    }

    public void AddPattern(PatternClass pattern)
    {
        patterns.Add(pattern);

        allPatternPass = false;
    }

    public void Update()
    {
        if(!allPatternPass && !pause)
        {
            if(!patterns[curPattern].IsRunning && !patterns[curPattern].IsComplete)
            {
                patterns[curPattern].Start();
            }

            if(patterns[curPattern].IsRunning)
            {
                patterns[curPattern].Update();

                if(patterns[curPattern].IsComplete)
                {
                    curPattern++;

                    if(curPattern == patterns.Count)
                    {
                        if(loop)
                        {
                            curPattern = 0;
                        }
                        else
                        {
                            allPatternPass = true;
                        }                        
                    }
                }
            }
        }
    }

    public PatternClass GetCurPattern()
    {
        return patterns[curPattern];
    }

    public void Stop()
    {
        allPatternPass = true;
    }

    public void Pause()
    {
        pause = true;
    }

    public void Resume()
    {
        pause = false;
    }

    public bool AllPatternPass
    {
        get
        {
            return allPatternPass;
        }
    }

    public bool Loop
    {
        get
        {
            return loop;
        }
        set
        {
            loop = value;
        }
    }
}
