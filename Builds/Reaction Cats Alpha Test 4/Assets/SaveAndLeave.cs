using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveAndLeave : MonoBehaviour {
	//public InventoryData myCatInventoryData;
	public CatDataStorage myCatInventoryData;
	public CatEditSlot editCat;
	public Cat catToSave;

	public void SaveCatAndLeave() {
		if (catToSave != null) {
			//myCatInventoryDataLoad ();
			//myCatInventoryData.AddCat (catToSave);
			myCatInventoryData.Save();
			SceneManager.LoadScene (0);
			//myCatInventoryData.Load();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (editCat.catToEdit != null) {
			catToSave = editCat.catToEdit;
		}
	}
}
