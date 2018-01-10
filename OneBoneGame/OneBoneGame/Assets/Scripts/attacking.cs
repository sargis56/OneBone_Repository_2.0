using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacking : MonoBehaviour {

	public GameObject attackEffect;
	public GameObject enemyExplosion;
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "enemy")
		{
			Instantiate(attackEffect, collision.transform.position, Quaternion.identity);
            if (collision.gameObject.GetComponent<enemy>().hp <= 0)
            {
                Instantiate(enemyExplosion, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            Debug.Log("enemy : " + collision.gameObject.GetComponent<enemy>().hp);
		}
	}
}
