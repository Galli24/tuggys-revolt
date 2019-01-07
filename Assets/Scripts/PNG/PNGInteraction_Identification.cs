using UnityEngine;
using UnityEngine.UI;

public class PNGInteraction_Identification : MonoBehaviour
{

    public string NPC_name;

    public GameObject noisettes;

    public void identification()
    {
        if (NPC_name == "Squirrel" && Flags.instance.reachTractor)
            squirrel_script();
        if (NPC_name == "Radio")
            radio_script();
    }

    void radio_script()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        if (audio.isPlaying)
            audio.Pause();
        else
            audio.Play();
    }

    public void squirrel_script()
    {
        if (!Flags.instance.squirrelQuestFinished)
        {
            if (!Flags.instance.squirrelQuest)
            {
                Flags.instance.squirrelQuest = true;
                Flags.instance.fill_dialog_box("Yo Tuggy c'est Squeelie, j'ai caché ta clé, trouve les 3 noisettes si tu veux la revoir ! Ahahahah. Si je me souviens bien, une est sur la terrasse d'un immeuble, une derrière l'Espincho, et la dernière à côté des burritos", true);
                AudioManager.instance.PlaySound("squeelie", transform.position);
                if (noisettes != null)
                    noisettes.SetActive(true);
            }
            else if (Flags.instance.gotSquirrelItem)
            {
                Flags.instance.squirrelQuestFinished = true;
                Flags.instance.fill_dialog_box("Bien joué, voici la clé... Mais qu'est-ce que...", true);
                Flags.instance.kill = 0;
            }
        }   
    }
}
