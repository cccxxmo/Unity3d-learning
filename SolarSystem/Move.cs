using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public float speed;
    float rx, ry;

    // Use this for initialization
    void Start () {
    	
        rx = Random.Range(-90, 90);
        ry = Random.Range(-90, 90);
    }	

	// Update is called once per frame
	void Update () {
        Vector3 raxis = new Vector3(rx, ry, 0);
        this.transform.RotateAround(this.transform.parent.position, raxis, speed * Time.deltaTime);
    }

}

