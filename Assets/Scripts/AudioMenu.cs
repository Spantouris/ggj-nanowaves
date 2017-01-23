using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour {
    
    public AudioClip loop;

    public AudioSource music;

    public float beforeLoop = 4f;

    private float current = 0f;

    private bool played = false;

    // Use this for initialization
    void Start ()
    {
        music = this.gameObject.GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!played)
        {
            current += Time.deltaTime;
        }

        if (current >= beforeLoop && !played)
        {
            played = true;
            music.clip = loop;
            music.loop = true;
            music.Play();
        }
    }
}
