using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

//READ: STEPS 1-7 FOR ADDING A NEW INPUT WILL BE NUMBERED ON GAMEMANAGER.CS AND KEYREBINDMENU.CS

//This script goes on an empty gameobject
public class GameManager : MonoBehaviour
{
    #region Variables

    public static GameManager Instance { get; private set; }

    //STEP #1: ADD A NEW KEYCODE AND KEY NAME STRING FOR THE KEY

    public KeyCode ForwardButton { get; set; }
    public KeyCode BackwardButton { get; set; }
    public KeyCode LeftButton { get; set; }
    public KeyCode RightButton { get; set; }
    public KeyCode JumpButton { get; set; }
    public KeyCode SprintButton { get; set; }
    public KeyCode PauseButton { get; set; }

    private const string FORWARDKEYNAME = "ForwardKey";
    private const string BACKWARDKEYNAME = "BackwardKey";
    private const string LEFTKEYNAME = "LeftKey";
    private const string RIGHTKEYNAME = "RightKey";
    private const string JUMPKEYNAME = "JumpKey";
    private const string SPRINTKEYNAME = "SprintKey";
    private const string PAUSEKEYNAME = "PauseKey";

    #endregion

    private void Awake()
    {
        /* Allows only a single instance of this script */
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }

        //STEP #2: ASSIGN DEFAULT VALUE FOR KEY

        // KeyCode list for default keys: https://docs.unity3d.com/ScriptReference/KeyCode.html
        ForwardButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(FORWARDKEYNAME, "W"));
        BackwardButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(BACKWARDKEYNAME, "S"));
        LeftButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(LEFTKEYNAME, "A"));
        RightButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(RIGHTKEYNAME, "D"));
        JumpButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(JUMPKEYNAME, "Space"));
        SprintButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(SPRINTKEYNAME, "LeftShift"));
        PauseButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(PAUSEKEYNAME, "Escape"));
    }
}
