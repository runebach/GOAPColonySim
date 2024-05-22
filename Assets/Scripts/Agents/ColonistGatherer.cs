using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColonistGatherer : GAgent
{

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();

        // GOALS
        SubGoal idle = new SubGoal("Idle", 1, false);
        goals.Add(idle, 1);
        SubGoal foodGathered = new SubGoal("FoodGathered", 1, false);
        goals.Add(foodGathered, 3);
        SubGoal energyRestored = new SubGoal("EnergyRestored", 1, false);
        goals.Add(energyRestored, 4);
        SubGoal hungerRestored = new SubGoal("HungerRestored", 1, false);
        goals.Add(hungerRestored, 5);
        SubGoal goldGathered = new SubGoal("GoldGathered", 1, false);
        goals.Add(goldGathered, 2);
        SubGoal healthRestored = new SubGoal("HealthRestored", 1, false);
        goals.Add(healthRestored, 6);
    }


}
