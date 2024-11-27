using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//Handles Player Inputs
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 5;
    [SerializeField] private Toolbar plantbarController;

    private Vector2 movement;
    private Rigidbody2D rb;
    private bool interact = false;
    private bool destroying = false;
    private bool canSell, canBin;
    private int plantsNum;
    private int curPlant;
    private int curToolSlot;
    private int selectedToolbar;
    private InventoryManagement inventoryManagement;
    private Consumer consumer;
    private float waitTime;

    private void Awake()
    {
        plantsNum =5;
        curPlant = 0;
        rb = GetComponent<Rigidbody2D>();
        inventoryManagement = GetComponent<InventoryManagement>();
        plantbarController.selectToolbar(curPlant);
    }
    private void OnMovement(InputValue value) {
        movement = value.Get<Vector2>();
    }   
    private void OnInteract(InputValue value)
    {
        interact = value.isPressed;
    }
    private void OnDestroying(InputValue value)
    {
        destroying = value.isPressed;
    }
    //Cycles through the plant index temporary feature testing
    private void OnPlantCycle(InputValue value)
    {
        if (curPlant+1 >= plantsNum)
        {
            curPlant = 0;
        }
        else
        {
            curPlant++;
        }
        plantbarController.selectToolbar(curPlant);
    }
    private void OnToolbarCycle(InputValue value)
    {
        if (curToolSlot + 1 >= plantsNum)
        {
            curToolSlot = 0;
        }
        else
        {
            curToolSlot++;
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * speed);

        if (Time.time > waitTime)
        {
            speed = 3;
        }
    }
    public bool getInteracting()
    {
        return interact;
    }
    public bool getDestroying(){
        return destroying;
    }
    public int getCurPlant()
    {
        return curPlant;
    }
    private void OnSelectOne()
    {
        inventoryManagement.setToolBar(0);
    }
    private void OnSelectTwo()
    {
        inventoryManagement.setToolBar(1);
    }
    private void OnSelectThree()
    {
        inventoryManagement.setToolBar(2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Consumer":
                canSell = true;
                consumer = collision.GetComponent<Consumer>();
                break;
            case "Item":
                Debug.Log(collision.GetComponent<Item>().getItemName());
                switch (collision.GetComponent<Item>().getItemName())
                {
                    case "coffee":
                        Debug.Log("coffee");
                        speed = 5;
                        waitTime = Time.time + 10f;
                        break;
                    case "fertiliser":
                        Debug.Log("fertal");
                        FindAnyObjectByType<GridController>().speedGrow();
                        break;
                    case "scythe":
                        Debug.Log("scythe");
                        FindAnyObjectByType<GridController>().removeDead();
                        break;

                }
                Destroy(collision.gameObject);
                break;
            case "Bin":
                canBin = true;
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Consumer":
                canSell = false;
                consumer = null;
                break;
            case "Bin":
                canBin = false;
                break;
        }

    }
    public bool playerCanSell()
    {
        return canSell;
    }
    public bool playerCanBin()
    {
        if (inventoryManagement.getItem() != null)
        {
            return canBin;
        }
        return false;
    }
    public Consumer GetConsumer()
    {
        return consumer;
    }
}
