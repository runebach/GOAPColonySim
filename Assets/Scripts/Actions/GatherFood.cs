using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GatherFood : GAction
{
    GameObject navMeshSurfaceObject;
    NavMeshSurface navMeshSurface;

    void Start(){
        navMeshSurfaceObject = GameObject.FindGameObjectWithTag("NavMeshSurface");
        navMeshSurface = navMeshSurfaceObject.GetComponent<NavMeshSurface>();
    }

    public override bool PrePerform()
    {
        Target = GWorld.Instance.GetQueue("gatherableFood").RemoveResource();
        if(Target == null){
            return false;
        }
        Inventory.AddItem(Target);
        return true;
    }
    public override bool PostPerform()
    {
        Inventory.RemoveItem(Target);
        Destroy(Target);
        StartCoroutine(BuildNavMesh());

        return true;
    }

    public IEnumerator BuildNavMesh(){
        yield return new WaitForSeconds(0.1f);
        navMeshSurface.BuildNavMesh();
    }
    //Most scuffed solution there is



}
