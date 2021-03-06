﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {


	private static List<Attractor> attractors;

	private Rigidbody2D myRigidbody;
	
	//////////////////// Unity Event Handlers ////////////////////
	
	private void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	private void FixedUpdate () {
		foreach (var attractor in attractors) {
			if (attractor != this) attract(attractor);
		}
	}

	private void OnEnable() {
		if (attractors == null) attractors = new List<Attractor>();
		attractors.Add(this);
	}

	private void OnDisable() {
		attractors.Remove(this);
	}
	
	//////////////////// Helper Methods //////////////////////////

	private void attract(Attractor obj) {
		var objectRigidBody = obj.GetComponent<Rigidbody2D>();
		var direction = myRigidbody.position - objectRigidBody.position;
		var distance = direction.magnitude;
		var forceMagnitude = (myRigidbody.mass * objectRigidBody.mass) / Mathf.Pow(distance, 2);
		var force = direction.normalized * forceMagnitude;
		
		objectRigidBody.AddForce(force);
	}
}
