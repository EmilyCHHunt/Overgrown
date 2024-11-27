using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericPlant : MonoBehaviour
{
    [SerializeField] private Plant plantType;
    [SerializeField] private GameObject plantScaleObj;
    [SerializeField] private Image progressBar;
    [SerializeField] private Image dyingProgressBar;

    private SpriteRenderer plantRenderer;
    private float timePlanted;
    private bool harvestable;
    private bool dead;
    private float nextGrowth;
    private Vector3 maxScale;
    private bool fertalised;

    void Start()
    {
        harvestable = false;
        timePlanted = Time.time;
        plantRenderer =  plantScaleObj.GetComponent<SpriteRenderer>();
        maxScale = plantScaleObj.transform.localScale;
        plantScaleObj.transform.localScale = maxScale / 10f;
    }

    void Update()
    {
        if (Time.time > nextGrowth && !fertalised)
        {
            progressBar.fillAmount = (Time.time-timePlanted) / plantType.growthTime;
            Grow();
            nextGrowth = Time.time + plantType.timeInterval;
        }
    }

    private void Grow()
    {
        float growthPercentage = 0;
        if (fertalised)
        {
            growthPercentage = 1;
        }
        else
        {
            growthPercentage = (Time.time - timePlanted) / plantType.growthTime;
        }
        dyingProgressBar.fillAmount = (growthPercentage-1) / (plantType.overRipenDeathPerc-1);
        if (growthPercentage < 1) {
            IncreaseSpriteSize(growthPercentage);
        }
        else if (growthPercentage >= 1 && growthPercentage <= plantType.overRipenDeathPerc) {
            plantScaleObj.transform.localScale = maxScale;
            harvestable = true;
            if (growthPercentage >= plantType.overRipenDeathPerc / 2)
            {
                float browning = 1 - ((growthPercentage - plantType.overRipenDeathPerc / 2) / (plantType.overRipenDeathPerc / 2));
                plantRenderer.color = new Color(browning, browning, browning);
            }
        }
        else
        {
            harvestable = false;
            dead = true;
            plantRenderer.color = Color.black;
            nextGrowth += Time.time * 1000;
        }
    }

    private void IncreaseSpriteSize(float newScale)
    {
        plantScaleObj.transform.localScale = maxScale * newScale;
    }
    public bool isHarvestable()
    {
        if (dead || harvestable)
        {
            return harvestable;
        }
        else
        {
            return false;
        }
    }
    public string getname()
    {
        return plantType.cropName;
    }
    public bool isDead()
    {
        return dead;
    }
    public void fertalise()
    {
        IncreaseSpriteSize(1);
        fertalised = true;
        progressBar.fillAmount = 1;
        Grow();
    }
}
