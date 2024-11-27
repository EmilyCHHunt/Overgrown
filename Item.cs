using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;

    private void Start()
    {
        Destroy(gameObject,20);
    }

    public string getItemName()
    {
        return itemName;
    }
}
