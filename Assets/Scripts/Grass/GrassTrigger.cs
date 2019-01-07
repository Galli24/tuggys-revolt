using UnityEngine;

public class GrassTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerShoot ps = other.GetComponent<PlayerShoot>();
            if (ps != null)
            {
                if (ps.GetGrass() != 1.0f)
                {
                    StartCoroutine(ps.AddGrass(0.6f));
                    AudioManager.instance.PlaySound2D("pickup");
                    gameObject.SetActive(false);
                }   
            }
        }
    }
}
