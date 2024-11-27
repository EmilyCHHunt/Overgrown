using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMangager : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] int spawnRate;

    private float timeOfLastSpawn;

    private void FixedUpdate()
    {
        if (timeOfLastSpawn + spawnRate < Time.time)
        {
            int randomItem = Random.Range(0, items.Length);
            int randomLocation = Random.Range(0, transform.childCount);
            if (transform.GetChild(randomLocation).childCount == 0)
            {
                Instantiate(items[randomItem], transform.GetChild(randomLocation));
                timeOfLastSpawn = Time.time;
            }
        }
    }
}
