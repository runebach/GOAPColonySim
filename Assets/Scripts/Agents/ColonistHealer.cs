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
        SubGoal idle = new SubGoal(StaticGoals.Idle, 1, false, true);
        goals.Add(idle, 1);
        SubGoal allyHealed = new SubGoal(StaticGoals.AllyHealed, 1, false, false);
        goals.Add(allyHealed, 4);
        SubGoal energyRestored = new SubGoal(StaticGoals.EnergyRestored, 1, false, false);
        goals.Add(energyRestored, 3);
        SubGoal hungerRestored = new SubGoal(StaticGoals.HungerRestored, 1, false, false);
        goals.Add(hungerRestored, 5);
        SubGoal healthRestored = new SubGoal(StaticGoals.HealthRestored, 1, false, false);
        goals.Add(healthRestored, 6);
        SubGoal medicineGathered = new SubGoal(StaticGoals.MedicineGathered, 1, false, false);
        goals.Add(medicineGathered, 2);
    }


}
