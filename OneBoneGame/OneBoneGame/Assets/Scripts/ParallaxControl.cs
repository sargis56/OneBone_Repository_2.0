using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxControl : MonoBehaviour {

    private Transform mainCameraTrans;
    public float parallaxSpeed;

	// Use this for initialization
	void Start () {
        mainCameraTrans = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {

        //update background position
        this.transform.position = new Vector3(mainCameraTrans.position.x,
            transform.position.y, this.transform.position.z) * parallaxSpeed;
	}
}
