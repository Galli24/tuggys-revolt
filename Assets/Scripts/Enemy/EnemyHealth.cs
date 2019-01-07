using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    [SerializeField]
    private float health = 100f;
    private bool dead = false;

    private float currentiframes = 0f;
    [SerializeField]
    private float iframes = 0.2f;

    public GameObject baconPrefab;

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2.5f);
        if (Random.Range(0f, 1f) <= 0.3f)
            Instantiate(baconPrefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.Euler(-90f, 0f, 0f));
        Destroy(gameObject);
    }

    void Die()
    {
        Flags.instance.kill = Flags.instance.kill + 1;
        dead = true;
        GetComponent<Animator>().SetTrigger("Death");
        StartCoroutine(Death());
        AudioManager.instance.PlaySound2D("pig_die");
    }

    void Update()
    {
        if (currentiframes > 0f)
            currentiframes -= Time.deltaTime;
    }

    public void takeDamage(float damage)
    {
        if (currentiframes > 0f)
            return;

        health -= damage;
        currentiframes = iframes;

        if (health <= 0 && !dead)
            Die();

        if (health > 0 && !dead)
            AudioManager.instance.PlaySound2D("pig_hit");
    }

    public float getHealth()
    {
        return health;
    }

    public bool getDead()
    {
        return dead;
    }

    public void setHealth(float value)
    {
        health = value;
    }
}
