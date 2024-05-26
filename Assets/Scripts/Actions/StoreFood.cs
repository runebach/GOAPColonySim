using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreFood : GAction
{

    public override bool PrePerform()
    {
        return true;
    }
    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState(StaticStates.GatheredFood, 1);
        return true;
    }


}
