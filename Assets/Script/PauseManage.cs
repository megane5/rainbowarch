using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManage : MonoBehaviour
{

    [SerializeField]
    private AudioClip cancel,decide;
    [SerializeField]
    private GameObject pauseUIPrefab;
    private GameObject pauseUIInstance;
    private GameObject cursol;
    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)||(cursol&&cursol.GetComponent<PauseCursolManage>().End))
        {
            if (pauseUIInstance == null)
            {
                GetComponent<AudioSource>().PlayOneShot(decide);
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                cursol = GameObject.Find("cursol");
                Time.timeScale = 0f;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape)){
                    GetComponent<AudioSource>().PlayOneShot(cancel);
                }
                Destroy(pauseUIInstance);
                cursol = null;
                Time.timeScale = 1f;
            }
        }
        if (pauseUIInstance)
        {
            
        }
    }
}
