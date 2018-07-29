using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.UI;

public class CatEditSlot : MonoBehaviour {
	public Image icon;
	public Cat catToEdit;
	public Cat catToSwap;
	public CatDataStorage storage;
	public Text attackText;
	public Text defenseText;
	public Text torsoHPText;

	public Text attackValue;
	public Text defenseValue;
	public Text torsoHPValue;

	// Use this for initialization

	public void Start() {
		storage.Load ();
	}

	public void RemoveCat() {
		//catToEdit = null;
		icon.sprite = null;
		icon.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (catToEdit != null) {
			icon.sprite = catToEdit.CatIcon;
			icon.enabled = true;
			if (attackText.text != "" && attackText.text != null) {
				float SetupAttack = float.Parse (attackText.text, CultureInfo.InvariantCulture.NumberFormat);
				catToEdit.Attack = SetupAttack;
			}
			if (defenseText.text != "" && defenseText.text != null) {
				float SetupAttack = float.Parse (defenseText.text, CultureInfo.InvariantCulture.NumberFormat);
				catToEdit.Defense = SetupAttack;
			}
			if (torsoHPText.text != "" && torsoHPText.text != null) {
				float SetupAttack = float.Parse (torsoHPText.text, CultureInfo.InvariantCulture.NumberFormat);
				catToEdit.TorsoHP = SetupAttack;
			}
			attackValue.text = "Attack: " + catToEdit.Attack.ToString ();
			defenseValue.text = "Defense: " + catToEdit.Defense.ToString ();
			torsoHPValue.text = "Torso HP: " + catToEdit.TorsoHP.ToString ();
		}
	}
}
