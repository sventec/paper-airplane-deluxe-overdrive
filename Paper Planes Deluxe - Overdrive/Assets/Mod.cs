using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod {

	// Mod Debug //

	public string modString;

	public void debugMethod () {
		Debug.Log ("this method was called");
	}

	// Mod Main //

	public float velMod;
	public float gravMod;
	public float stabMod; // Stabilization modifiers? Make acceptable launch force higher, etc?

}
