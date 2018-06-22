using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Cat", menuName = "Characters/Cat")]
public class Cat : ScriptableObject {
	public Sprite CatIcon;
	public Rarity catRarity;
	public List<Ability> abilitys = new List<Ability>();
	public float Defense;
	public float Attack;
	public float TorsoHP;
	public float MaxSpeed;
	public string CatName;
	public string RichtextCatName = "null";
	public int MaxStamina;
	public float? RumbleLevel = null;
	public int AttackSpeed;
	public int AttackRecharge;

	public virtual void Select() {
		Debug.Log ("Selected Cat: " + CatName);
		if (!CatSelectionManager.instance.selectedCats.Contains (this)) {
			CatSelectionManager.instance.Select (this);
		}
	}


		
}

public enum Rarity {Basic,Uncommon,Rare,Mega,Advanced,Epic,Legendary,Mythic,Insane,Supreme,Extreme,AWESOME};