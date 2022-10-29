
public class Hint
{
    private string text;
    private int id;

    public Hint(string text, int id)
    {
        this.text = text;
        this.id = id;
    }

    public string GetText()
    {
        return text;
    }

    public int GetId()
    {
        return id;
    }
}
