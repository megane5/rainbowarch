using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchManage : MonoBehaviour {
    private bool conected_=false;
    public bool onFire_ { get; set; }
    public int number { set; get; }

    AudioSource audio_;
    public bool conected
    {
        set { conected_ = value;}
        get { return conected_; }
    }


    SpriteRenderer spriteRenderer_;
    public Sprite normalTexture_;
    public Sprite onFireTexture_; 

	void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = normalTexture_;
        audio_ = GetComponent<AudioSource>();
        onFire_ = false;
    }
	
	void Update () {
		
	}

    public bool GetOnFire()
    {
        return onFire_;
    }

    public void setFire()
    {
        if (!onFire_)
        {
            audio_.PlayOneShot(audio_.clip);
            if (spriteRenderer_ == null)
                spriteRenderer_ = gameObject.GetComponent<SpriteRenderer>();
            onFire_ = true;
            spriteRenderer_.sprite = onFireTexture_;
            this.transform.position += new Vector3(0, (onFireTexture_.bounds.size.y - normalTexture_.bounds.size.y) / 2, 0);
        }
    }
    
}
