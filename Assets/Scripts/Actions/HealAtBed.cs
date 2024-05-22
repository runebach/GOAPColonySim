using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealAtBed : GAction
{

    GAgent gAgent;
    

    public override bool PrePerform()
    {
        Target = GWorld.Instance.GetQueue("hurtColonists").RemoveResource();
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
        GWorld.Instance.GetWorld().ModifyState("gatheredMedicine", -1);
        gAgent = Target.GetComponent<GAgent>();
        gAgent.gStateMonitors.FirstOrDefault(x => x.Name == "Health").UpdateStateStrength(100);
        return true;
    }


}
