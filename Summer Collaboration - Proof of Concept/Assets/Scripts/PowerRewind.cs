using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerRewind : MonoBehaviour
{
    [Tooltip("How far back in time the object can be reversed (in seconds). Negative value for infinite time.")]
    [SerializeField] private float rewindTime = 10f;
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
            StopRewind();
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
