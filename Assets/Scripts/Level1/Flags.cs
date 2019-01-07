using UnityEngine;
using UnityEngine.UI;


public class Flags : MonoBehaviour {

    public static Flags instance;

    public GameObject player;

    public Text text;
    public  TextBoxAnimation box;

    public GameObject PauseMenu;
    public bool paused = false;

    //Checkpoint
    Vector3 checkpoint = new Vector3(-74.43f, 9.37f, -79.6f);

    //Flags
    public bool canMove = true;
    public bool canJump = false;
    public bool canFire = false;
    public bool canSprint = false;
    public bool isDead = false;

    //Movement Tutorial
    public bool cameraTutorial = false;
    public bool jumpTutorial = false;
    public bool visionTutorial = false;
    public bool sprintTutorial = false;

    //Bridge
    public bool reachedBridge = false;

    //Tractor
    public bool reachTractor = false;

    //Squirrel
    public bool squirrelQuest = false;
    public bool gotfirstSquirrel = false;
    public bool gotsecondSquirrel = false;
    public bool gotthirdSquirrel = false;
    public bool gotSquirrelItem = false;
    public bool squirrelQuestFinished = false;

    //Pigs
    public int kill = 0;
    public bool wave1 = false;
    public bool fire = false;

    //SavePauseBools
    public bool _move;
    public bool _fire;
    public bool _jump;
    public bool _sprint;
    public bool _dead;

    //Boss
    public bool bossKilled = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Instantiate(player, checkpoint, Quaternion.Euler(new Vector3(0, 240, 0)));
    }

    public void fill_dialog_box(string str, bool stop)
    {
        if (stop)
            box.is_active = true;
        text.text = str;
    }

    public void Spawn()
    {
        Instantiate(player, checkpoint, Quaternion.identity);
        isDead = _dead;
        canFire = _fire;
        canSprint = _sprint;
        canJump = _jump;
        canMove = _move;
    }

    public void AllItemSquirrel()
    {
        if (Flags.instance.gotfirstSquirrel == true)
            if (Flags.instance.gotsecondSquirrel == true)
                if (Flags.instance.gotthirdSquirrel == true)
                    Flags.instance.gotSquirrelItem = true;
    }

    public void Pause()
    {
        _move = canMove;
        _fire = canFire;
        _sprint = canSprint;
        _jump = canJump;
        _dead = isDead;
        canFire = false;
        canSprint = false;
        canJump = false;
        canMove = false;
        isDead = true;
        PauseMenu.SetActive(true);
        paused = true;
    }

    public void UnPause()
    {
        canFire = _fire;
        canSprint = _sprint;
        canJump = _jump;
        canMove = _move;
        isDead = _dead;
        PauseMenu.SetActive(false);
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
