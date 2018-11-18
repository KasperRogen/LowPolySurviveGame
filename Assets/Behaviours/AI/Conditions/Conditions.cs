using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using BBUnity.Conditions;

[Condition("MyConditions/IsNight")]
[Help("Checks whether it is night. It searches for the first light labeled with " +
      "the 'MainLight' tag, and looks for its DayNightCycle script, returning the" +
      "informed state. If no light is found, false is returned.")]
public class Conditions: ConditionBase
{
    public override bool Check()
    {
        GameObject light = GameObject.FindGameObjectWithTag("MainLight");
        if (light != null)
        {
            DayNightCycle dnc = light.GetComponent<DayNightCycle>();
            if (dnc != null)
                return dnc.isNight;
        }

        return false;
    }
} // class IsNightCondition






[Condition("MyConditions/IsHungry")]
[Help("Checks whether the gameobject is currently hungry")]
public class IsHungry : GOCondition
{
    public override bool Check()
    {
        AnimalScript animal = gameObject.GetComponent<AnimalScript>();
        if (animal != null)
        {
                return animal.IsHungry;
        }

        return false;
    }
} // class IsHungryCondition


