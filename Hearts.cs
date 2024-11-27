using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    [SerializeField] private Image[] heartImages;
    [SerializeField] private int hearts;
    void Start()
    {
        heartImages = new Image[transform.childCount];
        hearts = transform.childCount;
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i] = transform.GetChild(i).GetComponent<Image>();
        }
        updateHearts();

    }
    private void updateHearts()
    {
        if (hearts < 0)
        {
            hearts = 0;
        }else if(hearts > 5)
        {
            hearts = 5;
        }
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i <= hearts)
            {
                heartImages[i].color = Color.white;
            }
            else
            {
                heartImages[i].color = Color.black;
            }
        }
    }
    public void setHearts(int newHearts) {
        hearts = newHearts;
        updateHearts();
    }
    public void reduceHearts()
    {
        hearts--;
        updateHearts();
    }
    public void increaseHearts()
    {
        hearts++;
        updateHearts();
    }

}
