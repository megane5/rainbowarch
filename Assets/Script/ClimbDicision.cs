using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbDicision : MonoBehaviour {
    private bool onLadder_;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool GetOnLadders()
    {
        return onLadder_;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ladders")
        {
            onLadder_ = false;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ladders")
        {
            onLadder_ = true;
        }
    }
}
