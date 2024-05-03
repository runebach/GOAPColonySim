using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : GAction
{
    float idleDistance = 10;
    Vector3 destination;
    GameObject waypoint;
    NavMeshPath navMeshPath;

    void Start(){
        navMeshPath = new NavMeshPath();
    }
    public override bool PrePerform()
    {
        destination = transform.position + new Vector3(Random.Range(-idleDistance, idleDistance), transform.position.y, Random.Range(-idleDistance, idleDistance));
        waypoint = new GameObject("Waypoint");
        waypoint.transform.position = destination;
        if(!Agent.CalculatePath(waypoint.transform.position, navMeshPath)){
            Destroy(waypoint);
            return false;
        }
        Target = waypoint;
        
        
        return true;
    }
    public override bool PostPerform()
    {
        Destroy(waypoint);
        return true;
    }



}
