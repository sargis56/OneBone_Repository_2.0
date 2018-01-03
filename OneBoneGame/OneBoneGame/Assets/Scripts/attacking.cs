using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacking : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "enemy")
		{
			if (collision.gameObject.GetComponent<enemy>().hp <= 0)
			{
				Destroy(collision.gameObject);

			}
			Debug.Log("enemy : " +collision.gameObject.GetComponent<enemy>().hp);
		}
	

	}
	
}
