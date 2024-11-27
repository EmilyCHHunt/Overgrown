using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    private Queue<Achievement> achievementsQueue = new Queue<Achievement>();

    [SerializeField] private TextMeshProUGUI achievementText;
    [SerializeField] private TextMeshProUGUI achievementDescriptionText;
    [SerializeField] private GameObject background;
    [SerializeField] private Achievement[] achievements;

    private void Start()
    {
        background.SetActive(false);
        achievementText.text = string.Empty;
        achievementDescriptionText.text = string.Empty;

        StartCoroutine("CheckQueue");
    }
    public void checkAchievements(string achType, int progress)
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            if (achievements[i].type.Equals(achType) && achievements[i].desiredNum <= progress && !achievements[i].complete)
            {
                achievements[i].complete = true;
                achievementComplete(achievements[i].id);
            }
        }
    }
    public void achievementComplete(int id)
    {
        achievementsQueue.Enqueue(getAchievement(id));
    }
    public void completeAchievement(Achievement ach)
    {
        ach.complete = true;
        background.SetActive(true);
        achievementText.text = ach.achName;
        achievementDescriptionText.text = ach.description;
    }
    private Achievement getAchievement(int id)
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            if (achievements[i].id == id)
            {
                return achievements[i];
            }
        }
        return null;
    }
    IEnumerator CheckQueue()
    {
        for (; ; )
        {
            if (achievementsQueue.Count > 0)
            {
                completeAchievement(achievementsQueue.Dequeue());
                yield return new WaitForSeconds(4);
                background.SetActive(false);
                achievementText.text = string.Empty;
                achievementDescriptionText.text = string.Empty;
            }
            yield return new WaitForSeconds(1);

        }
    }
}
