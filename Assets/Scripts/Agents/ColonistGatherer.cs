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
        SubGoal idle = new SubGoal(StaticGoals.Idle, 1, false, true);
        goals.Add(idle, 1);
        SubGoal foodGathered = new SubGoal(StaticGoals.FoodGathered, 1, false, false);
        goals.Add(foodGathered, 3);
        SubGoal energyRestored = new SubGoal(StaticGoals.EnergyRestored, 1, false, false);
        goals.Add(energyRestored, 4);
        SubGoal hungerRestored = new SubGoal(StaticGoals.HungerRestored, 1, false, false);
        goals.Add(hungerRestored, 5);
        SubGoal goldGathered = new SubGoal(StaticGoals.GoldGathered, 1, false, false);
        goals.Add(goldGathered, 2);
        SubGoal healthRestored = new SubGoal(StaticGoals.HealthRestored, 1, false, false);
        goals.Add(healthRestored, 6);
    }


}
