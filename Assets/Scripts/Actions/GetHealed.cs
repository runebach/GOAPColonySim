using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetHealed : GAction
{
    
    private GAgent agent;
    private GStateManager health;
    public float healthRestored = 100;

    public override bool PrePerform()
    {
        agent = GetComponent<GAgent>();
        health = agent.gStateMonitors.FirstOrDefault(x => x.GetType() == typeof(Health));
        Target = Inventory.FindItemWithTag(StaticTags.Bed);
        if(Target == null){
            return false;
        }
        return true;
    }
    public override bool PostPerform()
    {
        Inventory.RemoveItem(Target);
        health.UpdateStateStrength(healthRestored);
        return true;
    }


}
