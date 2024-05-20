using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceQueue{
    public Queue<GameObject> Queue = new Queue<GameObject>();
    public string Tag;
    public string State;
    public WorldStates WorldStates;

    public ResourceQueue(string tag, string state, WorldStates worldStates){
        Tag = tag;
        State = state;
        WorldStates = worldStates;
        if(Tag != ""){
            GameObject[] resources = GameObject.FindGameObjectsWithTag(Tag);
            foreach(GameObject resource in resources){
                Queue.Enqueue(resource);
            }
        }
        if(State != ""){
            WorldStates.ModifyState(State, Queue.Count);
        }
    }

    public void AddResource(GameObject resource){
        Queue.Enqueue(resource);
        WorldStates.ModifyState(State, 1);
    }
    public void RemoveResource(GameObject resource){
        Queue = new Queue<GameObject>(Queue.Where(r => r != resource));
        WorldStates.ModifyState(State, -1);
    }
    public GameObject RemoveResource(){
        if(Queue.Count <= 0){
            return null;
        }
        WorldStates.ModifyState(State, -1);
        return Queue.Dequeue();
    }
}
public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    private static Dictionary<string, ResourceQueue> resources = new Dictionary<string, ResourceQueue>();
    private static ResourceQueue colonists;
    private static ResourceQueue hurtColonists;
    private static ResourceQueue gatheredFood;
    private static ResourceQueue gatherableFood;
    private static ResourceQueue beds;
    private static ResourceQueue gatherableGold;
    private static ResourceQueue gatheredGold;
    private static ResourceQueue gatherableMedicine;
    private static ResourceQueue gatheredMedicine;

    static GWorld(){
        world = new WorldStates();
        colonists = new ResourceQueue("Colonists", "Colonists", world);
        resources.Add("colonists", colonists);
        hurtColonists = new ResourceQueue("", "", world);
        resources.Add("hurtColonists", hurtColonists);
        gatheredFood = new ResourceQueue("", "", world);
        resources.Add("gatheredFood", gatheredFood);
        gatherableFood = new ResourceQueue("GatherableFood", "GatherableFood", world);
        resources.Add("gatherableFood", gatherableFood);
        beds = new ResourceQueue("Bed", "FreeBed", world);
        resources.Add("beds", beds);
        gatherableGold = new ResourceQueue("GatherableGold", "GatherableGold", world);
        resources.Add("gatherableGold", gatherableGold);
        gatheredGold = new ResourceQueue("", "", world);
        resources.Add("gatheredGold", gatheredGold);
        gatherableMedicine = new ResourceQueue("GatherableMedicine", "GatherableMedicine", world);
        resources.Add("gatherableMedicine", gatherableMedicine);
        gatheredMedicine = new ResourceQueue("", "", world);
        resources.Add("gatheredMedicine", gatheredMedicine);

        Time.timeScale = 5;
    }

    public ResourceQueue GetQueue(string type){
        return resources[type];
    }

    private GWorld(){

    }

    public static GWorld Instance{
        get{return instance;}
    }
    public WorldStates GetWorld(){
        return world;
    }
}
