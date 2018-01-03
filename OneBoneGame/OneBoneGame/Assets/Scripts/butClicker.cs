using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class butClicker : MonoBehaviour {

    public Button buttStroy;
    public Button buttQuit;

    // Use this for initialization
    void Start () {
        buttStroy.GetComponent<Button>().onClick.AddListener(ClickedStroy);
        buttQuit.GetComponent<Button>().onClick.AddListener(ClickedQuit);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickedStroy()
    {
        SceneManager.LoadScene("Scenes/Level_01");
    }
    public void ClickedQuit()
    {
        Application.Quit();
    }
}
