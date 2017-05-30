using System.Collections.Generic;

public abstract class PatternClass
{
    private bool isRunning = false;
    private bool isComplete = false;

    public virtual void Start()
    {
        isRunning = true;
        isComplete = false;
    }
    
    public virtual void Update() {}

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
        if(!allPatternPass)
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
                        allPatternPass = true;
                    }
                }
            }
        }
    }

    public PatternClass GetCurPattern()
    {
        return patterns[curPattern];
    }

    public bool AllPatternPass
    {
        get
        {
            return allPatternPass;
        }
    }
}
