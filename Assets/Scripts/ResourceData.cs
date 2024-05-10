using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "resourceData", menuName = "Resource Data", order = 51)]
public class ResourceData : ScriptableObject
{
    public string ResourceTag;
    public string ResourceQueue;
    public string ResourceState;
}
