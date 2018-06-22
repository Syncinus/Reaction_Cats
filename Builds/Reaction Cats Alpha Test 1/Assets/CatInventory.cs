using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class CatInventory : MonoBehaviour {

	#region Singleton
	public static CatInventory instance;

	void Awake() {
		if (instance != null) {
			Debug.LogWarning ("More Than One Instance Of Cat Inventory Detected, Exiting!");
			return;
		}
		instance = this;
	}
	#endregion

	public List<Cat> cats = new List<Cat>(); //Remove The Equals New List Later Because Of SaveData!
}
