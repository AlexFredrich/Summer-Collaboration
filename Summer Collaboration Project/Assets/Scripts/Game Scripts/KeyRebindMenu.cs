using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]

//READ: STEPS 1-7 FOR ADDING A NEW INPUT WILL BE NUMBERED ON GAMEMANAGER.CS AND KEYREBINDMENU.CS

//This script goes on the menu canvas
public class KeyRebindMenu : MonoBehaviour
{
    #region Variables

    private Transform _keyRebindPanel;
    private Event _keyEvent;
    private KeyCode _newKey;
    private Text _buttonText;

    private bool _isWaitingForKey;
    private bool _cursorIsLocked;   //TODO: move to main menu script

    //STEP #3: COPY THE KEY NAME STRING FROM GAMEMANAGER.CS

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
        _keyRebindPanel = this.gameObject.transform.Find("Key Rebind Panel");   //the key rebind panel must be a child of the canvas this script is attached to and must be named "Key Rebind Panel"

        _keyEvent = null;
        _newKey = KeyCode.Space;
        _buttonText = null;

        _isWaitingForKey = false;
        _cursorIsLocked = false;    //TODO: move to main menu script

        _keyRebindPanel.gameObject.SetActive(false);

        /* Lock cursor */
        ChangeCursorLock(); //TODO: move to main menu script
    }

    private void Start()
    {
        Transform buttonGroup = _keyRebindPanel.gameObject.transform.Find("Buttons");

        //STEP #4: ADD A CASE FOR THE NEW KEY

        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            switch (buttonGroup.GetChild(i).name)
            {
                case FORWARDKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.ForwardButton.ToString());
                    break;
                case BACKWARDKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.BackwardButton.ToString());
                    break;
                case LEFTKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.LeftButton.ToString());
                    break;
                case RIGHTKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.RightButton.ToString());
                    break;
                case JUMPKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.JumpButton.ToString());
                    break;
                case SPRINTKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.SprintButton.ToString());
                    break;
                case PAUSEKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.PauseButton.ToString());
                    break;
                default:
                    break;
            }
        }
    }

    //TODO: move to main menu script
    private void Update()
    {
        if (Input.GetKeyDown(GameManager.Instance.PauseButton))
        {
            /* Toggle cursor */
            ChangeCursorLock();

            /* Toggle menu */
            _keyRebindPanel.gameObject.SetActive(!_keyRebindPanel.gameObject.activeSelf);
        }
    }

    //TODO: move to main menu script
    private void ChangeCursorLock()
    {
        /* Toggles the cursor on and off */
        if (_cursorIsLocked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            _cursorIsLocked = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _cursorIsLocked = true;
        }
    }

    private void OnGUI()
    {
        /* When the key rebind assignment is in progress, the next key pressed is saved */
        _keyEvent = Event.current;

        if (_keyEvent.shift && _isWaitingForKey)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _newKey = KeyCode.LeftShift;
            }
            else if (Input.GetKey(KeyCode.RightShift))
            {
                _newKey = KeyCode.RightShift;
            }

            _isWaitingForKey = false;
        }
        else if (_keyEvent.isKey && _isWaitingForKey)
        {
            _newKey = _keyEvent.keyCode;

            _isWaitingForKey = false;
        }
        else if (_isWaitingForKey)  //need to individually check for each controller button if there is no keyboard input
        {
            if (Input.GetKey(KeyCode.JoystickButton0))
            {
                _newKey = KeyCode.JoystickButton0;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton1))
            {
                _newKey = KeyCode.JoystickButton1;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton2))
            {
                _newKey = KeyCode.JoystickButton2;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton3))
            {
                _newKey = KeyCode.JoystickButton3;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton4))
            {
                _newKey = KeyCode.JoystickButton4;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton5))
            {
                _newKey = KeyCode.JoystickButton5;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton6))
            {
                _newKey = KeyCode.JoystickButton6;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton7))
            {
                _newKey = KeyCode.JoystickButton7;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton8))
            {
                _newKey = KeyCode.JoystickButton8;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton9))
            {
                _newKey = KeyCode.JoystickButton9;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton10))
            {
                _newKey = KeyCode.JoystickButton10;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton11))
            {
                _newKey = KeyCode.JoystickButton11;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton12))
            {
                _newKey = KeyCode.JoystickButton12;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton13))
            {
                _newKey = KeyCode.JoystickButton13;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton14))
            {
                _newKey = KeyCode.JoystickButton14;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton15))
            {
                _newKey = KeyCode.JoystickButton15;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton16))
            {
                _newKey = KeyCode.JoystickButton16;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton17))
            {
                _newKey = KeyCode.JoystickButton17;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton18))
            {
                _newKey = KeyCode.JoystickButton18;

                _isWaitingForKey = false;
            }
            else if (Input.GetKey(KeyCode.JoystickButton19))
            {
                _newKey = KeyCode.JoystickButton19;

                _isWaitingForKey = false;
            }
        }
    }

    /// <summary>
    /// Called on button click to set active button text.
    /// </summary>
    /// <param name="text"></param>
    public void SendText(Text text)
    {
        /* Saves a reference to the clicked on button's text */
        _buttonText = text;
    }

    /// <summary>
    /// Called on button click to start key rebinding.
    /// </summary>
    /// <param name="keyName"></param>
    public void StartKeyAssignment(string keyName)
    {
        /* Starts key assignment if not already doing so */
        if (!_isWaitingForKey)
        {
            StartCoroutine(AssignKeyName(keyName));
        }
    }
    
    private IEnumerator AssignKeyName(string keyName)
    {
        _isWaitingForKey = true;

        yield return WaitForKey();

        //STEP #5: ADD A CASE FOR THE NEW KEY

        /* Updates the rebind menu button text and PlayerPrefs with the new key */
        switch (keyName)
        {
            case FORWARDKEYNAME:
                GameManager.Instance.ForwardButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.ForwardButton.ToString());
                PlayerPrefs.SetString(FORWARDKEYNAME, GameManager.Instance.ForwardButton.ToString());
                break;
            case BACKWARDKEYNAME:
                GameManager.Instance.BackwardButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.BackwardButton.ToString());
                PlayerPrefs.SetString(BACKWARDKEYNAME, GameManager.Instance.BackwardButton.ToString());
                break;
            case LEFTKEYNAME:
                GameManager.Instance.LeftButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.LeftButton.ToString());
                PlayerPrefs.SetString(LEFTKEYNAME, GameManager.Instance.LeftButton.ToString());
                break;
            case RIGHTKEYNAME:
                GameManager.Instance.RightButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.RightButton.ToString());
                PlayerPrefs.SetString(RIGHTKEYNAME, GameManager.Instance.RightButton.ToString());
                break;
            case JUMPKEYNAME:
                GameManager.Instance.JumpButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.JumpButton.ToString());
                PlayerPrefs.SetString(JUMPKEYNAME, GameManager.Instance.JumpButton.ToString());
                break;
            case SPRINTKEYNAME:
                GameManager.Instance.SprintButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.SprintButton.ToString());
                PlayerPrefs.SetString(SPRINTKEYNAME, GameManager.Instance.SprintButton.ToString());
                break;
            case PAUSEKEYNAME:
                GameManager.Instance.PauseButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.PauseButton.ToString());
                PlayerPrefs.SetString(PAUSEKEYNAME, GameManager.Instance.PauseButton.ToString());
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitForKey()
    {
        /* Waits for the user to input a new key */
        while (_isWaitingForKey)
        {
            yield return null;
        }
    }

    private string SplitPascalCase(string originalString)
    {
        /* Adds a space before capitalized letters of Pascal case strings */
        return System.Text.RegularExpressions.Regex.Replace(originalString, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
    }

    //STEP #6: ADD A NEW BUTTON AND TEXT TO THE KEY REBIND UI PANEL (THE BUTTON MUST BE NAMED THE SAME AS THE KEY NAME STRING CONSTANT - E.G. ForwardKey)
    //STEP #7: SET UP BUTTON ONCLICK() EVENTS FOR KEYREBINDMENU.SENDTEXT (TEXT SHOULD BE THAT BUTTON'S TEXT) AND KEYREBINDMENU.STARTKEYASSIGNMENT (STRING SHOULD BE THAT BUTTON'S NAME / KEY NAME STRING CONSTANT - E.G. ForwardKey)
}
