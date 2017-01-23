using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public CharacterController cc;

	public float speed=5f;
	public float angleSpeed=5f;

	public Animator an;

    public ParticleSystem pa;

    public Color l0,l1, l2, l3;

    public Material top, bot;

	// Use this for initialization
	void Start () 
	{
		cc = this.gameObject.GetComponent<CharacterController> ();	
		an = this.gameObject.GetComponent<Animator> ();
        
        top.SetColor("_Color", l3);
        bot.SetColor("_Color", l3);
    }

    void OnDestroy()
    {
        top.SetColor("_Color", l3);
        bot.SetColor("_Color", l3);
    }

    public void updateColor(int lives)
    {
        if (lives == 2)
        {
            top.SetColor("_Color", l2);
            bot.SetColor("_Color", l2);
        }
        else if (lives == 1)
        {
            top.SetColor("_Color", l1);
            bot.SetColor("_Color", l1);
        }
        else if (lives == 0)
        {
            top.SetColor("_Color", l0);
            bot.SetColor("_Color", l0);
        }
        else if (lives == 3)
        {
            top.SetColor("_Color", l3);
            bot.SetColor("_Color", l3);
        }
    }

    public void Hit()
    {
        pa.Play();
        GameObject.FindObjectOfType<AudioManager>().hitSound();
        updateColor(GameManager.lives - 1);
    }

    // Update is called once per frame
    void Update()
    {
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");

		Vector3 newMove = new Vector3 (x, 0, z);
	
		newMove *= Time.deltaTime;
		newMove *= speed;

		cc.Move (newMove);

		if (x != 0 || z != 0) 
		{
			an.SetBool ("Walk", true);

			Quaternion newRot = Quaternion.LookRotation (newMove);

			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, newRot, Time.deltaTime * angleSpeed);		
		}
		else 
		{
			an.SetBool ("Walk", false);		
		}

	}


}
