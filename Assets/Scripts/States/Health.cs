using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : GStateManager
{

    new void Start(){
        base.Start();
    }
    
    protected override void OnStateMinimum()
    {
        if(currentStrength <= 0){
            GWorld.Instance.GetQueue(StaticQueues.Colonists).RemoveResource(gameObject);
            Destroy(gameObject);
        }

    }
}
