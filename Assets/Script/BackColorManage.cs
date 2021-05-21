using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackColorManage : MonoBehaviour {
    public string color;
    public string color2;
    private int state { get; set; }
    public Sprite colorSprite_;
    public Sprite colorSprite2_;
    public Sprite monoSprite_;
    SpriteRenderer spriteRenderer_;
    // Use this for initialization
    void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = monoSprite_;
        state = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void coloring()
    {
        if (spriteRenderer_ == null)
            spriteRenderer_ = gameObject.GetComponent<SpriteRenderer>();
        if(state==0)
            spriteRenderer_.sprite = colorSprite_;
        else
        {
            spriteRenderer_.sprite = colorSprite2_;
        }
        state++;
        //this.transform.position += new Vector3(0, (onFireTexture_.bounds.size.y - normalTexture_.bounds.size.y) / 2, 0);
    }
}
