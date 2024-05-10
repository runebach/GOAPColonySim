using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGold : GAction
{

    public override bool PrePerform()
    {
        return true;
    }
    public override bool PostPerform()
    {
        Debug.Log("gold");
        GWorld.Instance.GetWorld().ModifyState("GatheredGold", 1);
        return true;
    }


}
