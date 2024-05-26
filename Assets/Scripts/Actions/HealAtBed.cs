using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealAtBed : GAction
{

    GAgent gAgent;
    

    public override bool PrePerform()
    {
        Target = GWorld.Instance.GetQueue(StaticQueues.HurtColonists).RemoveResource();
        if(Target == null){
            return false;
        }
        if(Target == this.gameObject){
            return false;
        }
        return true;
    }
    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState(StaticStates.GatheredMedicine, -1);
        gAgent = Target.GetComponent<GAgent>();
        gAgent.gStateMonitors.FirstOrDefault(x => x.GetType() == typeof(Health)).UpdateStateStrength(100);
        return true;
    }


}
