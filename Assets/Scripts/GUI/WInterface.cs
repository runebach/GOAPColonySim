using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class WInterface : MonoBehaviour
{
    private GameObject focusObject;
    private ResourceData focusObjectData;
    private GameObject newResourcePrefab;
    private Vector3 goalPos;
    private Vector3 clickOffset = Vector3.zero;
    private bool offSetCalc = false;
    private bool deleteResource = false;
    public GameObject[] AllResources;
    public GameObject CollisionParent;
    public NavMeshSurface Surface;

    // Det her lort skal REFAKTORERES
    public void SelectBed(){
        if(GWorld.Instance.GetQueue("gatheredGold").Queue.Count >= 3){
            newResourcePrefab = AllResources[0];
            for(int i = 1; i < 3; i++){
                GWorld.Instance.GetQueue("gatheredGold").RemoveResource();
            }
        }
        Debug.Log("SelectBed(). newResourcePrefab: " + newResourcePrefab != null);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

            
            
            Debug.Log(newResourcePrefab);
            if(r != null){
                focusObject = hit.transform.gameObject;
                focusObjectData = r.Info;
            }
            else if(newResourcePrefab != null){
                Debug.Log("Instantiate");
                goalPos = hit.point;
                focusObject = Instantiate(newResourcePrefab, goalPos, newResourcePrefab.transform.rotation);
                focusObjectData = focusObject.GetComponent<Resource>().Info;  
            }
            
            if(focusObject){
                focusObject.GetComponent<Collider>().enabled = false;
            }
            
            
        }

        else if(focusObject && Input.GetMouseButtonUp(0)){
            if(deleteResource){
                GWorld.Instance.GetQueue(focusObjectData.ResourceQueue).RemoveResource(focusObject);
                GWorld.Instance.GetWorld().ModifyState(focusObjectData.ResourceState, -1);
                Destroy(focusObject);
            }
            else{
                focusObject.transform.parent = CollisionParent.transform;
                GWorld.Instance.GetQueue(focusObjectData.ResourceQueue).AddResource(focusObject);
                GWorld.Instance.GetWorld().ModifyState(focusObjectData.ResourceState, 1);
                focusObject.GetComponent<Collider>().enabled = true;
            }
            Surface.BuildNavMesh();
            focusObject = null;
        }
        
        else if(focusObject && Input.GetMouseButton(0)){
            
            
            int layerMask = 1 << 8;
            RaycastHit hitMove;
            Ray rayMove = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(!Physics.Raycast(rayMove, out hitMove, Mathf.Infinity, layerMask)){
                return;
            }

            
            if(!offSetCalc){
                clickOffset = hitMove.point - focusObject.transform.position;
                offSetCalc = true;
            }
            goalPos = hitMove.point - clickOffset;
            NavMeshHit hit;
            if(NavMesh.SamplePosition(goalPos, out hit, 10.0f,NavMesh.AllAreas)){
                goalPos = hit.position;
            }

            
            focusObject.transform.position = goalPos;

        }

        if(focusObject && Input.GetKeyDown(KeyCode.R)){
            focusObject.transform.Rotate(0, 90, 0);
        }
        if(focusObject && Input.GetKeyDown(KeyCode.E)){
            focusObject.transform.Rotate(0, -90, 0);
        }

    }
    


}
