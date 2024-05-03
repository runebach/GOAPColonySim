using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherFood : GAction
{

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
        // AgentBeliefs.ModifyState("HasFood", 1);
        Inventory.RemoveItem(Target);
        Destroy(Target);
        return true;
    }


}
