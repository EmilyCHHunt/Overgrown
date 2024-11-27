using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private GameObject highscoreUI;
    [SerializeField] private Transform highscoreParent;

    private List<GameObject> highscoreListUI = new List<GameObject>();

    public void UpdateUI(List<Highscore> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Highscore highscore = list[i];

            if (highscore.score > 0) { 
                if(i >= highscoreListUI.Count)
                {
                    var inst = Instantiate(highscoreUI,Vector3.zero, 
                            Quaternion.identity, highscoreParent);

                    highscoreListUI.Add(inst);
                }

                var texts = highscoreListUI[i].GetComponentsInChildren<TextMeshProUGUI>();

                texts[0].text = highscore.playerName;
                texts[1].text = ""+highscore.score;
            }
        }
    }
}
