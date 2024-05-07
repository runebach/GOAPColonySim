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
    GAgent gAgent;


    void Start(){
        navMeshPath = new NavMeshPath();
        gAgent = GetComponent<GAgent>();
    }
    public override bool PrePerform()
    {
        Vector3 randomPosition = gameObject.transform.position + Random.insideUnitSphere * idleDistance;
        Vector3 destination;

        NavMeshHit hit;
        if(!NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas)){
            return false;
        }
        destination = hit.position;

        Agent.CalculatePath(destination, navMeshPath);
        if(navMeshPath.status != NavMeshPathStatus.PathComplete){
            return false;
        }
            
        waypoint = new GameObject("Waypoint");
        waypoint.transform.position = destination;
        Target = waypoint;
        
        return true;
    }
    public override bool PostPerform()
    {
        Destroy(waypoint);
        return true;
    }



}
