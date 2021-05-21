using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterManage : MonoBehaviour {
    GameObject torch_;

    Animator animator_;
    // Use this for initialization
    void Start () {
        animator_ = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!torch_)
        {
            List<GameObject> objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Torch"));
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].GetComponent<TorchManage>().conected)
                {
                    objects.RemoveAt(i--);
                }
            }
            float min=10000f;
            int index=-1;
            Vector3 p1 = this.transform.position;
            for (int i=0;i<objects.Count;i++)
            {
                Vector3 p2 = objects[i].transform.position;
                if(min>Vector3.Distance(p1, p2))
                {
                    min = Vector3.Distance(p1, p2);
                    index = i;
                }
            }
            torch_ = objects[index];
            objects[index].GetComponent<TorchManage>().conected = true;
        }
        if (animator_.GetBool("OPEN") && animator_.GetCurrentAnimatorStateInfo(0).normalizedTime > 1&& animator_.GetCurrentAnimatorStateInfo(0).normalizedTime<2)
        {
            Destroy(gameObject);

        }
        if (torch_.GetComponent<TorchManage>().GetOnFire())
        {
            animator_.SetBool("OPEN", true);
        }

    }
}
