using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class DragonFly : MonoBehaviour, IDragonAction
{
    Animator anim;
    GameObject player;
    Transform target;
    NavMeshAgent nav;
    public float distance = 8f;
    public Vector3 destination;

    const string isFlyingStr = "IsFlying";
    public float movementSpeed = 3f;
    //public float rotationSpeed = 3f;
    public Dragon dragon;

    public string Name
    {
        get
        {
            return "Fly";
        }
    }

    public bool IsDoing
    {
        get;
        set;
    }


    // Use this for initialization
    void Start()
    {
        dragon = gameObject.GetComponent<Dragon>();
        anim = gameObject.GetComponent<Animator>();
        nav = gameObject.GetComponent<NavMeshAgent>();
        player = dragon.player;
        target = player.transform;
        destination = transform.position + (Vector3.up * 10);
        IsDoing = false;
    }

    // Update is called once per frame
    void Update()
    {
        player = dragon.player;
    }

    public void Set(bool towards, float dist)
    {
        distance = dist;
    }

    public bool CanDo()
    {
        return true;
    }

    public void Do()
    {
        if (player != null)
        {
            // The step size is equal to speed times frame time.

            // Move our position a step closer to the target.
            if (!IsDone())
            {
                IsDoing = true;
                Fly();
            }
            else
            {
                IsDoing = false;
                StopMovementAnimation();
            }
        }
    }


    public bool IsDone()
    {
        /*
        if (true)
        {
            return Vector3.Distance(transform.position, player.transform.position) <= distance;
        }
        */
        return false;
    }

    private void Fly()
    {
        //transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
        transform.position = transform.position + (Vector3.up * 3);
        nav.isStopped = true;
        StartMovementAnimation();
    }

    private void StartMovementAnimation()
    {
        anim.SetBool(isFlyingStr, true);
    }

    private void StopMovementAnimation()
    {
        anim.SetBool(isFlyingStr, false);
    }
}
