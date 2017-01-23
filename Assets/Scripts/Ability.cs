using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {

    public GameManager gm;

    public Transform play;

	// Use this for initialization
	void Start ()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        play = GameObject.FindObjectOfType<Player>().transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, play.position, Time.deltaTime);
	}

    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Player")
        {
            gm.PickedUp();
            Destroy(this.gameObject);
        }
    }
}
