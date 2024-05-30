using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SubGoal{
    public Dictionary<string, int> SubGoals;
    public bool Removable;
    public bool IsDefault;
    public string Name;
    public SubGoal(string goalName, int goalValue, bool removable, bool isDefault){
        SubGoals = new Dictionary<string, int>();
        SubGoals.Add(goalName, goalValue);
        Removable = removable;
        IsDefault = isDefault;
        Name = goalName;
    }
}
public class GAgent : MonoBehaviour
{
    public List<GAction> Actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    public GInventory Inventory = new GInventory();
    public WorldStates Beliefs = new WorldStates();
    public List<GStateManager> gStateMonitors = new List<GStateManager>();
    public GAction CurrentAction;
    public float actionDistance = 2.0f;
    private GPlanner planner;
    private Queue<GAction> actionQueue;
    public SubGoal currentGoal;
    private Vector3 destination = Vector3.zero;
    private bool invoked = false;
    // Start is called before the first frame update
    public void Awake()
    {
        GStateManager[] states = GetComponents<GStateManager>();
        foreach(GStateManager state in states){
            gStateMonitors.Add(state);
        }
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
        // Invokes CompleteAction method if close enough to target
        if(CurrentAction != null && CurrentAction.Running){
            float distanceToTarget = Vector3.Distance(destination, transform.position);
            if(distanceToTarget < actionDistance){
                if(!invoked){
                    Invoke("CompleteAction", CurrentAction.Duration);
                    invoked = true;
                }
            }
            return;
        }

        // If agent has no plan, begin planning new plan
        if(planner == null || actionQueue == null){
            planner = new GPlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;
            foreach(KeyValuePair<SubGoal, int> sg in sortedGoals){
                actionQueue = planner.Plan(Actions, sg.Key.SubGoals, Beliefs);
                if(actionQueue != null){
                    currentGoal = sg.Key;
                    break;
                }
            }
        }


        //If no more actions in plan, wipe the plan and ready for new plan
        if(actionQueue != null && actionQueue.Count == 0){
            if(currentGoal.Removable){
                goals.Remove(currentGoal);
            }
            planner = null;
            actionQueue = null;
        }

        // if still more actions, find next action in queue and do the stuff.
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



    // Is supposed to check for new plans. If there is found a new most important and possible plan that is different
    // than the current one, set the planner object to null.
    // void ReplaceOldPlan(){
    //     if(planner != null){
    //         GPlanner tPlanner = new GPlanner();
    //         Debug.Log(currentGoal.SubGoals.First().Key);
    //         var sortedGoals = from entry in goals orderby entry.Value descending select entry;

    //         foreach(KeyValuePair<SubGoal, int> sg in sortedGoals){
    //             actionQueue = tPlanner.Plan(Actions, sg.Key.SubGoals, Beliefs);
    //             if(actionQueue != null && sg.Value > currentGoal.SubGoals.First().Value  && currentGoal.IsDefault == false){
    //                 planner = null;
    //                 actionQueue = null;
    //             }
    //         }
    //     }
        
        
    // }

    // private IEnumerator DoPlanning(){

    //     while(true){
    //         yield return new WaitForSeconds(1);
    //         if(planner == null){
    //             planner = new GPlanner();
    //         }
            
    //         var sortedGoals = from entry in goals orderby entry.Value descending select entry;
    //         foreach(KeyValuePair<SubGoal, int> sg in sortedGoals){
    //             Queue<GAction> tempQueue = planner.Plan(Actions, sg.Key.SubGoals, Beliefs);
    //             SubGoal tempGoal;
    //             if(tempQueue != null){
    //                 tempGoal = sg.Key;
    //                 if(tempGoal != currentGoal && tempGoal.IsDefault == false){
    //                     currentGoal = tempGoal;
    //                     actionQueue = tempQueue;
    //                 }
    //             }
    //         }

    //     }
    // }
}
