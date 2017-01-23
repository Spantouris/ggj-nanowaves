using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip burn, ding;

    public AudioClip heart, pill, shield, slow;

    public AudioClip loop;

    public AudioSource ads;

    public AudioSource ambient;

    public GameManager gm;

    public float beforeLoop = 4f;

    private float current = 0f;

    private bool played = false;

	// Use this for initialization
	void Start ()
    {
        ads = this.gameObject.GetComponent<AudioSource>();
        gm = this.gameObject.GetComponent<GameManager>();
    }

    public void hitSound()
    {
        if (gm.lost)
        {
            return;
        }
        ads.clip = burn;
        ads.Play();
    }

    public void Heart()
    {
        ads.clip = heart;
        ads.Play();
    }

    public void Pill()
    {
        ads.clip = pill;
        ads.Play();
    }

    public void Shield()
    {
        ads.clip = shield;
        ads.Play();
    }

    public void Slow()
    {
        ads.clip = slow;
        ads.Play();
    }

    public void lostSound()
    {
        ambient.Stop();
        ads.Stop();
        ads.clip = ding;
        ads.Play();
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
            ambient.clip = loop;
            ambient.loop = true;
            ambient.Play();
        }
	}
}
