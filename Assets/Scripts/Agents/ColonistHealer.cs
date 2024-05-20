using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColonistHealer : GAgent
{

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();

        // GOALS
        SubGoal idle = new SubGoal("Idle", 1, false);
        goals.Add(idle, 1);
        SubGoal allyHealed = new SubGoal("AllyHealed", 1, false);
        goals.Add(allyHealed, 4);
        SubGoal energyRestored = new SubGoal("EnergyRestored", 1, false);
        goals.Add(energyRestored, 3);
        SubGoal hungerRestored = new SubGoal("HungerRestored", 1, false);
        goals.Add(hungerRestored, 5);
        SubGoal medicineCollected = new SubGoal("MedicineGathered", 1, false);
        goals.Add(medicineCollected, 2);
    }


}
