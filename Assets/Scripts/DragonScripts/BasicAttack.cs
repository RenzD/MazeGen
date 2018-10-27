using UnityEngine;

public class BasicAttack : DragonAttack
{
    Animator anim;
    GameObject player;

    int currentRepeatCount;

    public override string Name
    {
        get
        {
            return "BasicAttack";
        }
    }


    public override bool IsDoing
    {
        get;
        set;
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        IsDoing = false;
        repeatCount = 3; // -1
        currentRepeatCount = repeatCount;
        //minTriggerRange = 1f;
        //maxTriggerRange = 3f;
    }



    // Update is called once per frame
    void Update()
    {

    }

    public override void Do()
    {
        if (!IsDone())
        {
            IsDoing = true;
            TriggerAttackAnimation();
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
}

