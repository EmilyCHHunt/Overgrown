using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private List<Highscore> highscores = new List<Highscore>();
    [SerializeField] private int maxScores = 9;
    [SerializeField] private string filename;

    [SerializeField] private string playerName;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadHighScores();
        AddHighScore(12);
    }
    private void LoadHighScores()
    {
        highscores = FileHandler.ReadListFromJSON<Highscore>(filename);

        while (highscores.Count > maxScores) {
            highscores.RemoveAt(maxScores);
        }
    }
    private void SaveHighScores()
    {
        Debug.Log("saving");
        FileHandler.SaveToJSON<Highscore>(highscores,filename);
    }
    public void AddHighScore(int newScore)
    {
        Debug.Log("saving!");
        Highscore newHighscore = new Highscore(playerName, newScore);
        for (int i = 0; i < maxScores; i++)
        {
            if (i >= highscores.Count|| highscores[i].score <= newHighscore.score)
            {
                highscores.Insert(i,newHighscore);

                while (highscores.Count > maxScores)
                {
                    highscores.RemoveAt(maxScores);
                }
                SaveHighScores();
                break;
            }
        }
    }
    public void setPlayerName(string playerName)
    {
        this.playerName = playerName;
    }
    public List<Highscore> getHighscores()
    {
        return highscores;
    }
}
