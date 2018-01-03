using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour {
	public GameObject bullet;
	public float bulletSpeed;
	
	float time;
	// Use this for initialization
	void Start () {
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		
		if (collision.tag == "bonnie" || collision.tag == "bobbie")
		{
			if (time <= Time.time)
			{

				GameObject bullets = Instantiate(bullet, this.transform.position, Quaternion.identity) as GameObject;
				Vector2 forceVec = collision.transform.position - this.transform.position;
				forceVec *= bulletSpeed;
				bullets.GetComponent<Rigidbody2D>().AddForce(forceVec, ForceMode2D.Impulse);
				time = Time.time + 0.5f;     

			}
		}

	}
}
