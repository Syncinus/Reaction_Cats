using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSelectionUI : MonoBehaviour {

	public Transform catSlotParent;
	public static bool EnabledThings = false;
	public bool PreformedEnable = false;
	public GameObject selectionUI;
	public CatInventory selectableCats;

	CatSelectionSlot[] catslots;

	// Use this for initialization
	void Start () {
		selectionUI.SetActive (false);
		catslots = catSlotParent.GetComponentsInChildren<CatSelectionSlot> ();
		for (int i = 0; i < catslots.Length; i++) {
			if (i < selectableCats.cats.Count) {
				catslots [i].AddCat (selectableCats.cats [i]);
			} else {
				catslots [i].RemoveCat ();
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
