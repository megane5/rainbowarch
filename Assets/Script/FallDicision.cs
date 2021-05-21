using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDicision : MonoBehaviour {
    private bool onFall_;
    GameObject colGameObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public bool GetOnFall()
    {
        return onFall_;
    }
    public GameObject GetGameObject()
    {
        return colGameObject;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "FALL")
        {
            onFall_ = false;
            colGameObject = null;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "FALL")
        {
            onFall_ = true;
            colGameObject = col.gameObject;
        }
    }
}
