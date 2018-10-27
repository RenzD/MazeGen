using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : BaseCreature
{
    //public const string tag = "Dragon";
    const string isWalkingStr = "IsWalking";
    const string isFlyingStr = "IsFlying";
    const string basicAttackStr = "BasicAttack";
    const string flameAttackStr = "flameAttack";
    const float basicAttackMaxDistance = 1f;
    const float flameAttackMaxDistance = 1f;

    //public int health;
    //public int basicAttackDamage;
    //public float flyingSpeed = 2f;
    //public float movementSpeed = 6f;
    public float rotationSpeed = 3f;
    public float wakeDistance = 7f;
    //public float climbHeight = 5f;
    public GameObject player;

    bool isAwake = false;
    bool isAttacking = false;
    bool isAlive = true;
    string previousAnimationName = "";
    List<IDragonAction> actions;

    Rigidbody rb;
    Animator anim;
    NavMeshAgent nav;
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        player = GetPlayerToTrack();
        actions = new List<IDragonAction>()
        {
            //gameObject.GetComponent<DragonFly>()
            
            gameObject.GetComponent<DragonWalk>()
            , gameObject.GetComponent<BasicAttack>()
            //, gameObject.GetComponent<DragonRun>()
            //, gameObject.GetComponent<FlameAttack>()

        };
        health = 200;
    }


    void Start()
    {
        //gameObject.GetComponent<BasicAttack>().Do();
        anim.SetBool("IsWaiting", true);
    }
    // Update is called once per frame
    void Update()
    {
        if (isAwake)
        {
            if (health > 0)
            {
                Do();
            }
            else
            {
                Die();
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) < wakeDistance)
            {
                anim.SetBool("IsWaiting", false);
                isAwake = true;
            }
        }
        //Climb();
    }

    public override void Die()
    {
        if (isAlive)
        {
            anim.SetTrigger("Die");
            isAlive = false;
            nav.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Weapon"))
        {
            TakeDamage(1000);
        }
    }

    public void Do()
    {
        if (actions[0] is DragonAttack)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            Rotate();

            if (!isAttacking)
            {
                isAttacking = true;
                actions[0].Do();

            }
            if (!actions[0].IsDoing)
            {
                previousAnimationName = actions[0].Name;
                actions.RemoveAt(0);
                isAttacking = false;
            }

        }
        else
        {
            if (!IsPlayingAnimation(previousAnimationName))
            {
                actions[0].Do();
                if (!actions[0].IsDoing)
                {
                    actions.RemoveAt(0);
                }
            }
        }
        if (actions.Count == 0)
        {

            actions.Add(gameObject.GetComponent<DragonWalk>());
            actions.Add(gameObject.GetComponent<BasicAttack>());
            // actions.Add(gameObject.GetComponent<DragonRun>());
            //actions.Add(gameObject.GetComponent<FlameAttack>());

        }
    }

    public bool IsPlayingAnimation(string name)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(name) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void Rotate()
    {
        if (player != null)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            Vector3 targetDir = player.transform.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationStep, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
    public override void BasicAttack()
    {
    }



    /*
    private bool CanAttack()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 20f)
        {
            Debug.Log(distance);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Attack()
    {
        if (CanAttack())
        {
            if (!isAttacking)
            {
                isAttacking = true;
                TriggerAttackAnimation();
            }
        }
    }

    private void TriggerAttackAnimation()
    {
        if (isAttacking)
            anim.SetTrigger(currentAction.Name);
    }

    protected override void Move()
    {
        if (player != null)
        {
            // The step size is equal to speed times frame time.
            float movementStep = movementSpeed * Time.deltaTime;
            float rotationStep = rotationSpeed * Time.deltaTime;

            // Move our position a step closer to the target.
            if (!transform.position.Equals(player.transform.position))
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementStep);

                StartMovementAnimation();

                Vector3 targetDir = player.transform.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationStep, 0f);
                transform.rotation = Quaternion.LookRotation(newDir);

            }
            else
            {
                StopMovementAnimation();
            }
        }
    }

    public void Climb()
    {
        float flyingStep = flyingSpeed * Time.deltaTime;
        if (transform.position.y < climbHeight)
        {
            Vector3 movement = transform.position;
            movement.y = climbHeight;
            transform.position = Vector3.MoveTowards(transform.position, movement, flyingStep);
        }
    }

    public void FlyAway()
    {
        if (player != null)
        {
            // The step size is equal to speed times frame time.
            float flyingStep = flyingSpeed * Time.deltaTime;
            float rotationStep = rotationSpeed * Time.deltaTime;

            // Move our position a step closer to the target.
            if (Vector3.Distance(transform.position, player.transform.position) < 10f)
            {
                transform.LookAt(player.transform);
                transform.Rotate(0, 180, 0);
                StartFlyingAnimation();
                transform.Translate(Vector3.forward * flyingStep);

                Vector3 targetDir = player.transform.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationStep, 0f);
                transform.rotation = Quaternion.LookRotation(newDir);

            }
            else
            {

                StopFlyingAnimation();
            }
        }
    }

    private void StartFlyingAnimation()
    {
        anim.SetBool(isFlyingStr, true);
    }

    private void StopFlyingAnimation()
    {
        anim.SetBool(isFlyingStr, false);
    }

    private void StartMovementAnimation()
    {
        anim.SetBool(isWalkingStr, true);
    }

    private void StopMovementAnimation()
    {
        anim.SetBool(isWalkingStr, false);
    }
    */
    GameObject GetPlayerToTrack()
    {
        GameObject player = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        return player;
    }


    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damage)
    {
        health = health - damage;
        Debug.Log(health);
        if (health <= 0)
            Die();
    }
}
