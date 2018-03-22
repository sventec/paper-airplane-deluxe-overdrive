using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour {

	private CanvasGroup storeGroup;
	private bool isVisible;

	// Use this for initialization
	void Start () {
		storeGroup = gameObject.GetComponent<CanvasGroup>();
		isVisible = false;
	}

	public void Toggle()
	{
		isVisible = !isVisible;
		if(isVisible)
		{
			storeGroup.alpha = 1;
			storeGroup.interactable = true;
		}
		else
		{
			storeGroup.alpha = 0;
			storeGroup.interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
