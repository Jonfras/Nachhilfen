namespace Nachhilfen; 
public class Service
{
    public int Level { get; set; }
    public string Subject { get; set; }

    public override string ToString()
    {
        return $"{Subject} (Level {Level})";
    }
}