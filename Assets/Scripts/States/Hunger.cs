using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hunger : GStateManager
{
    GStateManager health;
    public float HungerHealthDecay = 2;

    new void Start(){
        base.Start();
        health = Agent.gStateMonitors.FirstOrDefault(x => x.GetType() == typeof(Health));
        Debug.Log(Agent.gStateMonitors.Count());
    }

    protected override void OnStateMinimum()
    {
        if(currentStrength <= 0){
            health.StateDecayRate = HungerHealthDecay;
        }
        else{
            health.StateDecayRate = 0;
        }
    }
}
