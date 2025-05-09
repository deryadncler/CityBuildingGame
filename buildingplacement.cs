using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingplacement : MonoBehaviour
{
    private bool currentlyPlacing;
    private bool currentlyDeleteing;

    private buildingpreset curBuildingPreset;

    private float indicatörUpdateTime = 0.05f;
    private float lastUpdateTime;
    private Vector3 curİndicatorPos;

    public GameObject placementİndicator;
    public GameObject deletingİndicator;

    public void BeginNewBuildingPlacement(buildingpreset preset)
    {
        /*if (city.instance.money < preset.cost)
        {
            return;
        }*/
        currentlyPlacing = true;
        curBuildingPreset = preset;
        placementİndicator.SetActive(true);
    }
    void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
        placementİndicator.SetActive(false);
    }
    public void ToggleDelete()
    {
        currentlyDeleteing = !currentlyDeleteing;
        deletingİndicator.SetActive(currentlyDeleteing);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelBuildingPlacement();
        if (Time.time - lastUpdateTime > indicatörUpdateTime)
        {
            lastUpdateTime = Time.time;

            curİndicatorPos = selector.instance.GetCurTilePosition();


            if (currentlyPlacing)
                placementİndicator.transform.position = curİndicatorPos;
            else if (currentlyDeleteing)
                deletingİndicator.transform.position = curİndicatorPos;
        }
        if(Input.GetMouseButtonDown(0)&& currentlyPlacing)
        {
            PlaceBuilding();
        }
        else if(Input.GetMouseButtonDown(0)&& currentlyDeleteing)
        {
            Bulldoze();
        }
    }
    void PlaceBuilding()
    {
        GameObject buildingobj = Instantiate(curBuildingPreset.prefab, curİndicatorPos, Quaternion.identity);
        city.instance.OnPLaceBuilding(buildingobj.GetComponent<building>());
        CancelBuildingPlacement();
    }
    void Bulldoze()
    {
        building buildingToDestroy = city.instance.buildings.Find(x => x.transform.position == curİndicatorPos);
        if (buildingToDestroy != null)
        {
            city.instance.OnRemoveBuilding(buildingToDestroy);
        }
    }

}
