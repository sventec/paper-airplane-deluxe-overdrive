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
		}
	}
}
