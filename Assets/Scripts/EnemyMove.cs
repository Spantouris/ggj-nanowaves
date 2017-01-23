using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	public float speed = 5f;

	public Vector3 target;

	public Vector3 dire;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        this.transform.Rotate(0, 10, 0);
		//this.transform.LookAt (target);
		if (Vector3.Distance (this.transform.position, target) < 2) {
			Destroy (this.gameObject);
		}
		this.transform.position += dire*speed*Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
            if (GameManager.invulnerable)
            {
                return;
            }
            other.gameObject.GetComponent<Player>().Hit();
			GameManager.lives--;
			Destroy (this.gameObject);		
		}
	}
}
