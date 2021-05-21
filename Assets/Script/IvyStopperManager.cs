using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyStopperManager : MonoBehaviour {

    private bool stop_;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetStop()
    {
        return stop_;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            stop_ = false;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            stop_ = true;
        }
    }
}
