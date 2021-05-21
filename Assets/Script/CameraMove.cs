using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public GameObject player_;
    private float offset_ = -30f;
    private float limit = 0f;

    
	void Start () {
        offset_ = transform.position.x - player_.transform.position.x;
	}
	
	void Update () {
        transform.position = new Vector3(player_.transform.position.x + offset_,0,-10);
        if (transform.position.x < limit)
        {
            transform.position = new Vector3(limit, 0, -10);
        }
	}
}
