using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TextFlash : MonoBehaviour
{

    [SerializeField]
    private float angularFrequency = 5f;
    //static readonly float DeltaTime = 0.0166f;
    private Text text;
    private float time = 0.0f;

    void Start()
    {

        text = GetComponent<Text>();
       
    }

    private void Update()
    {
        time += angularFrequency * Time.deltaTime;
        var color = text.color;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;
        text.color = color;
    }
}