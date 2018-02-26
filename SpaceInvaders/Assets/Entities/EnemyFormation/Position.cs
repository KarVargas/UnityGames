using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//	Draws a wire cube as a gizmo for the enemy ship's position in our gamespace
	void OnDrawGizmos() {
		Gizmos.DrawWireSphere (transform.position, 1);
	}

}
