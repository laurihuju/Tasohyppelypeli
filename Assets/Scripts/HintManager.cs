using System.Collections;

public class HintManager
{
    private ArrayList hints;
    private int nextHintId;

    public HintManager()
    {
        hints = new ArrayList();
        nextHintId = 0;
    }

    public int AddHint(string text)
    {
        Hint hint = new Hint(text, nextHintId);

        hints.Add(hint);
        
        nextHintId++;

        return nextHintId - 1;
    }

    public void RemoveHint(int hintId)
    {
        foreach(Hint hint in hints)
        {
            if (hint.GetId() == hintId)
            {
                hints.Remove(hint);
                return;
            }
        }
    }

    public bool HasAnyHints()
    {
        if(hints.Count == 0)
        {
            return false;
        }

        return true;
    }

    public override string ToString()
    {
        string s = "";
        for(int i = 0; i < hints.Count; i++)
        {
            s += ((Hint) hints[i]).GetText();
            if(i != hints.Count - 1)
            {
                s += "\n";
            }
        }

        return s;
    }
}
