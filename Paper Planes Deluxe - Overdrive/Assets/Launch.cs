using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Launch : MonoBehaviour {

	Rigidbody2D rb;
	bool isAirborne;
	bool track;
	bool isLaunching;
	bool powerUp;
	string maxHeightVal;

	public Text currentDistance;
	public Text currentHeight;
	public Text maxHeight;
	public Slider powerSlider;

	public float power;

	Mod testMod = new Mod();
	Mod rocketMod = new Mod();

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		isLaunching = false;
		rocketMod.enabled = true;
		rocketMod.vel = 5;
		/* testMod.debugMethod ();
		Debug.Log (testMod.modString); // This should be null
		testMod.modString = "new modString";
		Debug.Log (testMod.modString); // This should say 'new modString' */
	}

	public void Throw(float force)
	{
		float modForce;
		modForce = force;
		if (rocketMod.enabled)
			modForce = force + rocketMod.vel;
		rb.simulated = true;
		rb.AddRelativeForce(new Vector2(modForce, 0), ForceMode2D.Impulse);
		isAirborne = true;
		track = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Ragdoll
		if(isAirborne)
		{
			Vector2 v = rb.velocity;
			float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
	 		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	 	}

	 	//UI
		if(track)
		{
			currentDistance.text = "Distance: " + Math.Round(transform.position.x, 1).ToString();
			currentHeight.text = "Current Height: " + Math.Round(transform.position.y, 1).ToString();

			maxHeightVal = maxHeight.text.Remove(0, 12);
			if(transform.position.y > float.Parse(maxHeightVal))
				maxHeight.text = "Max Height: " + Math.Round(transform.position.y, 1	).ToString();
		}


	 	//Control
		if(isLaunching)
		{
			if(powerUp)
			{
				power += Time.deltaTime * powerSlider.maxValue;
				if(power >= powerSlider.maxValue)
					powerUp = false;
			}
			else
			{
				power -= Time.deltaTime * powerSlider.maxValue;
				if(power <= powerSlider.minValue)
					powerUp = true;
			}
			powerSlider.value = power;
			if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
			{
		 		Throw(power);
		 		isLaunching = false;
				power = 0;
				powerSlider.value = power;
		 	}
		}
		if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
			&& !isLaunching)
		{
			isLaunching = true;
			powerUp = true;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Ground")
		{
			isAirborne = false;
		}
	}
}
