using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour {

    public int health;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag == "bonnie") || (col.gameObject.tag == "bobbie"))
        {
            Destroy(this.gameObject);
        }          
    }
}
