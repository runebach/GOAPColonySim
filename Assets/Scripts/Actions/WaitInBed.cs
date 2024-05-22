using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaitInBed : GAction
{
    GameObject healer;
    

    public override bool PrePerform()
    {
        Target = GWorld.Instance.GetQueue("beds").RemoveResource();
        if(Target == null){
            Debug.Log("cant find bed");
            return false;
        }
        healer = GWorld.Instance.GetQueue("colonists").Queue.FirstOrDefault(x => x.GetComponent<GAgent>().GetType() == typeof(ColonistHealer));
        if(healer == null){
            Debug.Log("cant find healer");
            return false;
        }
        GWorld.Instance.GetWorld().ModifyState("HurtColonists", 1);
        Debug.Log("Found Bed");
        
        return true;
    }
    public override bool PostPerform()
    {
        if(Target == null){
            Debug.Log("target is null");
            return false;
        }
        Debug.Log(Target);

        return true;
    }


}
