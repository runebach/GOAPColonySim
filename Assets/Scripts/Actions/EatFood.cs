using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EatFood : GAction
{
    private GAgent agent;
    private GStateManager hunger;
    public float restoredHunger = 60;


    public override bool PrePerform()
    {
        agent = GetComponent<GAgent>();
        hunger = agent.gStateMonitors.FirstOrDefault(x => x.GetType() == typeof(Hunger));
        GWorld.Instance.GetWorld().ModifyState(StaticStates.GatheredFood, -1);
        return true;
    }
    public override bool PostPerform()
    {
        hunger.UpdateStateStrength(restoredHunger);
        return true;
    }


}
