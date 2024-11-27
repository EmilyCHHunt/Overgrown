using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Consumer : MonoBehaviour
{
    [SerializeField] private GameObject[] flowers;
    [SerializeField] Sprite tick;
    [SerializeField] float timeUntilDead;
    [SerializeField] float speed;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject speechbubble;

    private ConsumerSpawner ConsumerSpawner;
    private int numOfItems = 3;
    [SerializeField] private string[] shoppingList;
    private enemyMovement movementScript;
    private float timeOfDeath;
    private bool satisfied = false;
    private bool dead;
    private bool sell;
    private GameController gameController;

    private void Start()
    {
        ConsumerSpawner = transform.parent.GetComponent<ConsumerSpawner>();
        movementScript = GetComponent<enemyMovement>();
        transform.GetChild(0).gameObject.SetActive(false);
        shoppingList = new string[numOfItems];
        gameController = FindAnyObjectByType<GameController>();
        for (int i = 0; i < numOfItems; i++)
        {
            int selectedFlower = Random.Range(0, flowers.Length);
            shoppingList[i] = flowers[selectedFlower].name;
            
            speechbubble.transform.GetChild(i).GetComponent<Image>().sprite =
                    flowers[selectedFlower].GetComponentInChildren<SpriteRenderer>().sprite;
        }
    }
    private void FixedUpdate()
    {
        if (sell)
        {
            progressBar.fillAmount = (timeOfDeath - Time.time) / timeUntilDead;
            if (Time.time >= timeOfDeath && !satisfied)
            {
                if (!dead)
                {
                    ConsumerSpawner.clearSpace(movementScript.getQueuePlace());
                    ConsumerSpawner.reducePlayerHealth();
                    dead = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                    Destroy(gameObject, 10);
                }
                transform.position += transform.up * Time.deltaTime * speed;
            }
        } else
        {
            progressBar.fillAmount = 1;
        }
    }
    public bool itemWanted(GameObject item)
    {
        if(item != null)
        for (int i = 0; i < shoppingList.Length; i++)
        {
            if (shoppingList[i] == item.GetComponent<GenericPlant>().getname() && !dead)
            {
                shoppingList[i] = null;
                speechbubble.transform.GetChild(i).GetComponent<Image>().sprite = tick;
                numOfItems--;
                gameController.increaseScore(50);
                if (numOfItems <= 0)
                {
                    gameController.increaseScore((int) Mathf.Round(((timeOfDeath - Time.time) / timeUntilDead)*1000f));
                    gameController.OnOrderComplete();
                    GetComponent<enemyMovement>().stopWaiting();
                    sell = false;
                    satisfied = true;
                        ConsumerSpawner.clearSpace(movementScript.getQueuePlace());
                    transform.GetChild(0).gameObject.SetActive(false);
                        Destroy(gameObject, 10);
                    }
                    return true;
            }
        }
        return false;
    }
    public void startSelling()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        sell = true;
        timeOfDeath = timeUntilDead + Time.time;
    }
    public bool isSelling()
    {
        return sell;
    }
}
