using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    [SerializeField] private Toolbar toolbarController;
    [SerializeField] private int  toolBarLength;

    private int toolBarSelected;
    [SerializeField] private GameObject[] inventory;
    private void Start()
    {
        toolbarController.selectToolbar(toolBarSelected);
        inventory = new GameObject[toolBarLength];
    }

    public bool removeItem()
    {
        if (inventory[toolBarSelected] == null)
        {
            return false;
        }
        toolbarController.removeTool(toolBarSelected);
        inventory[toolBarSelected] = null;
        return true;
    }
    public bool addItem(GameObject item)
    {
        if (inventory[toolBarSelected] != null)
        {
            return false;
        }
        GameObject item_copy = Instantiate(item);
        item_copy.GetComponentInChildren<SpriteRenderer>().enabled = false;
        item_copy.transform.transform.localScale = new Vector3(0,0,-15);
        toolbarController.setToolbar(item_copy, toolBarSelected);
        inventory[toolBarSelected] = item_copy;
        return true;
    }
    public void setToolBar(int x)
    {
        toolBarSelected = x;
        toolbarController.selectToolbar(x);
    }
    public GameObject getItem()
    {
        return inventory[toolBarSelected];
    }
}
