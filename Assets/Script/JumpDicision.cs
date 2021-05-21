using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDicision : MonoBehaviour
{
    private bool onGround_;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetOnGround()
    {
        return onGround_;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            onGround_ = false;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            onGround_ = true;
        }
    }
}   