using UnityEngine;

public class Wave1 : MonoBehaviour {

    public GameObject camionPouetPouet;
    public GameObject PigWave1;
    public GameObject PigWave2;
    public GameObject PigWave3;
    public GameObject VonDerMesCouilles;

    bool wave2 = false;
    bool wave3 = false;
    bool wave_boss = false;

    public void pig_wave1()
    {
        Flags.instance.fill_dialog_box("Cinq cochons essayent de voler l'Espincho !", true);
        PigWave1.SetActive(true);
        camionPouetPouet.SetActive(true);
    }

    public void pig_wave2()
    {
        Flags.instance.fill_dialog_box("Dix cochons ont été aperçu devant la grange !", true);
        PigWave2.SetActive(true);
    }

    public void pig_wave3()
    {
        Flags.instance.fill_dialog_box("Ces satanés cochons envahissent la route, tue les !", true);
        PigWave3.SetActive(true);
    }

    public void boss_wave()
    {
        Flags.instance.fill_dialog_box("On a un problème, Otto Von Pigger s'en mêle..", true);
        VonDerMesCouilles.SetActive(true);
    }

    void Update () {
        if (Flags.instance.squirrelQuestFinished == true && Flags.instance.wave1 == false)
        {
            pig_wave1();
            Flags.instance.wave1 = true;
        }
        if (Flags.instance.kill == 5 && !wave2)
        {
            Flags.instance.kill = 0;
            wave2 = true;
            pig_wave2();
        }
        if (Flags.instance.kill == 10 && !wave3)
        {
            Flags.instance.kill = 0;
            wave3 = true;
            pig_wave3();
        }
        if (Flags.instance.kill == 15 && !wave_boss)
        {
            Flags.instance.kill = 0;
            wave_boss = true;
            boss_wave();
        }

        if (Flags.instance.kill == 1 && wave_boss)
            Flags.instance.bossKilled = true;
	}
}
