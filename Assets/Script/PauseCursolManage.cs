using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCursolManage : MonoBehaviour {
    [SerializeField]
    private GameObject play;
    [SerializeField]
    private GameObject goToTitle;
    [SerializeField]
    private AudioClip select;
    [SerializeField]
    private AudioClip decide;
    AudioSource audio_;
    private int point = 0;

    Vector3 playPos_;
    Vector3 titlePos_;
    RectTransform transform_;
    public bool End { get; set; }
    void Start () {
        transform_ = this.GetComponent<RectTransform>();
        playPos_ = new Vector3(transform_.position.x,play.GetComponent<RectTransform>().position.y);
        titlePos_ = new Vector3(transform_.position.x, goToTitle.GetComponent<RectTransform>().position.y);
        transform_.position = playPos_;
        End = false;
        audio_ = GetComponent<AudioSource>();
	}
	
	void Update () {
        if(Time.timeScale == 0f)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.S))
            {
                audio_.PlayOneShot(select);
                if (point==0)
                {
                    transform_.position = titlePos_;
                }
                else
                {
                    transform_.position = playPos_;
                }
                point = (point + 1) % 2;
            }
            if(Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown(KeyCode.Z))
            {
                audio_.PlayOneShot(decide);
                if (point==0)
                {
                    End = true;
                }
                else
                {
                    Time.timeScale = 1f;
                    End = true;
                    transform_.position = playPos_;
                    SceneManager.LoadScene("title");
                }
            }
        }

    }
}
