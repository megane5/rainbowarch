using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClearTextManage : MonoBehaviour {
    [SerializeField]
    private GameObject player;
    private PlayerMove pm_;
    public float interval = 1.0f;
    private Text text;

    // Use this for initialization
    void Start () {
        pm_ = player.GetComponent<PlayerMove>();
        text = GetComponent<Text>();
        text.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (pm_.endcount == 10)
        {
            StartCoroutine("Blink");
        }
	}

    IEnumerator Blink()
    {
        while (true)
        {
            GetComponent<Text>().enabled = !this.GetComponent<Text>().enabled;
            yield return new WaitForSeconds(interval);
        }
    }
}
