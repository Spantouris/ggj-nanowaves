using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Network : MonoBehaviour {

    private static string SERVER_URL = "http://83.212.120.66:8888/";

    private static string sessionToken;

    public static Network nt;

    public int score;
    public string user;
    
    void Awake()
    {
        if (nt != null)
        {
            Destroy(this.gameObject);
        }
        nt = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void newSession()
    {
        StartCoroutine(setTokenFromServer());
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(setTokenFromServer());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void submitScore(int score,string user,Text tx)
    {
        this.score = score;
        this.user = user;
        StartCoroutine(submit(this.score,this.user,tx,sessionToken));        
    }
    
    private static IEnumerator submit(int score,string username,Text txt,string token)
    {
        List<IMultipartFormSection> payload = new List<IMultipartFormSection>();
                
        payload.Add(new MultipartFormDataSection("test=test"+"&token="+token+"&highscore="+score+"&username="+username));
        
        UnityWebRequest www = UnityWebRequest.Post(SERVER_URL + "score", payload);
        
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.Send();
        
        if (www.isError)
        {
            Debug.Log(www.error);
            txt.gameObject.SetActive(true);
            txt.text = "Error";
        }
        else
        {
            Debug.Log("Resdponse Code: " + www.responseCode);
            Debug.Log(www.downloadHandler.text);
            if (www.responseCode != 200)
            {
                txt.gameObject.SetActive(true);
                txt.text = "Error";
            }
            else
            {
                txt.gameObject.SetActive(true);
                txt.text = "Submitted";
            }
        }
    }

    private static IEnumerator setTokenFromServer() 
	{
		List<IMultipartFormSection> payload = new List<IMultipartFormSection> ();
		payload.Add( new MultipartFormDataSection("test=test"));

		UnityWebRequest www = UnityWebRequest.Post (SERVER_URL + "session", payload);
		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			www.GetResponseHeaders ().TryGetValue ("token", out sessionToken);
			Debug.Log (sessionToken);
		}
	}
}
