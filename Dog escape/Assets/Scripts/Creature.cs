using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    public string creatureName;

    protected int health;

    protected float walkSpeed;
    protected float runSpeed;
    protected float jumpSpeed;
    protected float turnSpeed;

    protected bool isAlive = true;

    protected abstract void Move();

    public void AddHealth()
    {
        if (isAlive)
        {
            health ++;
        }
    }

    public void TakeDamage()
    {
        if (isAlive)
        {
            health --;
        }
        if (health < 1)
        {
            Die();
        }
    }

    protected virtual void CauseDamage(Creature creatureTakingDamage, int damageCount)
    {
        if (isAlive)
        {
            creatureTakingDamage.TakeDamage();
        }
        
    }

    public void Die()
    {
        if (isAlive)
        {
            isAlive = false;
        }
    }

    public void SetMovementBoundaries(float movementBounary)
    {
        //set boundary //49f;
        if (transform.position.x > movementBounary)
        {
            transform.position = new Vector3(movementBounary, transform.position.y, transform.position.z);
        }else if (transform.position.x < -movementBounary)
        {
            transform.position = new Vector3(-movementBounary, transform.position.y, transform.position.z);
        }else if (transform.position.z > movementBounary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, movementBounary);
        }else if (transform.position.z < -movementBounary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -movementBounary);
        }
    }

}
