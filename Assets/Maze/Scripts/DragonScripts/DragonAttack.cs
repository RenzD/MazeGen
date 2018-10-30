using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragonAttack : MonoBehaviour, IDragonAction
{
    public abstract string Name { get; }


    public int repeatCount;
    //public float minTriggerRange;
    //public float maxTriggerRange;


    public abstract bool IsDoing { get; set; }
    public abstract bool CanDo();

    public abstract void Do();

    public abstract bool IsDone();


}