using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public sealed class WorldInterface : MonoBehaviour
{

    public GameObject[] AllResources;
    public GameObject NavMeshParent;
    public NavMeshSurface Surface;
    private GameObject newResourcePrefab;
    private GameObject focusObject;
    private ResourceData focusObjectData;
    private Vector3 goalPos;
    private Vector3 clickOffset = Vector3.zero;
    private bool offSetCalc = false;
    private bool deleteResource = false;
    private int costToBuild;


    private static readonly WorldInterface instance = new WorldInterface();
    static WorldInterface(){
    }
    private WorldInterface(){
    }
    public static WorldInterface Instance{
        get{return instance;}
    }

    void Update(){
        SelectObject();
        MoveObject();
        ReleaseObject();
        RotateObject();
        
    }

    public void SelectBuildItem(int index, int cost){
        if(GWorld.Instance.GetWorld().GetState(StaticStates.GatheredGold).Value >= cost || true){
            costToBuild = cost;
            newResourcePrefab = AllResources[index];
        }
    }
    private void BuildItem(){
        GWorld.Instance.GetWorld().ModifyState(StaticStates.GatheredGold, -costToBuild);
        if(GWorld.Instance.GetWorld().GetState(StaticStates.GatheredGold).Value < costToBuild || false){
            newResourcePrefab = null;
        }
    }
    public void SelectObject(){
        if(Input.GetMouseButtonDown(0)){
            if(EventSystem.current.IsPointerOverGameObject()){
                return;
            }
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(!Physics.Raycast(ray, out hit)){
                return;
            }
            offSetCalc = false;
            clickOffset = Vector3.zero;
            Resource r = hit.transform.gameObject.GetComponentInParent<Resource>();
            Debug.Log("r: " + r);
            Debug.Log("newResourcePrefab: " + newResourcePrefab);
            if(r != null){
                Debug.Log("r isnt null");
                focusObject = hit.transform.gameObject;
                focusObjectData = r.Info;
            }

            else if(newResourcePrefab != null){
                Debug.Log("Attempt to build");
                goalPos = hit.point;
                focusObject = Instantiate(newResourcePrefab, goalPos, newResourcePrefab.transform.rotation);
                focusObjectData = focusObject.GetComponent<Resource>().Info;
                BuildItem();
            }
            if(focusObject){
                focusObject.GetComponent<Collider>().enabled = false;
            }
        }
    }
    public void ReleaseObject(){
        if(focusObject && Input.GetMouseButtonUp(0)){
        if(deleteResource == true){
            GWorld.Instance.GetQueue(focusObjectData.ResourceQueue).RemoveResource(focusObject);
            Destroy(focusObject); 
        }
        else{
            focusObject.transform.parent = NavMeshParent.transform;
            GWorld.Instance.GetQueue(focusObjectData.ResourceQueue).AddResource(focusObject);
            focusObject.GetComponent<Collider>().enabled = true;
        }
        Surface.BuildNavMesh();
        focusObject = null;
        }
    }

    public void MoveObject(){
        if(focusObject && Input.GetMouseButton(0)){
            int layerMask = 1 << 8;
            RaycastHit hitMove;
            Ray rayMove = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(!Physics.Raycast(rayMove, out hitMove, Mathf.Infinity, layerMask)){
                return;
            }
            if(offSetCalc == false){
                clickOffset = hitMove.point - focusObject.transform.position;
                offSetCalc = true;
            }
            goalPos = hitMove.point - clickOffset;
            NavMeshHit hit;
            if(NavMesh.SamplePosition(goalPos, out hit, 10.0f, NavMesh.AllAreas)){
                goalPos = hit.position;
            }
            focusObject.transform.position = goalPos;
            
        }
    }

    public void RotateObject(){
        if(focusObject && Input.GetKeyDown(KeyCode.R)){
            focusObject.transform.Rotate(0, 90, 0);
        }
        if(focusObject && Input.GetKeyDown(KeyCode.E)){
            focusObject.transform.Rotate(0, -90, 0);
        }
    }
}
