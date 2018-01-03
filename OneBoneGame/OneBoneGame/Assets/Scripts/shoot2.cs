using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot2 : MonoBehaviour
{

	//public variables
	public GameObject bullet;
	public List<Transform> direction;
	public Transform center;
	public float bulletSpeed;
    private GameObject bobbie;
    private playerController script;

	//input direction
	Transform currnetDirection;


	// Use this for initialization
	void Start()
	{
        bobbie = GameObject.FindGameObjectWithTag("bobbie");
        script = bobbie.GetComponent<playerController>();
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S)) //down-left  down-right
		{
            if (script.grounded == false)
			    currnetDirection = direction[4];
            else
                currnetDirection = direction[2];
        }
        else if (Input.GetKey(KeyCode.S)) //down
		{
            if (script.grounded == false)
                currnetDirection = direction[1];
            else
                currnetDirection = direction[2];
        }
		else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)) //up-left up-right
			currnetDirection = direction[3];

		else if (Input.GetKey(KeyCode.W)) //up
			currnetDirection = direction[0];
		
		else //default to last horizontal direction if no buttons is pressed
			currnetDirection = direction[2];
		

		if (Input.GetKeyUp(KeyCode.Mouse0)) //shooting a bullet using left click
		{
			GameObject bullets = Instantiate(bullet, center.transform.position, Quaternion.identity) as GameObject;
			Vector2 forceVec = currnetDirection.transform.position - center.transform.position;
			forceVec *= bulletSpeed;
			bullets.GetComponent<Rigidbody2D>().AddForce(forceVec,ForceMode2D.Impulse);
		}
	}
}