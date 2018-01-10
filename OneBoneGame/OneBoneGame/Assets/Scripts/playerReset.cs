using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerReset : MonoBehaviour {

	public Transform spawnPoint;
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter2D(Collider2D other)
	{

		if (other.tag == "bobbie" || other.tag == "bonnie")
		{
			other.transform.position = spawnPoint.position;
			other.GetComponent<Rigidbody2D>().velocity = new Vector2 (0,0);
			other.GetComponent<playerController>().lives = other.GetComponent<playerController>().lives - 1;
			other.GetComponent<playerController>().hp = 60;
			other.GetComponent<playerController>().updateLife();
			other.GetComponent<playerController>().updateLives();

			//Instantiate(player, spawnPoint.transform.position, spawnPoint.transform.rotation);
			//Destroy(other.gameObject);
		}

	}


}

