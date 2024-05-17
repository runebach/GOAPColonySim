using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    private WInterface wInterface;
    // Start is called before the first frame update
    void Start()
    {
        wInterface = GetComponent<WInterface>();
    }


    public void SelectColonist(){
        wInterface.SelectBuildItem(0, 5);
    }
    public void SelectBed(){
        wInterface.SelectBuildItem(1, 3);
    }
}
