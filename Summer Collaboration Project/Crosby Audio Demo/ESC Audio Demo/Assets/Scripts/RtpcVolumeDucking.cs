using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RtpcVolumeDucking : MonoBehaviour
{
    public string VO_Playing_True;

    float value;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        int type = 1;

        

        AkSoundEngine.GetRTPCValue(VO_Playing_True, gameObject, 0, out value, ref type);

        if (other.gameObject.tag == "Player")
        {
            value = 10;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        value = 100;
        this.gameObject.SetActive(false);
    }
}
