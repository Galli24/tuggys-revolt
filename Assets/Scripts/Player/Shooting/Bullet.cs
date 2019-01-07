using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private float BulletSpeed = 25f;
    [SerializeField]
    private float lifeTime = 1.5f;
    private float timeLeft;
    [SerializeField]
    private float damage = 10f;

    private Vector3 dir;

    void Start()
    {
        dir = transform.forward;
        timeLeft = lifeTime;
    }

    void Update ()
    {
        timeLeft -= Time.deltaTime;
        //if (timeLeft <= 0)
            //Destroy(gameObject);
        Vector3 newPos = transform.position + dir * BulletSpeed * Time.deltaTime;
        if (timeLeft < 0.99 * lifeTime)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + .8f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            dir = transform.forward;
        }
        transform.position = newPos;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
                enemyHealth.takeDamage(damage);
        }

        if (other.tag != "Player" && other.tag != "Bullet" && other.tag != "Bullet Pos")
            SimplePool.Despawn(gameObject);
    }
}
