using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Linq;

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
	public Button resetBtn;

	private float power;

	public AudioClip launchFx;
	public AudioClip landFX;
	public AudioClip bgFX;
	private AudioSource launchSound;
	private AudioSource landSound;
	private AudioSource bgSound;

	Mod testMod = new Mod();
	Mod rocketMod = new Mod();
	Mod balloonMod = new Mod();
	List<Mod> modArray = new List<Mod>();
	private Vector3 prevPos;


	// Use this for initialization
	void Start () {
		//Audio
		launchSound = gameObject.AddComponent<AudioSource>();
		landSound = gameObject.AddComponent<AudioSource>();
		bgSound = gameObject.AddComponent<AudioSource>();

		launchSound.loop = false;
		landSound.loop = false;
		bgSound.loop = true;

		launchSound.playOnAwake = false;
		landSound.playOnAwake = false;
		bgSound.playOnAwake = false;

		launchSound.volume = 1;
		landSound.volume = 1;
		bgSound.volume = 1;

		launchSound.clip = launchFx;
		landSound.clip = landFX;
		bgSound.clip = bgFX;
		//End Audio

		rb = gameObject.GetComponent<Rigidbody2D>();
		dolla = 0;
		Init ();
		ModProperties ();
	}

	public void Init()
	{
		prevPos = new Vector3();
		isLaunching = false;

		// Enable Mods START //
		//modArray.Add (rocketMod);
		//modArray.Add (balloonMod);

		//rocketMod.enabled = false;
		//balloonMod.enabled = true;	
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
		resetBtn.gameObject.SetActive(false);
		bgSound.Stop();
	}

	// Init properties of all mods
	void ModProperties()
	{
		rocketMod.vel = 5;
		rocketMod.grav = 0.25f;

		balloonMod.vel = -0.85f;
		balloonMod.grav = -0.45f;
	}

	public void Throw(float force)
	{
		float modForce;
		modForce = force;
		// Force/Velocity mods below
		foreach (Mod mod in modArray) {
			if (mod.enabled == true)
				modForce += mod.vel;
		}
		//if (rocketMod.enabled)
		//	modForce = force + rocketMod.vel;
		// --- //
		rb.simulated = true;

		rb.AddRelativeForce(new Vector2(modForce, 0), ForceMode2D.Impulse);
		isLaunching = false;

		isAirborne = true;
		track = true;
		power = 0;
		powerSlider.value = power;

		// Gravity mods below
		foreach (Mod mod in modArray) {
			if (mod.enabled == true)
				rb.gravityScale += mod.grav;
		}
		//if (rocketMod.enabled)
		//	rb.gravityScale += rocketMod.grav;
		// --- //

		launchSound.Play();
		bgSound.Play();
	}
	
	// Update is called once per frame
	void Update () {
		//Rotate to face velocity
		if(isAirborne)
		{
			Vector2 v = rb.velocity;
			if(v.y < (8 / -v.x) - 2)
			{
				v.y = (8 / -v.x) - 2;
				rb.velocity = v;
			}
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
				resetBtn.gameObject.SetActive(true);
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
			landSound.Play();
			bgSound.Stop();
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

	public void Relaunch()
	{
		dolla += (int)Math.Round(transform.position.x, 1);
		Init();
	}
}
