using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManage : MonoBehaviour {
    [SerializeField]
    private Sprite getted;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Delete()
    {
        GetComponent<SpriteRenderer>().sprite = getted;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
