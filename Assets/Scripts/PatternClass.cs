
public class PatternClass
{
    private bool isComplete = false;

    public virtual void Start() {}
    
    public virtual void Update() {}

    public bool IsComplete
    {
        get
        {
            return isComplete;
        }
    }
}
