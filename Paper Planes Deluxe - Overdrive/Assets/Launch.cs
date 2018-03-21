using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Launch : MonoBehaviour {

	Rigidbody2D rb;
	bool isAirborne;
	bool track;
	bool isLaunching;
	bool powerUp;

	public Text currentDistance;
	public Text currentHeight;
	public Text maxHeight;
	public Slider powerSlider;

	public float power;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		isLaunching = false;
	}

	public void Throw(float force)
	{
		rb.simulated = true;
		rb.AddRelativeForce(new Vector2(force, 0), ForceMode2D.Impulse);
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
	 		currentDistance.text = "Distance: " + transform.position.x.ToString();
	 		currentHeight.text = "Current Height: " + transform.position.y.ToString();
	 		if(transform.position.y > float.Parse(maxHeight.text))
	 			maxHeight.text = transform.position.y.ToString();
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
