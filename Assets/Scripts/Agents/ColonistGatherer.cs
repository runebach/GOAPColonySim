using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColonistGatherer : GAgent
{

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        // GOALS
        SubGoal idle = new SubGoal("Idle", 1, false);
        goals.Add(idle, 1);
        SubGoal foodGathered = new SubGoal("FoodGathered", 1, false);
        goals.Add(foodGathered, 2);
        SubGoal energyRestored = new SubGoal("EnergyRestored", 1, false);
        goals.Add(energyRestored, 3);
        SubGoal hungerRestored = new SubGoal("HungerRestored", 1, false);
        goals.Add(hungerRestored, 4);
    }


}
