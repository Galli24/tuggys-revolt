using UnityEngine;

public class Bacon : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                if (ph.getHealth() < 100f)
                {
                    ph.setHealth(ph.getHealth() + 30);
                    AudioManager.instance.PlaySound2D("pickup");
                    Destroy(gameObject);
                }
            }
        }
    }
}
