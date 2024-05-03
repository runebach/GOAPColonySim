using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal{
    public Dictionary<string, int> SubGoals;
    public bool Removable;
    public SubGoal(string goalName, int goalValue, bool removable){
        SubGoals = new Dictionary<string, int>();
        SubGoals.Add(goalName, goalValue);
        Removable = removable;
    }
}
public class GAgent : MonoBehaviour
{
    public List<GAction> Actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    public GInventory Inventory = new GInventory();
    public WorldStates Beliefs = new WorldStates();
    public List<GStateManager> gStateMonitors;
    public GAction CurrentAction;
    private GPlanner planner;
    private Queue<GAction> actionQueue;
    private SubGoal currentGoal;
    private Vector3 destination = Vector3.zero;
    private bool invoked = false;
    // Start is called before the first frame update
    public void Start()
    {
        gStateMonitors = GetComponents<GStateManager>().ToList();
        GAction[] acts = GetComponents<GAction>();
        foreach(GAction a in acts){
            Actions.Add(a);
        }
    }
    private void CompleteAction(){
        CurrentAction.Running = false;
        CurrentAction.PostPerform();
        invoked = false;
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if(CurrentAction != null && CurrentAction.Running){
            float distanceToTarget = Vector3.Distance(destination, transform.position);
            if(distanceToTarget < 2f){
                if(!invoked){
                    Invoke("CompleteAction", CurrentAction.Duration);
                    invoked = true;
                }
            }
            return;
        }

        if(planner == null || actionQueue == null){
            planner = new GPlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;
            foreach(KeyValuePair<SubGoal, int> sg in sortedGoals){
                actionQueue = planner.Plan(Actions, sg.Key.SubGoals, Beliefs);
                if(actionQueue != null){
                    currentGoal =sg.Key;
                    break;
                }
            }
        }

        if(actionQueue != null && actionQueue.Count == 0){
            if(currentGoal.Removable){
                goals.Remove(currentGoal);
            }
            planner = null;
        }
        if(actionQueue != null && actionQueue.Count > 0){
            CurrentAction = actionQueue.Dequeue();
            if(CurrentAction.PrePerform()){
                if(CurrentAction.Target == null && CurrentAction.TargetTag != ""){
                    CurrentAction.Target = GameObject.FindWithTag(CurrentAction.TargetTag);
                }
                if(CurrentAction.Target != null){
                    CurrentAction.Running = true;

                    destination = CurrentAction.Target.transform.position;
                    Transform dest = CurrentAction.Target.transform.Find("Destination");
                    if(dest != null){
                        destination = dest.position;
                    }

                    CurrentAction.Agent.SetDestination(destination);
                }
            }
            else{
                actionQueue = null;
            }
        }
    }
}
