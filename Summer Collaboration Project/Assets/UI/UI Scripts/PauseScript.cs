using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    public GameObject WatchMenu;
    public GameObject Player;
    public Camera PlayerCamera;
    public Camera WatchCamera;


    // Start is called before the first frame update
    void Start()
    {
        WatchMenu.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {

            //PlayerCamera.gameObject.SetActive(false);
            Player.gameObject.SetActive(false);
            WatchMenu.gameObject.SetActive(true);

        }
    }
}
