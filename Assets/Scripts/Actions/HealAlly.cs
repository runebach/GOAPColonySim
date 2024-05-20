using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealAlly : GAction
{
    


    public override bool PrePerform()
    {
        Target = Inventory.FindItemWithTag("Bed");
        if(Target == null){
            return false;
        }
        
        return true;
    }
    public override bool PostPerform()
    {
        GWorld.Instance.GetQueue("beds").AddResource(Target);
        Inventory.RemoveItem(Target);
        
        return true;
    }


}
