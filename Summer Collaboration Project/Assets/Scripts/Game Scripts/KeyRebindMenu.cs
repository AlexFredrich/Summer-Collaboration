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
    private bool _cursorIsLocked;

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
        _keyRebindPanel = this.gameObject.transform.Find("Key Rebind Panel");

        _isWaitingForKey = false;
        _cursorIsLocked = false;

        _keyRebindPanel.gameObject.SetActive(false);

        /* Lock cursor */
        ChangeCursorLock();
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
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.MNKForwardButton.ToString());
                    break;
                case BACKWARDKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.MNKBackwardButton.ToString());
                    break;
                case LEFTKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.MNKLeftButton.ToString());
                    break;
                case RIGHTKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.MNKRightButton.ToString());
                    break;
                case JUMPKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.MNKJumpButton.ToString());
                    break;
                case SPRINTKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.MNKSprintButton.ToString());
                    break;
                case PAUSEKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = SplitPascalCase(GameManager.Instance.MNKPauseButton.ToString());
                    break;
                default:
                    break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameManager.Instance.MNKPauseButton))
        {
            /* Toggle cursor */
            ChangeCursorLock();
            
            /* Toggle menu */
            _keyRebindPanel.gameObject.SetActive(!_keyRebindPanel.gameObject.activeSelf);
        }
    }
    
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

        if (_keyEvent.isKey && _isWaitingForKey)
        {
            _newKey = _keyEvent.keyCode;

            _isWaitingForKey = false;
        }
    }

    /// <summary>
    /// Called on button click to set active button text.
    /// </summary>
    /// <param name="text"></param>
    public void SendText(Text text)
    {
        /* Saves reference to current button's text */
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
                GameManager.Instance.MNKForwardButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.MNKForwardButton.ToString());
                PlayerPrefs.SetString(FORWARDKEYNAME, GameManager.Instance.MNKForwardButton.ToString());
                break;
            case BACKWARDKEYNAME:
                GameManager.Instance.MNKBackwardButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.MNKBackwardButton.ToString());
                PlayerPrefs.SetString(BACKWARDKEYNAME, GameManager.Instance.MNKBackwardButton.ToString());
                break;
            case LEFTKEYNAME:
                GameManager.Instance.MNKLeftButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.MNKLeftButton.ToString());
                PlayerPrefs.SetString(LEFTKEYNAME, GameManager.Instance.MNKLeftButton.ToString());
                break;
            case RIGHTKEYNAME:
                GameManager.Instance.MNKRightButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.MNKRightButton.ToString());
                PlayerPrefs.SetString(RIGHTKEYNAME, GameManager.Instance.MNKRightButton.ToString());
                break;
            case JUMPKEYNAME:
                GameManager.Instance.MNKJumpButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.MNKJumpButton.ToString());
                PlayerPrefs.SetString(JUMPKEYNAME, GameManager.Instance.MNKJumpButton.ToString());
                break;
            case SPRINTKEYNAME:
                GameManager.Instance.MNKSprintButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.MNKSprintButton.ToString());
                PlayerPrefs.SetString(SPRINTKEYNAME, GameManager.Instance.MNKSprintButton.ToString());
                break;
            case PAUSEKEYNAME:
                GameManager.Instance.MNKPauseButton = _newKey;
                _buttonText.text = SplitPascalCase(GameManager.Instance.MNKPauseButton.ToString());
                PlayerPrefs.SetString(PAUSEKEYNAME, GameManager.Instance.MNKPauseButton.ToString());
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitForKey()
    {
        /* Waits for the user to input a new key */
        while (!_keyEvent.isKey)
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
