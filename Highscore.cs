using System;

[Serializable]
public class Highscore
{
    public string playerName;
    public int score;

    public Highscore(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score; 
    }
}
