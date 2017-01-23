using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour {

	public GameObject lives,lives1,lives2;
	public Text scores;

	public GameManager gm;

    public float tutorialTime = 5f;

    private float current = 0;

    public GameObject tutorial;

    public GameObject losePanel;

    public Animation an;

    public Text scoreLabelLose;

    private bool stop = false;

    public Slider abili;

    public Image abiImage;

    public Sprite[] abi;

    public bool abilityEnabled = false;

    public string username = "";

    public bool submitted = false;

    public Text subError ;

	// Use this for initialization
	void Start () 
	{
		gm = this.gameObject.GetComponent<GameManager> ();

        tutorial.SetActive(true);
	}

    public void updateScore(int score)
    {
        scores.text = "Your score\n" + score.ToString();
    }

    public void enableAbility(int which)
    {
        abiImage.gameObject.SetActive(true);
        abiImage.sprite = abi[which];

        abili.gameObject.SetActive(true);
        abili.value = 5;
        abilityEnabled = true;
    }

    public void disableAbility()
    {
        abiImage.gameObject.SetActive(false);

        abili.gameObject.SetActive(false);
        abili.value = 5;
        abilityEnabled = false;
    }

    public void changeUser(string user)
    {
        username = user;
    }

    public void submitScore()
    {
        if (submitted)
        {
            return;
        }
        Network cur = GameObject.FindObjectOfType<Network>();
        cur.submitScore(gm.score, username,subError);
        submitted = true;
    }

    public void Lose()
    {
        scores.text = "";
        tutorial.SetActive(false);
        losePanel.SetActive(true);
        an.Play();
        scoreLabelLose.text = gm.score.ToString();
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update () 
	{
        if (!stop)
        {
            current += Time.deltaTime;          
        }

        if(abilityEnabled)
        {
            abili.value -= Time.deltaTime;
        }

        if (current >= tutorialTime && !stop)
        {
            stop = true;
            tutorial.SetActive(false);
        }
        
        if (GameManager.lives == 3)
        {
            lives.SetActive(true);
            lives1.SetActive(true);
            lives2.SetActive(true);
        } else if (GameManager.lives == 2)
        {
            lives.SetActive(true);
            lives1.SetActive(true);
            lives2.SetActive(false);
        }
        else if (GameManager.lives == 1)
        {
            lives.SetActive(true);
            lives1.SetActive(false);
            lives2.SetActive(false);
        }
        else
        {
            lives.SetActive(false);
            lives1.SetActive(false);
            lives2.SetActive(false);
        }

	}
}
