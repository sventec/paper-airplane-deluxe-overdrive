using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
	private CanvasGroup storeGroup;
	private bool isVisible;
	public GameObject[] mods;
	private Mod[] modData;

	public ModMenu modInv;

	// Use this for initialization
	void Start ()
	{
		storeGroup = gameObject.GetComponent<CanvasGroup>();
		isVisible = false;
		Populate();
	}

	void Populate()
	{
		modData = new Mod[3];

		Mod rocket = new Mod();
		rocket.modString = "Rocket Engine";
		rocket.vel = 5;
		rocket.grav = 0.25f;
		modData[0] = rocket;

		Mod balloon = new Mod();
		balloon.modString = "Balloon";
		balloon.grav = -.5f;
		modData[1] = balloon;

		Mod lead = new Mod();
		lead.modString = "Lead";
		lead.grav = 1;
		modData[2] = lead;

		//rocket
		GameObject tmp = Instantiate(mods[0], this.transform);
		tmp.GetComponent<RectTransform>().anchoredPosition = new Vector2(36,-36);

		//balloon
		tmp = Instantiate(mods[1], this.transform);
		tmp.GetComponent<RectTransform>().anchoredPosition = new Vector2(90,-36);

		//lead
		tmp = Instantiate(mods[2], this.transform);
		tmp.GetComponent<RectTransform>().anchoredPosition = new Vector2(144,-36);
	}

	public void Toggle()
	{
		isVisible = !isVisible;
		if(isVisible)
		{
			storeGroup.alpha = 1;
			storeGroup.interactable = true;
			storeGroup.blocksRaycasts = true;
		}
		else
		{
			storeGroup.alpha = 0;
			storeGroup.interactable = false;
			storeGroup.blocksRaycasts = false;
		}
	}

	public void PurchaseMod(int index, int cost)
	{
		modInv.AddMod(modData[index]);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
