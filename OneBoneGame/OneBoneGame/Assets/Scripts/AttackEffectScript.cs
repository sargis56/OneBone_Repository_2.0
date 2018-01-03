using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectScript : MonoBehaviour {
    public float detonationTime = 0.4f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        detonationTime -= Time.deltaTime;
        if (detonationTime <= 0)
            Destroy(this.gameObject);
        
    }
}
