using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManage : MonoBehaviour {
    SpriteRenderer renderer_;
	// Use this for initialization
	void Start () {
        renderer_ = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Drow(Vector3 vec3)
    {
        //Debug.Log("draw");
        transform.position = vec3;
        renderer_.enabled = true;
    }
    public void Delete()
    {
        renderer_.enabled = false;
    }
}
