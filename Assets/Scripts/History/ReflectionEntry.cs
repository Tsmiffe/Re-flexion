using System;

[Serializable]
public class ReflectionEntry
{
    public string reflectionText;
    public string pros;
    public string cons;
    public string finalDecision;
    public string summaryTable;
    public string timestamp;

    public ReflectionEntry(string reflectionText, string pros, string cons, string finalDecision, string summaryTable)
    {
        this.reflectionText = reflectionText;
        this.pros = pros;
        this.cons = cons;
        this.finalDecision = finalDecision;
        this.summaryTable = summaryTable;
        this.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
