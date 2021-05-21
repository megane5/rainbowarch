using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManage : MonoBehaviour {
    [SerializeField]
    private AudioClip water;
    string state="NORMAL";
    Animator animator_;
    // Use this for initialization
    void Start () {
        animator_ = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (state == "DELETING" && animator_.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && GetComponent<BoxCollider2D>().enabled)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }


    }

    public void Delete()
    {
        GetComponent<AudioSource>().PlayOneShot(water);
        animator_.SetBool("DELETING", true);
        state = "DELETING";
    }
}
