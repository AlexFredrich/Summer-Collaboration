using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

//This script goes on an empty gameobject
public class GameManager : MonoBehaviour
{
    #region Variables

    public static GameManager Instance { get; private set; }

    public KeyCode MNKForwardButton { get; private set; }
    public KeyCode MNKBackwardButton { get; private set; }
    public KeyCode MNKLeftButton { get; private set; }
    public KeyCode MNKRightButton { get; private set; }
    public KeyCode MNKJumpButton { get; private set; }
    public KeyCode MNKSprintButton { get; private set; }
    public KeyCode MNKPauseButton { get; private set; }

    public KeyCode MNKMouseXAxis { get; private set; }
    public KeyCode MNKMouseYAxis { get; private set; }

    private const string FORWARDKEYNAME = "forwardKey";
    private const string BACKWARDKEYNAME = "backwardKey";
    private const string LEFTKEYNAME = "leftKey";
    private const string RIGHTKEYNAME = "rightKey";
    private const string JUMPKEYNAME = "jumpKey";
    private const string SPRINTKEYNAME = "sprintKey";
    private const string PAUSEKEYNAME = "cancelKey";

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

        MNKForwardButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(FORWARDKEYNAME, "W"));
        MNKBackwardButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(BACKWARDKEYNAME, "S"));
        MNKLeftButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(LEFTKEYNAME, "A"));
        MNKRightButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(RIGHTKEYNAME, "D"));
        MNKJumpButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(JUMPKEYNAME, "Space"));
        MNKSprintButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(SPRINTKEYNAME, "LeftShift"));
        MNKPauseButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(PAUSEKEYNAME, "Escape"));
    }
}
