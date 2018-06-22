using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipSystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public Transform Tooltip;
	public Transform clonedTooltip;
	public bool PointerOver = false;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		offset = new Vector3 (54, -90);
		if (PointerOver == true) {
			if (clonedTooltip != null) {
				clonedTooltip.position = Input.mousePosition + offset;
			}
		}
		if (clonedTooltip != null && this.gameObject.GetComponent<CatSelectionSlot>().currentCat == null) {
			PointerOver = false;
			GameObject toolTipToDestroy = clonedTooltip.gameObject;
			clonedTooltip = null;
			Destroy (toolTipToDestroy);
	    }
	}

	public void OnPointerEnter(PointerEventData eventData) {
		if (this.gameObject.GetComponent<CatSelectionSlot> ().currentCat != null) {
			//Debug.Log ("HERE COME DAT BOI");
			//Tooltip.position = Input.mousePosition;
			GameObject tooltipClone = (GameObject)Instantiate(Tooltip.gameObject, this.transform.parent) as GameObject;
			Cat ourCat = this.gameObject.GetComponent<CatSelectionSlot> ().currentCat;
			clonedTooltip = tooltipClone.transform;
			clonedTooltip.SetParent (this.transform.parent);
			clonedTooltip.gameObject.SetActive (true);
			PointerOver = true;
			clonedTooltip.Find ("CatName").GetComponent<Text> ().text = this.gameObject.GetComponent<CatSelectionSlot> ().currentCat.CatName;
			Text nameText = clonedTooltip.Find ("CatName").GetComponent<Text> ();
			if (ourCat.RichtextCatName != "null") {
				nameText.supportRichText = true;
				nameText.text = ourCat.RichtextCatName;
			}
			Text rarityText = clonedTooltip.Find ("CatRarity").GetComponent<Text> ();
			Text attackText = clonedTooltip.Find ("CatAttack").GetComponent<Text> ();
			Text defenseText = clonedTooltip.Find ("CatDefense").GetComponent<Text> ();
			Text speedText = clonedTooltip.Find ("CatSpeed").GetComponent<Text> ();
			Text HpText = clonedTooltip.Find ("CatHP").GetComponent<Text> ();
			attackText.text = "Cat Attack: " + ourCat.Attack.ToString ();
			defenseText.text = "Cat Defense: " + ourCat.Defense.ToString ();
			speedText.text = "Cat Speed: " + ourCat.MaxSpeed.ToString ();
			HpText.text = "Cat Torso Hp: " + ourCat.TorsoHP.ToString ();
			rarityText.text = this.gameObject.GetComponent<CatSelectionSlot> ().currentCat.catRarity.ToString ();

			if (ourCat.catRarity == Rarity.Basic) {
				rarityText.color = new Color (1, 1, 1, 1);
			}

			if (ourCat.catRarity == Rarity.Uncommon) {
				rarityText.color = new Color (0, 0, 1, 1);
			}

			if (ourCat.catRarity == Rarity.Rare) {
				rarityText.color = new Color32 (232, 0, 254, 255);
			}

			if (ourCat.catRarity == Rarity.Mega) {
				rarityText.color = new Color32 (179, 223, 32, 255);
			}

			if (ourCat.catRarity == Rarity.Advanced) {
				rarityText.color = new Color (1, 0, 0, 1);
			}

			if (ourCat.catRarity == Rarity.Epic) {
				rarityText.color = new Color32 (254, 161, 0, 255);
			}

			if (ourCat.catRarity == Rarity.Legendary) {
				rarityText.color = new Color32 (0, 201, 254, 255);
			}

			if (ourCat.catRarity == Rarity.Mythic) {
				rarityText.color = new Color (0, 1, 1, 1);
			}

			if (ourCat.catRarity == Rarity.Insane) {
				rarityText.color = new Color32 (60, 0, 254, 255);
			}

			if (ourCat.catRarity == Rarity.Supreme) {
				rarityText.color = new Color (1, 0.92f, 0.016f, 1);
			}

			if (ourCat.catRarity == Rarity.Extreme) {
				//rarityText.color = new Color32 (254, 161, 0, );
				rarityText.color = new Color32 (153, 62, 210, 255);
			}

			if (ourCat.catRarity == Rarity.AWESOME) {
				rarityText.supportRichText = true;
				rarityText.text = "<b><color=red>A</color><color=orange>W</color><color=yellow>E</color><color=green>S</color><color=blue>O</color><color=#9400D3>M</color><color=#4B0082>E</color></b>";
			}

		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		if (clonedTooltip != null) {
			PointerOver = false;
			GameObject toolTipToDestroy = clonedTooltip.gameObject;
			clonedTooltip = null;
			Destroy (toolTipToDestroy);
		}
	}
}
