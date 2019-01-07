using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    GameObject player;
    PlayerHealth ph;
    PlayerShoot ps;

    public Image hp;
    public Image grass;
    public Image milk;

	void Update ()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            ph = player.GetComponent<PlayerHealth>();
            ps = player.GetComponent<PlayerShoot>();
            return;
        }
        hp.fillAmount = ph.getHealth() / 100;
        grass.fillAmount = ps.GetGrass();
        milk.fillAmount = ps.GetMilk();
	}
}
