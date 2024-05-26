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
        GWorld.Instance.GetWorld().ModifyState(StaticStates.GatheredGold, 1);
        return true;
    }


}
