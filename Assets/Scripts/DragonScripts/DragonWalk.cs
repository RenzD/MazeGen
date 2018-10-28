﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonWalk : MonoBehaviour, IDragonAction
{
    Animator anim;
    GameObject player;
    Transform target;
    NavMeshAgent nav;
    bool walkTowards = true;
    public float distance = 8f;

    const string isWalkingStr = "IsWalking";
    public float movementSpeed = 6f;
    public float rotationSpeed = 3f;
    public Dragon dragon;

    public string Name
    {
        get
        {
            return "Walk";
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
        IsDoing = false;
    }

    // Update is called once per frame
    void Update()
    {
        player = dragon.player;
    }

    public void Set(bool towards, float dist)
    {
        walkTowards = towards;
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
                if (nav.isOnNavMesh)
                {
                    if (walkTowards)
                    {
                        WalkTowards();
                    }
                    else
                    {
                        WalkAway();
                    }
                }
            }
            else
            {
                IsDoing = false;
                nav.velocity = Vector3.zero;
                nav.isStopped = true;
                StopMovementAnimation();
            }
        }
    }


    public bool IsDone()
    {
        if (walkTowards)
        {
            return Vector3.Distance(transform.position, player.transform.position) <= distance;
        }
        else
        {
            return Vector3.Distance(transform.position, player.transform.position) >= distance;

        }
    }


    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ResetTarget()
    {
        target = player.transform;
    }

    private void WalkTowards()
    {
        nav.destination = target.position;
        nav.speed = movementSpeed;
        if (!nav.pathPending && nav.remainingDistance > distance)
        {
            nav.isStopped = false;
            StartMovementAnimation();

        }
    }

    private void WalkAway()
    {
        Vector3 toPlayer = player.transform.position - transform.position;
        if (Vector3.Distance(player.transform.position, transform.position) < distance)
        {
            Vector3 targetPosition = toPlayer.normalized * -1 * distance;
            nav.destination = targetPosition;
            nav.isStopped = false;
        }
        StartMovementAnimation();
    }

    private void StartMovementAnimation()
    {
        anim.SetBool(isWalkingStr, true);
    }

    private void StopMovementAnimation()
    {
        anim.SetBool(isWalkingStr, false);
    }
}
