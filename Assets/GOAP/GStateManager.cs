using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GStateManager : MonoBehaviour
{
    public string Name;
    public string BeliefState;
    public float InitialStrength = 100;
    public float StateDecayRate = 0;
    public float LowerActionBoundary = 25;
    public WorldStates Beliefs;
    protected GAgent Agent;
    public float currentStrength;
    public float currentDecayRate;

    // Start is called before the first frame update
    public void Start()
    {
        Beliefs = GetComponent<GAgent>().Beliefs;
        Agent = GetComponent<GAgent>();
        currentStrength = InitialStrength;
        currentDecayRate = StateDecayRate;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentStrength -= currentDecayRate * Time.deltaTime;
        currentStrength = Mathf.Clamp(currentStrength, 0, InitialStrength);

        UpdateBelief();
        OnStateMinimum();
        
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
    public void StopDecayRate(){
        currentDecayRate = 0;
    }
    public void StartDecayRate(){
        currentDecayRate = StateDecayRate;
    }
    protected abstract void OnStateMinimum();
}
