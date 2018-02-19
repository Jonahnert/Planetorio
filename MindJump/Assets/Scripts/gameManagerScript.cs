using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour {
    //TODO: have proximity based draining be the score instead of time
    public GameObject numberText;
    public GameObject goalText;
    public GameObject bestText;
    public GameObject nextSceneButton;
    private float time = 0;
    public float goalTime = 5;
    private float bestTime = 0;
    // Use this for initialization
    void Awake () {
        goalText.GetComponent<Text>().text = goalTime.ToString();
    }
	
	// Update is called once per frame
	void Update () {
       // time += Time.deltaTime;
        //goalText.GetComponent<Text>().text = time.ToString();

    }

    public void SetTimeAlive(float timeAlive)
    {
        numberText.GetComponent<Text>().text = timeAlive.ToString();


    }

    public void WinLevel()
    {
        Debug.Log("YOu Win");
        
        nextSceneButton.SetActive(true);
    }
    public void CheckTimeAlive(float timeAlive)
    {
        if (timeAlive > bestTime)
        {
            bestTime = timeAlive;
            bestText.GetComponent<Text>().text = bestTime.ToString();
        }
        if (timeAlive >= goalTime)
        {
            WinLevel();
        }
    }
}
