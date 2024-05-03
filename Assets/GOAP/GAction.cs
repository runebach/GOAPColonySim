using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    public string ActionName = "action";
    public float Cost = 1;
    public GameObject Target;
    public string TargetTag;
    public float Duration = 0;
    public NavMeshAgent Agent;
    public WorldState[] preConditions;
    public WorldState[] postConditions;
    public Dictionary<string, int> PreConditions;
    public Dictionary<string, int> PostConditions;
    public WorldStates AgentBeliefs;
    public bool Running;
    public GInventory Inventory;

    public GAction(){
        PreConditions = new Dictionary<string, int>();
        PostConditions = new Dictionary<string, int>();
    }

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        if(preConditions != null){
            foreach(WorldState ws in preConditions){
                PreConditions.Add(ws.key, ws.value);
            }
        }
        if(postConditions != null){
            foreach(WorldState ws in postConditions){
                PostConditions.Add(ws.key, ws.value);
            }
        }
        Inventory = GetComponent<GAgent>().Inventory;
        AgentBeliefs = GetComponent<GAgent>().Beliefs;
    }

    public bool IsAchievable(){
        return true;
    }
    public bool IsAchievableGiven(Dictionary<string, int> conditions){
        foreach(KeyValuePair<string, int> p in PreConditions){
            if(!conditions.ContainsKey(p.Key)){
                return false;
            }
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
