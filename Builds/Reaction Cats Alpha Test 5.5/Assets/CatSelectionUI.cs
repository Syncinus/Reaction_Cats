using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CatSelectionUI : MonoBehaviour {

	public Transform catSlotParent;
	public static bool EnabledThings = false;
	public bool PreformedEnable = false;
	public GameObject selectionUI;
	public CatInventory selectableCats;
	public CatDataStorage myCatData;

	CatSelectionSlot[] catslots;

	// Use this for initialization
	void Start () {
		//myCatData.Load ();
		//InventoryData.Load();
		selectionUI.SetActive (false);
		catslots = catSlotParent.GetComponentsInChildren<CatSelectionSlot> ();
		myCatData.Load ();
		for (int i = 0; i < catslots.Length; i++) {
			Cat myCatThing = null;
			if (i < myCatData.CatData.Count) {
				myCatThing = myCatData.CatData.ElementAt (i);
				//myCatThing.CatIcon = Resources.Load<Sprite> ("/CatTiles" + myCatThing.CatIconName);
			}
			if (myCatThing != null) {
				catslots.ElementAt (i).currentCat = myCatThing;
				Debug.Log ("Cat Added.");
			}
			if (myCatThing == null) {
				catslots.ElementAt (i).RemoveCat ();
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (EnabledThings == true && PreformedEnable == false) {
			selectionUI.SetActive (true);
			PreformedEnable = true;
		}
	}
}
