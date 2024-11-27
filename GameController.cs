using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController:MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverScoreText;
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject player;
    private int numPlanted;
    private int numSold;
    private int numOrdersComp;
    private int score;

    private AchievementManager achievementManager;
    private GridController gridController;

    private void Start()
    {
        gridController = FindAnyObjectByType<GridController>();
        achievementManager = GetComponent<AchievementManager>();
        StartCoroutine("checkAchievements");
    }
    IEnumerator checkAchievements()
    {
        for (; ; )
        {
            achievementManager.checkAchievements("planted", numPlanted);
            achievementManager.checkAchievements("sold", numSold);
            achievementManager.checkAchievements("orders", numOrdersComp);
            yield return new WaitForSeconds(1);
        }
    }

    public void updatePlayerHealth(int health)
    {
        player.GetComponent<Health>().setHearts(health);
    }
    public void OnPlayerDeath()
    {
        GameOverScreen.SetActive(true);
        gameOverScoreText.text = "Final Score:\n" + score;
        FindAnyObjectByType<HighScoreManager>().AddHighScore(score);
        Time.timeScale = 0;
    }
    public void OnPlayerPlant()
    {
        numPlanted++;
    }
    public void OnPlayerSell()
    {
        numSold++;
    }
    public void OnOrderComplete()
    {
        numOrdersComp++;
    }
    public void increaseScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void MainMenuButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
