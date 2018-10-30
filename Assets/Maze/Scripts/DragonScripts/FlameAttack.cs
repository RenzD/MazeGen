using System.Collections;
using UnityEngine;

public class FlameAttack : DragonAttack
{
    Animator anim;
    GameObject player;

    int currentRepeatCount;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        IsDoing = false;
        repeatCount = 3; // -1
        currentRepeatCount = repeatCount;
        //minTriggerRange = 10f;
        //maxTriggerRange = 15f;
    }



    // Update is called once per frame
    void Update()
    {

    }

    public override string Name
    {
        get
        {
            return "FlameAttack";
        }
    }

    public override bool IsDoing
    {
        get;
        set;
    }

    public override void Do()
    {
        if (!IsDone())
        {
            IsDoing = true;
            TriggerAttackAnimation();
        }
    }

    private void TriggerAttackAnimation()
    {
        if (IsDoing)
        {
            if (IsDone())
            {
                IsDoing = false;
                currentRepeatCount = repeatCount;
            }
            else
            {
                anim.SetTrigger(Name);
            }

            --currentRepeatCount;
        }
    }


    public override bool IsDone()
    {
        return currentRepeatCount <= 0;
    }

    public override bool CanDo()
    {
        return true;
    }
}
