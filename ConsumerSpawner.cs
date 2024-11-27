using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerSpawner : MonoBehaviour
{
    [SerializeField] GameObject consumerPrefab;
    [SerializeField] GameObject queueParent;
    [SerializeField] private float spawnRate;
    [SerializeField] private Health playerHealth;

    private List<GameObject> consumerList = new List<GameObject>();
    private float timeOfLastSpawn;
    private Transform[] movementPoints;
    private Transform[] queuePoints;
    private bool[] queuePlaces;
    private Transform[] endPoints;

    private void Start()
    {
        
        timeOfLastSpawn = Time.time;
        movementPoints = new Transform[transform.childCount];

        for(int i = 0; i < movementPoints.Length; i++)
        {
            movementPoints[i] = transform.GetChild(i);
        }
       
        queuePoints = new Transform[queueParent.transform.childCount];
        queuePlaces = new bool[queuePoints.Length];
        for (int i = 0; i < queuePoints.Length; i++)
        {
            queuePoints[i] = queueParent.transform.GetChild(i);
            queuePlaces[i] = true;
        }
        endPoints = new Transform[2];
        endPoints[0] = movementPoints[3];
        endPoints[1] = movementPoints[4];
    }

    private void FixedUpdate()
    {
        if (timeOfLastSpawn + spawnRate < Time.time)
        {
            GameObject newCustomer = Instantiate(consumerPrefab,
                   movementPoints[0].position, movementPoints[0].rotation, transform);
            newCustomer.GetComponent<enemyMovement>().setTargetPos(movementPoints[1]);
            newCustomer.GetComponent<enemyMovement>().setTargetPos(movementPoints[2]);
            consumerList.Add(newCustomer);
            timeOfLastSpawn = Time.time + spawnRate;
        }

        for (int i = 0;i < queuePlaces.Length;i++)
        {
            if (queuePlaces[i] && consumerList.Count > 0)
            {
                consumerList[0].GetComponent<enemyMovement>().joinqueue(i, 
                    queuePoints[i], endPoints);
                removeConsumer(consumerList[0]);
                queuePlaces[i] = false;
            }       
        }
    }
    public void reducePlayerHealth()
    {
        playerHealth.reduceHearts();
    }
    public void clearSpace(int i)
    {
        queuePlaces[i] = true;
    }
    public void removeConsumer(GameObject consumer)
    {
        consumerList.Remove(consumer);
    }
}
