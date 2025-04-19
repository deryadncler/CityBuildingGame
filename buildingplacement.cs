using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingplacement : MonoBehaviour
{
    private bool currentlyPlacing;
    private bool currentlyDeleteing;

    private buildingpreset curBuildingPreset;

    private float indicatˆrUpdateTime = 0.05f;
    private float lastUpdateTime;
    private Vector3 cur›ndicatorPos;

    public GameObject placement›ndicator;
    public GameObject deleting›ndicator;

    public void BeginNewBuildingPlacement(buildingpreset preset)
    {
        /*if (city.instance.money < preset.cost)
        {
            return;
        }*/
        currentlyPlacing = true;
        curBuildingPreset = preset;
        placement›ndicator.SetActive(true);
    }
    void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
        placement›ndicator.SetActive(false);
    }
    public void ToggleDelete()
    {
        currentlyDeleteing = !currentlyDeleteing;
        deleting›ndicator.SetActive(currentlyDeleteing);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelBuildingPlacement();
        if (Time.time - lastUpdateTime > indicatˆrUpdateTime)
        {
            lastUpdateTime = Time.time;

            cur›ndicatorPos = selector.instance.GetCurTilePosition();


            if (currentlyPlacing)
                placement›ndicator.transform.position = cur›ndicatorPos;
            else if (currentlyDeleteing)
                deleting›ndicator.transform.position = cur›ndicatorPos;
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
        GameObject buildingobj = Instantiate(curBuildingPreset.prefab, cur›ndicatorPos, Quaternion.identity);
        city.instance.OnPLaceBuilding(buildingobj.GetComponent<building>());
        CancelBuildingPlacement();
    }
    void Bulldoze()
    {
        building buildingToDestroy = city.instance.buildings.Find(x => x.transform.position == cur›ndicatorPos);
        if (buildingToDestroy != null)
        {
            city.instance.OnRemoveBuilding(buildingToDestroy);
        }
    }

}
