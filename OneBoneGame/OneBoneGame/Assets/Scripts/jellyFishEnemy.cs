using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jellyFishEnemy : enemy {

	private Rigidbody2D rb;
	GameObject gameControllerObject;
	public int speed = 5;

	public Transform point1;
	public Transform point2;

	bool playerNear;
	bool hit;


	float time;

	Vector2 forceVec;

	void Start()
	{
		time = 0;
		playerNear = false;         //initializing variables
		hit = false;
		rb = GetComponent<Rigidbody2D>();
	}

	// attacks when player is in the collider
	private void OnTriggerStay2D(Collider2D other)
	{

		forceVec = (other.transform.position) -this.transform.position ;

		forceVec *= 1f;


		if (other.tag == "bonnie" || other.tag == "bobbie")
		{
			playerNear = true;
			this.GetComponent<Rigidbody2D>().velocity=forceVec; //enemy attacks/jump to player

		}

		
	}

	//enemy bounce back after attack
	private void OnCollisionEnter2D(Collision2D collision)
	{


		if (collision.gameObject.tag == "bonnie" || collision.gameObject.tag == "bobbie")
		{
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(-this.GetComponent<Rigidbody2D>().velocity.x * 5f, 7f	);

			if (collision.gameObject.GetComponent<playerController>().attacking == false)
			{
				collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x * 2), 7f);
				
			}
			hp -= 1;

		}



	}

	//checking if player leaves the attack area
	private void OnTriggerExit2D(Collider2D collision)
	{

		playerNear = false;

	}

	void Update()
	{

		if (playerNear == false)    //enemy goes back to patrol if player leaves the area
		{
			patrolling();
		}


	}

	public void patrolling()
	{
		if (this.gameObject.tag == "enemy")
		{

			if (rb.position.x <= point1.position.x && speed < 0)    //moving towards point1
			{
				//rb.velocity = new Vector2(speed, 0);
				speed *= -1;
			}
			if (rb.position.x >= point2.position.x && speed > 0)        //moving towards point2
			{
				speed *= -1;
				//rb.velocity = new Vector2(speed * -1, 0);
			}
			rb.velocity = new Vector2(speed, 0);    //enemy moving
		}
		else
		{
			Debug.Log("Cannot find Game Object");
		}

	}

}
