using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModMenu : MonoBehaviour {

	private List<Mod> modList;
	public Dropdown[] modSlots;
	private List<Mod> activeMods;

	// Use this for initialization
	void Start () {
		modList = new List<Mod>();
		activeMods = new List<Mod>();

		//Debug
		Mod rocket = new Mod();
		rocket.modString = "Rocket Engine";
		rocket.vel = 5;
		rocket.grav = 0.25f;
		modList.Add(rocket);

		Mod balloon = new Mod();
		balloon.modString = "Balloon";
		balloon.grav = -.5f;
		modList.Add(balloon);

		Mod lead = new Mod();
		lead.modString = "Lead";
		lead.grav = 1;
		modList.Add(lead);

		Refresh();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Refresh()
	{
		foreach(Dropdown d in modSlots)
		{
			d.ClearOptions();
			Dropdown.OptionData entry = new Dropdown.OptionData();
			entry.text = "No mod";
			d.options.Add(entry);
			foreach(Mod mod in modList)
			{
				entry = new Dropdown.OptionData();
				entry.text = mod.modString;
				d.options.Add(entry);
			}
			d.RefreshShownValue();
		}
	}
}
