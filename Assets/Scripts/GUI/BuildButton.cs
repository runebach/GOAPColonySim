using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public int index;
    public int cost;

    public void BuildObject(){
        WorldInterface.Instance.SelectBuildItem(index, cost);
    }
}
