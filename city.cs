using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class city : MonoBehaviour
{
    public int money;
    public int day;
    public int curPopulation;
    public int curJobs;
    public int curFood;
    public int maxPopulation;
    public int maxJobs;
    public int incomePerJobs;
    public TextMeshProUGUI startsText;
    public List<building> buildings = new List<building>();

    public static city instance;

    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        UpdateStatText();
    }
    public void OnPLaceBuilding(building building)
    {
        money -= building.preset.cost;
        maxPopulation += building.preset.population;
        maxJobs += building.preset.jobs;
        buildings.Add(building);
        UpdateStatText();
    }
    public void OnRemoveBuilding(building building)
    {
        
        maxPopulation -= building.preset.population;
        maxJobs -= building.preset.jobs;
        buildings.Remove(building);
        Destroy(building.gameObject);
        UpdateStatText();
    }
    private void UpdateStatText()
    {
        startsText.text = string.Format("Day:{0} Money: {1} Pop: {2}/{3} Jobs:{4} Food:{6}", new object[7] { day, money, curPopulation, maxPopulation, curJobs, maxJobs, curFood });
    }
    void CalculateMoney()
    {
        money += curJobs * incomePerJobs;
        foreach (building building in buildings)
            money -= building.preset.custPerTurn;
    }
    void CalculatePopulation()
    {
        if (curFood >= curPopulation && curPopulation < maxPopulation)
        {
            curFood -= curPopulation / 4;
            curPopulation = Mathf.Min(curPopulation + curFood / 4,maxPopulation);
        }
        else if (curFood < curPopulation)
        {
            curPopulation = curFood;
        }
    }
    void CalculateJobs()
    {
        curJobs = Mathf.Min(curPopulation, maxJobs);
    }
    void CalculateFoods()
    {
        curFood = 0;
        foreach (building building in buildings)
            curFood += building.preset.food;
    }
    public void EndTurn()
    {
        day++;
        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFoods();
        UpdateStatText();
    }
}
