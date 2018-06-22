using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatLoader : MonoBehaviour {
	#region Singleton

	public static CatLoader instance;

	void Awake() {
		if (instance != null) {
			Debug.LogWarning ("More Than One Cat Loader Instance Detected, Exiting!");
			return;
		}
		instance = this;
	}
	#endregion

	public List<Cat> theCatList = new List<Cat>();
}
