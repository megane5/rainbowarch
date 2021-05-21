using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyManage : MonoBehaviour
{
    [SerializeField]
    private AudioClip tree;
    public bool grown_ { get; set; }
    public bool stop_ { get; set; }
    SpriteRenderer spriteRenderer_;
    BoxCollider2D stopper_;
    IvyStopperManager stopperCollision_;
    Animator animator_;
    public Sprite normalTexture_;
    public Sprite grownTexture_;
    private float offset_;

    // Use this for initialization
    void Start () {
        stopper_ = gameObject.transform.Find("Stopper").gameObject.GetComponent<BoxCollider2D>();
        stopperCollision_ = gameObject.transform.Find("Stopper").gameObject.GetComponent<IvyStopperManager>();
        spriteRenderer_ = gameObject.GetComponent<SpriteRenderer>();
        offset_ = (grownTexture_.bounds.size.y - normalTexture_.bounds.size.y)/2;
        animator_ = GetComponent<Animator>();
        grown_ = false;
    }

   
	
	// Update is called once per frame
	void Update () {
        stop_ = stopperCollision_.GetStop();
	}

    public void grow()
    {
        if (!grown_)
        {
            if (spriteRenderer_ == null)
                spriteRenderer_ = gameObject.GetComponent<SpriteRenderer>();
            grown_ = true;
            GetComponent<AudioSource>().PlayOneShot(tree);
            animator_.SetBool("GROW", true);
            spriteRenderer_.sprite = grownTexture_;
            GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y * (float)8.0);
            GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<BoxCollider2D>().offset.x, 0);
            stopper_.offset = new Vector2(stopper_.offset.x, GetComponent<BoxCollider2D>().size.y/2);
            //this.transform.position += new Vector3(0, (grownTexture_.bounds.size.y - normalTexture_.bounds.size.y) / 2, 0);
        }
    }            
}
