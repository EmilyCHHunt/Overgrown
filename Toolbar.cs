using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class Toolbar : MonoBehaviour
{
    private GameObject[] toolbar;
    private int selectedBox;
    private void Start()
    {
        toolbar = new GameObject[transform.childCount];
    }


    public bool setToolbar(GameObject plant, int i)
    {
        if (toolbar[i] == null)
        {
            Sprite sprite = plant.GetComponentInChildren<SpriteRenderer>().sprite;
            toolbar[i] = plant;
            transform.GetChild(i).GetChild(1).GetComponent<Image>().enabled = true;
            transform.GetChild(i).GetChild(1).GetComponent<Image>().sprite = sprite;
            transform.GetChild(i).GetChild(1).GetComponent<Image>().preserveAspect = true;
            return true;
        }
        return false;
    }
    public void selectToolbar(int i)
    {
        transform.GetChild(selectedBox).GetComponent<Image>().color = 
                    new Color(0.754717f, 0.754717f, 0.754717f);
        selectedBox = i;
        transform.GetChild(selectedBox).GetComponent<Image>().color = Color.black;
    }
    public bool removeTool(int i)
    {
        if (toolbar[i] == null)
        {
            return false;
        }
        toolbar[i] = null;
        transform.GetChild(i).GetChild(1).GetComponent<Image>().enabled = false;
        return true;
    }
    public GameObject getItem(int i)
    {
        return toolbar[i];
    }
}
