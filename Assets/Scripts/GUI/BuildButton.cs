using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public int index;
    public int cost;
    public WorldInterface worldInterface;
    public void Start(){
        worldInterface = GameObject.FindGameObjectWithTag("WInterface").GetComponent<WorldInterface>();
    }

    public void BuildObject(){
        worldInterface.SelectBuildItem(index, cost);
    }
}
