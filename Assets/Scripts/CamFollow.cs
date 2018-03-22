﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = target.transform.position;
		pos.z = -10;
		transform.position = pos;
	}
}
