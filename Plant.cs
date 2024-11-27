using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "ScriptableObjects/Plant", order=1)]
public class Plant : ScriptableObject
{
    public Plant parent;
    public string cropName;
    public float growthTime;
    public float growthRateMult;
    public float timeInterval;
    public float overRipenDeathPerc;

    private void OnEnable()
    {
        if (parent == null) return;
        if (cropName == "") cropName = parent.cropName;
        if (growthTime == 0f) growthTime = parent.growthTime;
        if (growthRateMult == 0f) growthRateMult = parent.growthRateMult;
        if (timeInterval == 0f) timeInterval = parent.timeInterval;
        if (overRipenDeathPerc == 0f) overRipenDeathPerc = parent.overRipenDeathPerc;
    }
}
