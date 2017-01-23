using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float min;
	public float max;

	public Vector3 basic;

	public float enemySpeed =5;

	public int xyz=0;

	public Vector3 rot;

	public float timerUntilSpawn;

	private float currentTimer;

	public GameObject enemy;

	public Vector3 dire = Vector3.zero;

	public float timeMax = 3.5f;

	// Use this for initialization
	void Start () 
	{
		float r = Random.Range (0f, timeMax);

		timerUntilSpawn = r;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentTimer += Time.deltaTime;

		if (currentTimer >= timerUntilSpawn) {
			spawn ();

			float r = Random.Range (0f, timeMax);

			timerUntilSpawn = r;

			currentTimer = 0;	
		}
	}

	void spawn()
	{
		Vector3 newMove = Vector3.zero;

		Vector3 target = Vector3.zero;


		if(xyz==1)
		{
			float x = Random.Range(min,max);
			newMove = new Vector3(x,basic.y,basic.z);
			target = new Vector3(x,basic.y,-basic.z);
		}else if(xyz==3)
		{
			float z = Random.Range(min,max);
			newMove = new Vector3(basic.x,basic.y,z);
			target = new Vector3(-basic.x,basic.y,z);
		}
		//Debug.Log (newMove + " " + target);
		GameObject obj = (GameObject)Instantiate (enemy, newMove,Quaternion.Euler(rot));

		EnemyMove em = obj.GetComponent<EnemyMove> ();
		em.target = target;
		em.dire = dire;
		em.speed = enemySpeed;
	}

}
