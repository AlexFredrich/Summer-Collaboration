using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerRewind : MonoBehaviour
{
    [Tooltip("How far back in time the object can be reversed (in seconds). Negative value for infinite time.")]
    public float RewindTime = 10f;
    [Tooltip("If time should stay frozen after it has completely be reverted or not.")]
    public bool StayFrozen = true;
    [SerializeField] private GameObject hands;
    [SerializeField] private GameObject watch;

    private bool isRewinding = false;

    private void Start()
    {
        Animator anim = watch.GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartRewind();
        }
        if (Input.GetMouseButtonUp(1))
        {
            StopRewind(); //Add timer that goes up with time and goes down when RMB is held. When timer is 0, change the watch animation speed to 0
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        hands.GetComponent<Animator>().SetBool("IsRewinding", true);
        watch.GetComponent<Animator>().SetFloat("WatchRewind", -1);
    }

    public void StopRewind()
    {
        isRewinding = false;
        hands.GetComponent<Animator>().SetBool("IsRewinding", false);
        watch.GetComponent<Animator>().SetFloat("WatchRewind", 1);
    }
}
