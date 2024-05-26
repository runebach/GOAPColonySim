using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(GAgentVisual))]
[CanEditMultipleObjects]
public class GAgentVisualEditor : Editor 
{


    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        GAgentVisual agent = (GAgentVisual) target;
        GUILayout.Label("Name: " + agent.name);
        GUILayout.Label("Current Action: " + agent.gameObject.GetComponent<GAgent>().CurrentAction);
        GUILayout.Label("Current Goal: " + agent.gameObject.GetComponent<GAgent>().currentGoal.Name);
        GUILayout.Label("Actions: ");
        foreach (GAction a in agent.gameObject.GetComponent<GAgent>().Actions)
        {
            string pre = "";
            string eff = "";

            foreach (KeyValuePair<string, int> p in a.PreConditions)
                pre += p.Key + ", ";
            foreach (KeyValuePair<string, int> e in a.PostConditions)
                eff += e.Key + ", ";

            GUILayout.Label("====  " + a.ActionName + "(" + pre + ")(" + eff + ")");
        }
        GUILayout.Label("Goals: ");
        foreach (KeyValuePair<SubGoal, int> g in agent.gameObject.GetComponent<GAgent>().goals)
        {
            GUILayout.Label("---: ");
            foreach (KeyValuePair<string, int> sg in g.Key.SubGoals)
                GUILayout.Label("=====  " + sg.Key);
        }

        GUILayout.Label("MonitorStates: ");
        foreach(GStateManager g in agent.gameObject.GetComponent<GAgent>().gStateMonitors){
            GUILayout.Label("==== " + g.Name + ": " + g.currentStrength);
        }

        GUILayout.Label("Beliefs: ");
        foreach (KeyValuePair<string, int> sg in agent.gameObject.GetComponent<GAgent>().Beliefs.GetStates())
        {
                GUILayout.Label("=====  " + sg.Key + ", " + sg.Value);
        }



        GUILayout.Label("Inventory: ");
        foreach (GameObject g in agent.gameObject.GetComponent<GAgent>().Inventory.items)
        {
            GUILayout.Label("====  " + g.tag);
        }


        serializedObject.ApplyModifiedProperties();
    }
}