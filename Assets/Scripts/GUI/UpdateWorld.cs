using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateWorld : MonoBehaviour
{
    public TextMeshProUGUI states;


    void LateUpdate(){
        Dictionary<string, int> worldstates = GWorld.Instance.GetWorld().GetStates();

        states.text = "";
        foreach(KeyValuePair<string, int> s in worldstates){
            states.text += s.Key + ": " + s.Value + "\n";
        }
    }
}
