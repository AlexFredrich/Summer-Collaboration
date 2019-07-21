using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]

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

    private const string FORWARDKEYNAME = "ForwardKey";
    private const string BACKWARDKEYNAME = "BackwardKey";
    private const string LEFTKEYNAME = "LeftKey";
    private const string RIGHTKEYNAME = "RightKey";
    private const string JUMPKEYNAME = "JumpKey";
    private const string SPRINTKEYNAME = "SprintKey";
    private const string PAUSEKEYNAME = "PauseKey";

    #endregion

    //TODO: improve code
    //TODO: add comments
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

        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            switch (buttonGroup.GetChild(i).name)
            {
                case FORWARDKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.MNKForwardButton.ToString();
                    break;
                case BACKWARDKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.MNKBackwardButton.ToString();
                    break;
                case LEFTKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.MNKLeftButton.ToString();
                    break;
                case RIGHTKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.MNKRightButton.ToString();
                    break;
                case JUMPKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.MNKJumpButton.ToString();
                    break;
                case SPRINTKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.MNKSprintButton.ToString();
                    break;
                case PAUSEKEYNAME:
                    buttonGroup.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.MNKPauseButton.ToString();
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
        _buttonText = text;
    }

    /// <summary>
    /// Called on button click to start key rebinding.
    /// </summary>
    /// <param name="keyName"></param>
    public void StartKeyAssignment(string keyName)
    {
        if (!_isWaitingForKey)
        {
            StartCoroutine(AssignKeyName(keyName));
        }
    }
    
    private IEnumerator AssignKeyName(string keyName)
    {
        _isWaitingForKey = true;

        yield return WaitForKey();

        switch (keyName)
        {
            case FORWARDKEYNAME:
                GameManager.Instance.MNKForwardButton = _newKey;
                _buttonText.text = GameManager.Instance.MNKForwardButton.ToString();
                PlayerPrefs.SetString(FORWARDKEYNAME, GameManager.Instance.MNKForwardButton.ToString());
                break;
            case BACKWARDKEYNAME:
                GameManager.Instance.MNKBackwardButton = _newKey;
                _buttonText.text = GameManager.Instance.MNKBackwardButton.ToString();
                PlayerPrefs.SetString(BACKWARDKEYNAME, GameManager.Instance.MNKBackwardButton.ToString());
                break;
            case LEFTKEYNAME:
                GameManager.Instance.MNKLeftButton = _newKey;
                _buttonText.text = GameManager.Instance.MNKLeftButton.ToString();
                PlayerPrefs.SetString(LEFTKEYNAME, GameManager.Instance.MNKLeftButton.ToString());
                break;
            case RIGHTKEYNAME:
                GameManager.Instance.MNKRightButton = _newKey;
                _buttonText.text = GameManager.Instance.MNKRightButton.ToString();
                PlayerPrefs.SetString(RIGHTKEYNAME, GameManager.Instance.MNKRightButton.ToString());
                break;
            case JUMPKEYNAME:
                GameManager.Instance.MNKJumpButton = _newKey;
                _buttonText.text = GameManager.Instance.MNKJumpButton.ToString();
                PlayerPrefs.SetString(JUMPKEYNAME, GameManager.Instance.MNKJumpButton.ToString());
                break;
            case SPRINTKEYNAME:
                GameManager.Instance.MNKSprintButton = _newKey;
                _buttonText.text = GameManager.Instance.MNKSprintButton.ToString();
                PlayerPrefs.SetString(SPRINTKEYNAME, GameManager.Instance.MNKSprintButton.ToString());
                break;
            case PAUSEKEYNAME:
                GameManager.Instance.MNKPauseButton = _newKey;
                _buttonText.text = GameManager.Instance.MNKPauseButton.ToString();
                PlayerPrefs.SetString(PAUSEKEYNAME, GameManager.Instance.MNKPauseButton.ToString());
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitForKey()
    {
        while (!_keyEvent.isKey)
        {
            yield return null;
        }
    }
}
