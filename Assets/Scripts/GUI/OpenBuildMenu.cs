using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenBuildMenu : MonoBehaviour
{
    public GameObject BuildMenu;
    bool isActive = false;

    void Start(){
    }
    public void ToggleBuildMenu(){
        if(BuildMenu == null){
            return;
        }
        else{
            isActive = !isActive;
            BuildMenu.SetActive(isActive);
        }
    }
}
