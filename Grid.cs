using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//Class which handles and stores the grid for placing plants and calculating NPC pathfindning
//Consists of an array of tiles which correspond to a position in game
public class Grid
{
    private int width;
    private int height;
    private float tileSize;
    private Tile[,] gridArray;
    private Vector2 origin;
    public Grid(int width, int height, float tileSize, Vector2 origin)
    {
        this.width = width;
        this.height = height;
        this.tileSize = tileSize;
        this.origin = origin;
        
        gridArray = new Tile[width, height];

        //Creates an empty grid
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++) { 
                Debug.DrawLine(getWorldPos(x, y), getWorldPos(x, y + 1), Color.white, 100f);
                Debug.DrawLine(getWorldPos(x, y), getWorldPos(x + 1, y), Color.white, 100f);
                gridArray[x, y] = new Tile(default(GameObject), true);
            }
        }
    }
   
    private Vector2 getWorldPos(int x, int y)
    {
        return new Vector2(x +(tileSize * 0.5f), y + (tileSize * 0.5f)) * tileSize + origin;
    }
    private void GetXY(Vector2 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos-origin).x / tileSize);
        y = Mathf.FloorToInt((worldPos-origin).y / tileSize); 
    }
    public void SetValue(int x, int y, Tile value)
    {
        if (checkBounds(x,y))
        {
            gridArray[x, y] = value;
        }
    }
    public void SetValue(Vector2 worldPos, Tile value)
    {
        int x, y;
        GetXY(worldPos, out x, out y); 
        SetValue(x, y, value);
    }
    
    public GameObject GetValue(int x, int y) {
        if (checkBounds(x,y))
        {
            return gridArray[x,y].getObjectInTile();
        }
        else
        {
            return default(GameObject);
        }
    }
    public GameObject GetValue(Vector2 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return GetValue(x, y);
    }
    private bool checkBounds(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool checkBound(Vector2 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return checkBounds(x,y);
    }
    public void assignGameobject(Vector2 worldPos, GameObject obj) {
        int x, y;
        GetXY(worldPos, out x,out y);
        Vector2 pos = getWorldPos(x, y);
        obj.transform.position = pos;   
    }
    public Tile getTile(int x, int y)
    {
        if (checkBounds(x,y))
        {
            return gridArray[x,y];
        }
        else
        {
            return null;
        }
    }
    public Tile getTile(Vector2 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return getTile(x, y);
    }
}
//Represents each tile in the grid
// Stores a gameobject, whether a creature can walk through a tile
public class Tile
{
    private GameObject objectInTile;
    private bool walkable = true; 
    
    public Tile(GameObject obj, bool walkable)
    {
        this.walkable = walkable;
        this.objectInTile = obj;
    }
    public GameObject getObjectInTile() { 
        return objectInTile;
    }
    public void setObjectInTile(GameObject obj)
    {
        objectInTile = obj;
    }
    public void destroyObjectIntTile()
    {
        objectInTile = null;
    }
}