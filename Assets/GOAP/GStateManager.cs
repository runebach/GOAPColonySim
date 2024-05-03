using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GStateManager : MonoBehaviour
{
    public string Name;
    public string BeliefState;
    public float InitialStrength = 100;
    public float StateDecayRate = 2;
    public float LowerActionBoundary = 25;
    public WorldStates Beliefs;
    public float currentStrength;

    // Start is called before the first frame update
    void Start()
    {
        Beliefs = GetComponent<GAgent>().Beliefs;
        currentStrength = InitialStrength;
    }

    // Update is called once per frame
    void Update()
    {
        currentStrength -= StateDecayRate * Time.deltaTime;
        currentStrength = Mathf.Clamp(currentStrength, 0, InitialStrength);

        UpdateBelief();
        
    }

    public void UpdateStateStrength(float value){
        currentStrength += value;
    }
    private void UpdateBelief(){
        if(currentStrength < LowerActionBoundary){
            Beliefs.SetState(BeliefState, 1);
        }
        else if(currentStrength > LowerActionBoundary){
            Beliefs.RemoveState(BeliefState);
        }
    }
    protected abstract void OnStateMinimum();
}
