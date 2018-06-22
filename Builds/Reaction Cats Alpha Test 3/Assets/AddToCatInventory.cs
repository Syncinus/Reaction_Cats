using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class AddToCatInventory : MonoBehaviour {

	public CatInventory myCatInventory;
	public CatEditSlot thatCatEditSlot;
	public CatDataStorage myDataStorage;
	public Cat catBeingEdited;

	public void SaveCatToCatInventory() {
		if (catBeingEdited != null) {
			Debug.Log ("Cat Was Added To Cat Inventory!");
			myDataStorage.CatData.Add (catBeingEdited);
			myDataStorage.Save ();
		}
	}

	public void Update() {
		if (thatCatEditSlot.catToEdit != null) {
			catBeingEdited = thatCatEditSlot.catToEdit;
		}
	}
}
