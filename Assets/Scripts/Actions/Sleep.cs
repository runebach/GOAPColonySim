using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Sleep : GAction
{
    private GAgent agent;
    private GStateManager energy;
    public float restoredEnergy = 100;
    
    public override bool PrePerform()
    {
        agent = GetComponent<GAgent>();
        energy = agent.gStateMonitors.FirstOrDefault(x => x.GetType() == typeof(Energy));
        Target = GWorld.Instance.GetQueue(StaticQueues.Beds).RemoveResource();
        Inventory.AddItem(Target);
        energy.StopDecayRate();
        
        return true;
    }
    public override bool PostPerform()
    {
        
        GWorld.Instance.GetQueue("beds").AddResource(Target);
        Inventory.RemoveItem(Target);
        energy.UpdateStateStrength(restoredEnergy);
        energy.StartDecayRate();
        return true;
    }



}
