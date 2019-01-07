using UnityEngine;

public class TriggersScript : MonoBehaviour {

    [SerializeField]
    string TriggerName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
        if (TriggerName == "JumpTutorial")
            jumpTutorial(other);
        if (TriggerName == "CameraTutorial")
            cameraTutorial();
        if (TriggerName == "VisionTutorial")
            visionTutorial();
        if (TriggerName == "SprintTutorial")
            sprintTutorial();
        if (TriggerName == "PontTrigger")
            bridgeTrigger();
        if (TriggerName == "Tractor")
            TractorTrigger();
        if (TriggerName == "Fire")
            fireTrigger();
    }

    void jumpTutorial(Collider other)
    {
        if (AudioManager.instance.GetSFX2DAS().isPlaying)
            AudioManager.instance.GetSFX2DAS().Stop();
        AudioManager.instance.PlaySound2D("tuto_saut");
        Flags.instance.fill_dialog_box("Appuie sur le touche ESPACE ou sur A (Manette) pour sauter au dessus de ces bottes de foin !", false);
        Flags.instance.canJump = true;
        Flags.instance.jumpTutorial = true;
        Destroy(gameObject);
    }

    void visionTutorial()
    {
        if (AudioManager.instance.GetSFX2DAS().isPlaying)
            AudioManager.instance.GetSFX2DAS().Stop();
        AudioManager.instance.PlaySound2D("tuto_camera");
        Flags.instance.fill_dialog_box("Bouge ta caméra en utilisant ta souris (ou tes joysticks si tu joues avec la manette).", true);
        Flags.instance.visionTutorial = true;
        Destroy(gameObject);
    }

    void cameraTutorial()
    {
        if (AudioManager.instance.GetSFX2DAS().isPlaying)
            AudioManager.instance.GetSFX2DAS().Stop();
        AudioManager.instance.PlaySound2D("choupi");
        Flags.instance.fill_dialog_box("Appuie sur la touche de saut afin de passer les dialogues ! Tu peux interagir avec les objets qui t'environnent avec la touche E (Y sur manette). Essaye avec la radio !", true);
        Flags.instance.cameraTutorial = true;
        Destroy(gameObject);
    }

     void sprintTutorial()
    {
        if (AudioManager.instance.GetSFX2DAS().isPlaying)
            AudioManager.instance.GetSFX2DAS().Stop();
        AudioManager.instance.PlaySound2D("tuto_pont_bois");
        Flags.instance.canSprint = true;
        Flags.instance.sprintTutorial = true;
        Flags.instance.fill_dialog_box("Tu peux courrir en appuyant sur MAJ (clic sur le joystick si tu es sur manette). Essaye d'aller vers le pont pour aller commencer à travailler aujourd'hui !", true);
        Destroy(gameObject);
    }

    void fireTrigger()
    {
        if (!Flags.instance.wave1)
            return;
        if (AudioManager.instance.GetSFX2DAS().isPlaying)
            AudioManager.instance.GetSFX2DAS().Stop();
        Flags.instance.canFire = true;
        Flags.instance.fire = true;
        AudioManager.instance.PlaySound2D("tuto_combat");
        Flags.instance.fill_dialog_box("Tu peux tirer sur ces porcs en utilisant ton clic gauche (gachette pour les utilisateurs de manette).", true);
        Destroy(gameObject);
    }

    void bridgeTrigger()
    {
        if (AudioManager.instance.GetSFX2DAS().isPlaying)
            AudioManager.instance.GetSFX2DAS().Stop();
        AudioManager.instance.PlaySound2D("tuto_tracteur");
        Flags.instance.reachedBridge = true;
        Flags.instance.fill_dialog_box("Va utiliser ton tracteur !! Il est temps que tu travaille aujourd'hui !", false);
        Destroy(gameObject);
    }

    void TractorTrigger()
    {
        Flags.instance.fill_dialog_box("Mais où sont les clées de mon tracteur ? Je devrais aller voir Squeelie !", true);
        Flags.instance.reachTractor = true;
        Destroy(gameObject);
    }
}

