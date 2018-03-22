using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModForSale : MonoBehaviour {

	public int id;
	public int cost;
	Store store;

	void Start()
	{
		store = GameObject.FindGameObjectWithTag("Store").GetComponent<Store>();
	}

	public void Purchase()
	{
		Debug.Log("clicked");
		store.PurchaseMod(id, cost);
		Destroy(this.gameObject);
	}
}
