using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {
	//public variables
	public GameObject bullet;
	public List<Transform> direction;
	public float bulletSpeed;

	//input direction
	Transform currnetDirection;
	Transform lastDirection;

	// Use this for initialization
	void Start () {
		currnetDirection = direction[3];
		lastDirection = direction[3];
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))  //up-right
		{
			currnetDirection = direction[4];		
			lastDirection = direction[3];
		}
		else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S)) //down-right
		{
			currnetDirection = direction[5];
			lastDirection = direction[3];
		}
		else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) //down-left
		{
			currnetDirection = direction[6];
			lastDirection = direction[2];
		}
		else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) //up-left
		{
			currnetDirection = direction[7];
			lastDirection = direction[2];
		}
		else if (Input.GetKey(KeyCode.S)) //down
		{
			currnetDirection = direction[1];

		}
		else if (Input.GetKey(KeyCode.A)) //right
		{
			currnetDirection = direction[2];
			lastDirection = direction[2];
		}
		else if (Input.GetKey(KeyCode.D)) //left
		{
			currnetDirection = direction[3];
			lastDirection = direction[3];
		}
		else if (Input.GetKey(KeyCode.W)) //up
		{
			currnetDirection = direction[0];
		}
		else //default to last horizontal direction if no buttons is pressed
		{
			currnetDirection = lastDirection;
		}
		


		if (Input.GetKeyUp(KeyCode.Mouse0)) //shooting a bullet using left click
		{
			GameObject bullets = Instantiate(bullet,this.transform.position, Quaternion.identity) as GameObject;
			Vector2 forceVec = currnetDirection.transform.position - this.transform.position;
			forceVec *= bulletSpeed;
			bullets.GetComponent<Rigidbody2D>().AddForce(forceVec/*,ForceMode2D.Impulse*/);
		}
	}
}
