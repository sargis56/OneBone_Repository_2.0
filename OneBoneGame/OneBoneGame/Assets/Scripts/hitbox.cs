using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour {
	public BoxCollider2D hitBox;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		if (this.GetComponent<playerController>().attacking)
		{

			hitBox.enabled = true;
			if (gameObject.transform.rotation.y == 0) // LEFT
				hitBox.offset = new Vector2(-0.15f, hitBox.offset.y);
			else if (gameObject.transform.rotation.y == 180) // RIGHT
				hitBox.offset = new Vector2(0.15f, hitBox.offset.y);
		}
		else
			hitBox.enabled = false;
	}
}
