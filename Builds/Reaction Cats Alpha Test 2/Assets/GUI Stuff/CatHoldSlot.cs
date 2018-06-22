using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatHoldSlot : MonoBehaviour {
	public Image icon;
	public Cat currentCat;

	public void RemoveCat() {
		icon.sprite = null;
		icon.enabled = false;
	}

	public void Update() {
		if (currentCat != null) {
			icon.sprite = currentCat.CatIcon;
			icon.enabled = true;
		}
	}


}
