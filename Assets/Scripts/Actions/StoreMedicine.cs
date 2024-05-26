using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreMedicine : GAction
{
    
    

    public override bool PrePerform()
    {
        return true;
    }
    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState(StaticStates.GatheredMedicine, 1);
        return true;
    }


}
