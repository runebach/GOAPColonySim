using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetAlly : GAction
{
    GameObject resource;
    

    public override bool PrePerform()
    {
        Target = GWorld.Instance.GetQueue("hurtColonists").RemoveResource();
        if(Target == null){
            return false;
        }
        resource = GWorld.Instance.GetQueue("beds").RemoveResource();
        if(resource != null){
            Inventory.AddItem(resource);
        }
        else{
            GWorld.Instance.GetQueue("colonists").AddResource(Target);
            Target = null;
            return false;
        }
        return true;
    }
    public override bool PostPerform()
    {
        if(Target != null){
            Target.GetComponent<GAgent>().Inventory.AddItem(resource);
        }
        return true;
    }


}
