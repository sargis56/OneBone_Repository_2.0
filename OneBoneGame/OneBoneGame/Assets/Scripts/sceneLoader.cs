using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour {

    private Rigidbody2D rb;
    public GameObject player1;
    public GameObject player2;
    public string nextLevel;
    public float currentLevelEnd;
    public bool finalLevel = false;
    GameObject aEnemy;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        aEnemy = GameObject.FindGameObjectWithTag("enemy");
        //SceneManager.LoadScene(level2);
    }
	
	// Update is called once per frame
	void Update () {
		if ((player1.GetComponent<Rigidbody2D>().position.x >= currentLevelEnd) || (player2.GetComponent<Rigidbody2D>().position.x >= currentLevelEnd))
        {
            if (finalLevel == true)
            {
                if (aEnemy == null)
                {
                    SceneManager.LoadScene(nextLevel);
                }
            }
            else
            {
                SceneManager.LoadScene(nextLevel);
            }
            //SceneManager.LoadScene(nextLevel);
        }
	}
}
