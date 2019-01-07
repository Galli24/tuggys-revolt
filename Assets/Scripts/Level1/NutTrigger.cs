using UnityEngine;

public class NutTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.instance.PlaySound2D("pickup");
            if (!Flags.instance.gotfirstSquirrel)
            {
                Flags.instance.gotfirstSquirrel = true;
                Flags.instance.AllItemSquirrel();
                Flags.instance.fill_dialog_box("Vous avez trouvé la première noisette !", true);
                Destroy(gameObject);
                return;
            }
            else if (!Flags.instance.gotsecondSquirrel)
            {
                Flags.instance.fill_dialog_box("Vous avez trouvé la seconde noisette !", true);
                Flags.instance.gotsecondSquirrel = true;
                Flags.instance.AllItemSquirrel();
                Destroy(gameObject);
                return;
            }
            if (!Flags.instance.gotthirdSquirrel)
            {
                Flags.instance.fill_dialog_box("Vous avez trouvé la dernière noisette !", true);
                Flags.instance.gotthirdSquirrel = true;
                Flags.instance.AllItemSquirrel();
                Destroy(gameObject);
                return;
            }
        }
    }   
}
