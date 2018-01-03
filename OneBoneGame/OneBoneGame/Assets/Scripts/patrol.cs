using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : enemy {

	private Rigidbody2D rb;
	public Transform point1;
	public Transform point2;
	GameObject gameControllerObject;
	public int speed = 5;

	bool playerNear;
	bool hit;

	public Transform groundCheck;
	public LayerMask whatIsGround; //layers of that player can jump on (this case everything)
	bool grounded = false;
	float groundRadius = 0.1f; //radius of the groundcheck


	float time; 

	Vector2 forceVec;

	void Start()
    {
		time = 0;
		playerNear = false;			//initializing variables
		hit = false;
        rb = GetComponent<Rigidbody2D>();
    }

	// attacks when player is in the collider
	private void OnTriggerStay2D(Collider2D other)
	{

		forceVec = this.transform.position - ((other.transform.position + new Vector3 (0,1)));
		
		forceVec *= -5f;


		if (other.tag == "bonnie" || other.tag == "bobbie")
		{
			playerNear = true;
			if (grounded && time < Time.time)
			{
			
				this.GetComponent<Rigidbody2D>().AddForce(forceVec, ForceMode2D.Impulse); //enemy attacks/jump to player
				time = Time.time + 2;		// attacks every 2 seconds
			}

		}


	}

	//enemy bounce back after attack
	private void OnCollisionEnter2D(Collision2D collision)
	{


		if (collision.gameObject.tag == "bonnie" || collision.gameObject.tag == "bobbie" || collision.gameObject.tag == "bullet")
		{
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(-this.GetComponent<Rigidbody2D>().velocity.x*1.5f, 7f);

			if (collision.gameObject.tag == "sword")
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
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); //check if groundcheck overlap with the ground

		if (playerNear == false && grounded)	//enemy goes back to patrol if player leaves the area
		{
			patrolling();
		}
		

	}

	public void patrolling()
	{
		if (this.gameObject.tag == "enemy")
		{
			
			if (rb.position.x <= point1.position.x && speed < 0)	//moving towards point1
			{
				//rb.velocity = new Vector2(speed, 0);
				speed *= -1;
			}
			if (rb.position.x >= point2.position.x && speed > 0)		//moving towards point2
			{
				speed *= -1;
				//rb.velocity = new Vector2(speed * -1, 0);
			}
			rb.velocity = new Vector2(speed, 0);	//enemy moving
		}
		else
		{
			Debug.Log("Cannot find Game Object");
		}

	}

}
