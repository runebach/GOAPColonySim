using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// StaticQueues contain all the strings for the different Queues in the world.
/// A queue only contains objects in the gameworld that can be INTERACTED with, 
/// thus not including states that just track a number.
/// </summary>
public static class StaticQueues
{
    public static string Colonists = "Colonists";
    public static string HurtColonists = "HurtColonists";
    public static string GatherableFood = "GatherableFood";
    public static string Beds = "Beds";
    public static string GatherableGold = "GatherableGold";
    public static string GatherableMedicine = "GatherableMedicine";



}
