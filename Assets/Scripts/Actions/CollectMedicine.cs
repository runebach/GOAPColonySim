using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectMedicine : GAction
{


    public override bool PrePerform()
    {
        GWorld.Instance.GetWorld().ModifyState(StaticStates.GatheredMedicine, -1);
        return true;
    }
    public override bool PostPerform()
    {
        return true;
    }


}
