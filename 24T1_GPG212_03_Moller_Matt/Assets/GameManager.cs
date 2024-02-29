using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AudioSource bgm;
    // Start is called before the first frame update
    void Start()
    {
        bgm  = GetComponent<AudioSource>(); 
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
