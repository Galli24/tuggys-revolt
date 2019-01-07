using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

    public void LoadByIndex(int sceneindex)
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlayMusic(null, 0);
        SceneManager.LoadScene(sceneindex);
    }
}