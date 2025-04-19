using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="building preset",menuName ="new building preset")]
public class buildingpreset : ScriptableObject
{
    public int cost;
    public int custPerTurn;
    public GameObject prefab;

    public int population;
    public int jobs;
    public int food;

}
