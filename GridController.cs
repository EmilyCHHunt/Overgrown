using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles the grid and any interactions between other classes and the grid
public class GridController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] plants; //List of current plants
    [SerializeField] private GameObject origin;
    [SerializeField] int x, y;

    private GameController gameController;
    private Grid grid;
    private PlayerMovement playerScript;
    private InventoryManagement invScript;

    void Start()
    {
        gameController = FindAnyObjectByType<GameController>();
        playerScript = player.GetComponent<PlayerMovement>();
        invScript = player.GetComponent<InventoryManagement>(); 
        grid = new Grid(x,y,1 ,origin.transform.position);
    }
    //Checks for player interactions and reacts accordingly
    private void Update()
    {
        if (playerScript.getInteracting())
        {
            Vector2 playerPos = player.GetComponent<Rigidbody2D>().position;
            if (grid.checkBound(playerPos)) {
                if (grid.GetValue(playerPos) == default)
                {
                    //Checks what plant the player has selected
                    gameController.OnPlayerPlant();
                    GameObject newFlower = Instantiate(plants[playerScript.getCurPlant()]);
                    grid.assignGameobject(playerPos, newFlower);
                    grid.SetValue(playerPos, new Tile(newFlower, false));
                }
            }
            else if(playerScript.playerCanSell())
            {
                Consumer consumer = playerScript.GetConsumer();
                if (consumer.itemWanted(invScript.getItem()))
                {
                    gameController.OnPlayerSell();
                    invScript.removeItem();
                }
            }else if (playerScript.playerCanBin())
            {
                gameController.increaseScore(50);
                invScript.removeItem();
            }
        }else if (playerScript.getDestroying())
        {
            Vector2 playerPos = player.GetComponent<Rigidbody2D>().position;
            GameObject gridValue = grid.GetValue(playerPos);
            if (gridValue != default)
            {
                if (gridValue.GetComponent<GenericPlant>().isHarvestable())
                {
                    if (invScript.addItem(grid.GetValue(playerPos)) )
                    {
                        Destroy(gridValue);
                        grid.getTile(playerPos).destroyObjectIntTile();
                    }
                }else if (gridValue.GetComponent<GenericPlant>().isDead())
                {
                    Destroy(gridValue);
                    grid.getTile(playerPos).destroyObjectIntTile();
                }

            }
        }
    }
    public void speedGrow()
    {
        for (int i = 0; i < y; i++)
        {
            for(int j = 0; j < x; j++)
            {
                if (grid.GetValue(j, i)!=null){
                    if (!grid.GetValue(j, i).GetComponent<GenericPlant>().isDead())
                    {
                        grid.GetValue(j, i).GetComponent<GenericPlant>().fertalise();
                    }
                }
            }
        }
    }
    public void removeDead()
    {
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (grid.GetValue(j, i) != null)
                {
                    if (grid.GetValue(j, i).GetComponent<GenericPlant>().isDead())
                    {
                        Destroy(grid.GetValue(j, i));
                        grid.getTile(j, i).destroyObjectIntTile();
                    }
                }
            }
        }
    }
    public bool checkBounds(Vector2 worldPos){
        return grid.checkBound(worldPos);
    }
}
