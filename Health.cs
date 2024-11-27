using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Hearts heartBar;
    private int hearts;

    // Start is called before the first frame update
    void Start()
    {
        hearts = 5;
        heartBar.setHearts(hearts);
    }

    public void reduceHearts()
    {
        hearts--;
        heartBar.reduceHearts();
        checkDeath();
    }
    public void increaseHearts()
    {
        if (hearts < 5)
        {
            hearts++;
            heartBar.increaseHearts();
        }
    }
    public void setHearts(int newHearts)
    {
        hearts = newHearts;
        heartBar.setHearts(hearts);
        checkDeath();
    }
    void checkDeath()
    {
        if (hearts <= 0) 
        {
            FindAnyObjectByType<GameController>().OnPlayerDeath();
        }
    }
}
