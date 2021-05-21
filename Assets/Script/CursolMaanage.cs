using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursolMaanage : MonoBehaviour {
    [SerializeField]
    private AudioClip select, decide;
    public GameObject startObject_;
    public GameObject exitObject_;
    private float distance;
    Transform transform_;
	// Use this for initialization
	void Start () {
        transform_ = GetComponent<Transform>();
        transform_.position = new Vector3(transform_.position.x, startObject_.transform.position.y, 0);
        distance = startObject_.transform.position.y - exitObject_.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            if (isStart())
            {
                transform_.position= new Vector3(transform_.position.x, startObject_.transform.position.y- distance, 0);
            }
            else
            {
                transform_.position = new Vector3(transform_.position.x, startObject_.transform.position.y, 0);
            }
            GetComponent<AudioSource>().PlayOneShot(select);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isStart())
            {
                SceneManager.LoadScene("game");
            }
            else
            {
                Application.Quit();
            }
            GetComponent<AudioSource>().PlayOneShot(decide);
        }
	}

    bool isStart()
    {
        if(transform_.position.y== startObject_.transform.position.y)
        {
            return true;
        }
        return false;
    }
}
