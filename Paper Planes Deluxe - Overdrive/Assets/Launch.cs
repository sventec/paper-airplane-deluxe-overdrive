using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Launch : MonoBehaviour {

	Rigidbody2D rb;
	bool isAirborne;
	bool isLaunching;
	bool isLanding;
	bool powerUp;
	bool track;
	string maxHeightVal;

	int dolla;

	public Text currentDistance;
	public Text currentHeight;
	public Text maxHeight;
	public Slider powerSlider;

	public float power;


	Mod testMod = new Mod();
	Mod rocketMod = new Mod();
	public Vector3 prevPos;


	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		dolla = 0;
		Init ();
		ModProperties ();
	}

	void Init()
	{
		prevPos = new Vector3();
		isLaunching = false;

		// Enable Mods START //
		rocketMod.enabled = false;

		// Enable Mods END //

		/* testMod.debugMethod ();
		Debug.Log (testMod.modString); // This should be null
		testMod.modString = "new modString";
		Debug.Log (testMod.modString); // This should say 'new modString' */

		isAirborne = false;
		isLanding = false;
		track = false;
		transform.position = new Vector3(0,20,0);
		transform.rotation = Quaternion.Euler(new Vector3(0,0,45));
		rb.velocity = new Vector2();
		rb.simulated = false;

	}

	// Init properties of all mods
	void ModProperties()
	{
		rocketMod.vel = 5;
		rocketMod.grav = 0.25f;
	}

	public void Throw(float force)
	{
		float modForce;
		modForce = force;
		// Force/Velocity mods below
		if (rocketMod.enabled)
			modForce = force + rocketMod.vel;
		// --- //
		rb.simulated = true;

		rb.AddRelativeForce(new Vector2(modForce, 0), ForceMode2D.Impulse);
		isLaunching = false;

		isAirborne = true;
		track = true;
		power = 0;
		powerSlider.value = power;

		// Gravity mods below
		if (rocketMod.enabled)
			rb.gravityScale += rocketMod.grav;
		// --- //
	}
	
	// Update is called once per frame
	void Update () {
		//Rotate to face velocity
		if(isAirborne)
		{
			Vector2 v = rb.velocity;
			float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
	 		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	 	}

	 	//UI updates
		if(track)
		{
			TextUpdate();
		}

		if(!isLanding)
		{
			//Launch and flight (?) controls
			ControlUpdate();
		}
		else
		{
			//determine stop
			if(prevPos != transform.position)
				prevPos = transform.position;
			else
			{
				isLanding = false;
				rb.simulated = false;
			}
		}
	}

	void TextUpdate()
	{
		currentDistance.text = "Distance: " + Math.Round(transform.position.x, 1).ToString();
		currentHeight.text = "Current Height: " + Math.Round(transform.position.y, 1).ToString();

		maxHeightVal = maxHeight.text.Remove(0, 12);
		if(transform.position.y > float.Parse(maxHeightVal))
			maxHeight.text = "Max Height: " + Math.Round(transform.position.y, 1	).ToString();
	}

	void ControlUpdate()
	{
		//Landing controls
		if(isLanding)
		{
			//no landing controls yet
		}
		//Air controls
		if(isAirborne)
		{
			//no airborne controls yet
		}

		//Launch confirm
		else if(isLaunching)
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
			//Launch on space/m1 up!
			if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
			{
				Throw(power);
		 	}
		}
		//Start launch on space/m1
		else if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
			&& !isLaunching && !isAirborne && !isLanding)
		{
			isLaunching = true;
			powerUp = true;
		}

		//Reset!
		if(Input.GetKeyDown(KeyCode.R))
		{
			Init();
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Ground")
		{
			isAirborne = false;
			isLanding = true;
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if(col.gameObject.tag == "Ground")
		{
			isAirborne = true;
			isLanding = false;
		}
	}
}
