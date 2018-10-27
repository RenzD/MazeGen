using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonRun : MonoBehaviour, IDragonAction
{
    Animator anim;
    GameObject player;
    Transform target;
    NavMeshAgent nav;
    bool runTowards = false;
    float distance = 12f;

    const string isRunningStr = "IsRunning";
    public float movementSpeed = 12f;
    public float rotationSpeed = 3f;
    public Dragon dragon;

    public string Name
    {
        get
        {
            return "Run";
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
        runTowards = towards;
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
                if (runTowards)
                {
                    RunTowards();
                }
                else
                {
                    RunAway();
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
        if (runTowards)
        {
            return Vector3.Distance(transform.position, player.transform.position) <= distance;
        }
        else
        {
            //transform.Rotate(0, 180, 0);
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

    private void RunTowards()
    {
        nav.destination = target.position;
        nav.speed = movementSpeed;
        if (!nav.pathPending && nav.remainingDistance > distance)
        {
            nav.isStopped = false;
            StartMovementAnimation();
            //walking = true;
        }
        //if (Vector3.Distance(transform.position, target.position))
        //{
        /*
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

        StartMovementAnimation();

        Vector3 targetDir = player.transform.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);
        //}
        */
    }

    private void RunAway()
    {

        Vector3 runTo = transform.position + ((transform.position - player.transform.position));
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < distance)
        {
            nav.destination = runTo;
            nav.speed = movementSpeed;
            nav.isStopped = false;
        }

        /*
        //if (Vector3.Distance(transform.position, target.position) < distance)
        //{
        transform.LookAt(target);
        transform.Rotate(0, 180, 0);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        Vector3 targetDir = player.transform.position - transform.position;
        //targetDir.x = -targetDir.x;
        //targetDir.z = -targetDir.z;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0f);
        //transform.rotation = Quaternion.LookRotation(newDir);
        */
        StartMovementAnimation();
        //}
    }

    private void StartMovementAnimation()
    {
        anim.SetBool(isRunningStr, true);
    }

    private void StopMovementAnimation()
    {
        anim.SetBool(isRunningStr, false);
    }
}
