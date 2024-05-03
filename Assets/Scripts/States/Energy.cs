using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : GStateManager
{
    public float SpeedModifier = 0.5f;
    private float speed;
    new void Start(){
        base.Start();
    }
    
    protected override void OnStateMinimum()
    {
        if(currentStrength <= 0){
            speed = SpeedModifier;

        }
        else{
            speed = 1;
        }
        Agent.CurrentAction.Agent.speed *= speed;

    }
}
