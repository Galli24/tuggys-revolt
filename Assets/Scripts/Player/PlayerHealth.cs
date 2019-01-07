using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private float health = 100f;
    private float currentiframes = 0f;
    [SerializeField]
    private float iframes = 0.5f;

    IEnumerator Death()
    {

        yield return new WaitForSeconds(2);
        Flags.instance.Spawn();
        Destroy(gameObject);
    }

    public void Die()
    {
        Flags.instance._move = Flags.instance.canMove;
        Flags.instance._fire = Flags.instance.canFire;
        Flags.instance._sprint = Flags.instance.canSprint;
        Flags.instance._jump = Flags.instance.canJump;
        Flags.instance._dead = Flags.instance.isDead;
        Flags.instance.canFire = false;
        Flags.instance.canSprint = false;
        Flags.instance.canJump = false;
        Flags.instance.canMove = false;
        Flags.instance.isDead = true;
        GetComponent<Animator>().SetTrigger("Death");
        StartCoroutine(Death());
    } 

    void Update()
    {
        if (currentiframes > 0f)
            currentiframes -= Time.deltaTime;
    }

    public void takeDamage(float damage)
    {
        if (!Flags.instance.canMove)
            return;
        if (currentiframes > 0f)
            return;

        health -= damage;
        currentiframes = iframes;

        if (health <= 0)
            Die();
    }

    public float getHealth()
    {
        return health;
    }

    public void setHealth(float value)
    {
        health = value;
        if (health > 100f)
            health = 100f;
    }
}

