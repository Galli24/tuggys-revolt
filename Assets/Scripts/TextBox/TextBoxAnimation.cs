using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class TextBoxAnimation : MonoBehaviour {

    public bool is_active = false;
    public GameObject img;

	// Update is called once per frame
	void Update ()
    {
        Text text = gameObject.GetComponent<Text>();
        if (is_active || Flags.instance.isDead)
            Flags.instance.canMove = false;
        else
            Flags.instance.canMove = true;
        if (text.text != "")
            img.SetActive(true);
        else
            img.SetActive(false);
        if (text.text != "" && CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            is_active = false;
            text.text = "";
        }
    }
}
