using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public Animation an;
    
	// Use this for initialization
	void Start ()
    {
        Time.timeScale = 1;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void High()
    {
        //Exe Open URL
        //Application.OpenURL("http://83.212.120.66/nanowaves.html");

        //WebGL Open New Tab
        Application.ExternalEval("window.open(\"http://83.212.120.66/nanowaves.html\")");
    }

    public void PresstToExit()
	{
		Application.Quit ();
	}

	public void LoadScene()
	{
		SceneManager.LoadScene (1);
	}

    public void Credits()
    {
        an.Play();
    }

    public void CreditsBack()
    {
        an.Play("CreditsBack");
    }
}
