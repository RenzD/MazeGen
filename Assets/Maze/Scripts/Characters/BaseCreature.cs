using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCreature : MonoBehaviour
{
    public int health { get; set; }
    public int basicAttackDamage { get; set; }
    public int movementSpeed { get; set; }

	public abstract void Move ();

    public abstract void TakeDamage(int damage);

	public abstract void BasicAttack ();

	public abstract void Die ();

}
