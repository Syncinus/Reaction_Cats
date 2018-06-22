using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CatSelectionManager : NetworkBehaviour {
	#region Singleton
	public static CatSelectionManager instance;

	void Awake() {
		if (instance != null) {
			Debug.LogWarning ("More Than One Instance Of Cat Selection Manager Detected, Exiting!");
			return;
		}
		instance = this;
	}
	#endregion


	public List<Cat> selectedCats = new List<Cat>();


	public void Select(Cat selectedCat) {
		selectedCats.Add (selectedCat);
		Debug.Log (selectedCat.CatName + " Has Been Added To The Selected List. ");
	}

}
