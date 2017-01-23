using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class GameManager : MonoBehaviour 
{
	public static int lives = 3;

    [Header("Level Variables")]
    public float[] speeds;

	public Spawner[] spawns;

    public int level = 0;

    public bool stop = false;

    public float timeForTimeUpdate = 15f;

    private float currentTimeForTime = 0;

    [Header("Score Variables")]
	public int score = 0;

	public float scoreTimer = 1f;

	private float currScoreTimer = 0;

    [Header("Game Variable")]
    public bool lost = false;
   
    private UIHandler ui;

    private AudioManager am;

    [Header("Abilities")]
    public float abilityTimer = 20f;

    private float currAbilityTimer = 0f;

    public bool spawned = false;

    public float durationTimer = 5f;

    private float currDurationTimer = 0f;

    public float leaveTimer = 5f;

    private float currLeaveTimer = 0f;

    public bool picked = false;
    
    public int whichAbility;

    public GameObject[] ability;

    private GameObject abGo;

    public static bool invulnerable = false;

    private float prevSpeed = 20f;

    [Header("Materials")]
    public Color prevColor;
    public Color invuColor;

    public Material top, bot;

    public MotionBlur mb;

    // Use this for initialization
    void Awake ()
    {
        Time.timeScale = 1;
        lives = 3;
        invulnerable = false;

        top.SetColor("_EmissionColor", prevColor);
        bot.SetColor("_EmissionColor", prevColor);

        ui = this.gameObject.GetComponent<UIHandler>();
        am = this.gameObject.GetComponent<AudioManager>();
	}

    void Start()
    {
        abilityTimer = Random.Range(10f, 30f);
    }

    void OnDestroy()
    {
        top.SetColor("_EmissionColor", prevColor);
        bot.SetColor("_EmissionColor", prevColor);
    }

    public void PickedUp()
    {
        picked = true;
        if (whichAbility == 0)
        {
            am.Shield();
            ui.enableAbility(0);
            invulnerable = true;
            top.SetColor("_EmissionColor", invuColor);
            bot.SetColor("_EmissionColor", invuColor);
        }
        else if (whichAbility == 1)
        {
            am.Slow();
            ui.enableAbility(1);
            foreach (EnemyMove x in GameObject.FindObjectsOfType<EnemyMove>())
            {
                x.speed = 6;
            }
            foreach (Spawner x in spawns)
            {
                x.enemySpeed = 6;
            }
        }
        else if (whichAbility == 2)
        {
            am.Heart();
            if (lives < 3 && lives > 0)
            {
                lives++;
                GameObject.FindObjectOfType<Player>().updateColor(lives);
            }
        }
        else if (whichAbility == 3)
        {
            am.Pill();

            ui.enableAbility(2);

            mb.enabled = true;
        }
    }

    void disableAbility()
    {
        ui.disableAbility();
        if (whichAbility == 0)
        {
            invulnerable = false;
            top.SetColor("_EmissionColor", prevColor);
            bot.SetColor("_EmissionColor", prevColor);
        }
        else if (whichAbility == 1)
        {
            foreach (EnemyMove x in GameObject.FindObjectsOfType<EnemyMove>())
            {
                x.speed = prevSpeed;
            }

            foreach (Spawner x in spawns)
            {
                x.enemySpeed = prevSpeed;
            }
        }else if(whichAbility == 3)
        {
            mb.enabled = false;
        }
    }

    void abilities()
    {
        if (!spawned && !picked)
        {
            currAbilityTimer += Time.deltaTime;
        }
        else if (!picked)
        {
            currLeaveTimer += Time.deltaTime;
        }
        else
        {
            currDurationTimer += Time.deltaTime;
        }

        if (currAbilityTimer >= abilityTimer && !spawned && !picked)
        {
            spawned = true;

            whichAbility = Random.Range(0, ability.Length);

            Vector3 spawnPos = Vector3.zero;

            if (whichAbility == 0)
            {
                spawnPos = new Vector3(Random.Range(-10f, 10f), 4f, Random.Range(-10f, 10f));
            }
            else
            {
                spawnPos = new Vector3(Random.Range(-10f, 10f), 2f, Random.Range(-10f, 10f));
            }

            abGo = (GameObject)Instantiate(ability[whichAbility], spawnPos, Quaternion.identity);
        }
        else if (currLeaveTimer >= leaveTimer && spawned && !picked)
        {
            spawned = false;
            currLeaveTimer = 0;
            currAbilityTimer = 0;
            abilityTimer = Random.Range(10f, 30f);
            Destroy(abGo);
        }
        else if (currDurationTimer >= durationTimer && picked && spawned)
        {
            spawned = false;
            picked = false;
            currLeaveTimer = 0;
            currAbilityTimer = 0;
            currDurationTimer = 0;
            abilityTimer = Random.Range(10f, 30f);
            disableAbility();
        }

    }

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (0);
		}

        abilities();

		if (lives <= 0 && !lost) {
            lost = true;
            am.lostSound();
            ui.Lose();
            //SceneManager.LoadScene (0);
		}

		currScoreTimer += Time.deltaTime;

		if (currScoreTimer >= scoreTimer && !lost) 
		{
			score += 1 + level * 10;	
			currScoreTimer = 0;
            ui.updateScore(score);
		}

		if (!stop) {
			currentTimeForTime += Time.deltaTime;	
		}
		else {
			changeSpeed (40);
		}
	
		if (currentTimeForTime >= timeForTimeUpdate && !stop) 
		{
			level++;
			changeTime ();
			if (level == 5) {
				stop = true;
			}
			timeForTimeUpdate = (level+1) * 15f;
			currentTimeForTime = 0;
		}
	}

	public void changeSpeed(float speed)
	{
        prevSpeed = speed;
		foreach (Spawner x in spawns) {
			x.enemySpeed = speed;		
		}
	}

	public void changeTime()
	{
		foreach (Spawner x in spawns) {
			x.timeMax = speeds [level - 1];		
		}
	}
}
