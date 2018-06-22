using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "New Cat", menuName = "Characters/Cat")]
[System.Serializable] public class Cat : ScriptableObject {
	public Sprite CatIcon;
	public Rarity catRarity;
	public int CatIconName;
	public List<Ability> abilitys = new List<Ability>();
	public float Defense;
	public float Attack;
	public float TorsoHP;
	public float MaxSpeed;
	public string CatName;
	public float TravelSpeed;
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

	/*
	public void Init(Rarity theRarity, float CatDefense, float CatAttack, float CatTorsoHP, float CatMaxSpeed, string TheCatName, float CatTravelSpeed, int CatMaxStamina, int CatAttackSpeed, int CatAttackRecharge, List<Ability> CatAbilitys) {
		this.catRarity = theRarity;
		this.Defense = CatDefense;
		this.Attack = CatAttack;
		this.TorsoHP = CatTorsoHP;
		this.MaxSpeed = CatMaxSpeed;
		this.CatName = TheCatName;
		this.TravelSpeed = CatTravelSpeed;
		this.MaxStamina = CatMaxStamina;
		this.AttackSpeed = CatAttackSpeed;
		this.AttackRecharge = CatAttackRecharge;
		this.abilitys = CatAbilitys;
	}



	public static Cat CreateInstance(Rarity qRarity, float qDefense, float qAttack, float qTorsoHP, float qMaxSpeed, string qCatName, float qTravelSpeed, int qMaxStamina, int qAttackSpeed, int qAttackRecharge, List<Ability> qAbilitys) {
		var data = ScriptableObject.CreateInstance<Cat> ();
		data.Init (qRarity, qDefense, qAttack, qTorsoHP, qMaxSpeed, qCatName, qTravelSpeed, qMaxStamina, qAttackSpeed, qAttackRecharge, qAbilitys);
		return data;
	}
	*/


		
}

/*
public struct Cat {
	public Sprite CatIcon;
	public Rarity catRarity;
	public List<Ability> abilitys;
	public float Defense;
	public float Attack;
	public float TorsoHP;
	public float MaxSpeed;
	public string CatName;
	public float TravelSpeed;
	public string RichtextCatName;
	public int MaxStamina;
	public int AttackSpeed;
	public int AttackRecharge;

}
*/


public enum Rarity {Basic,Uncommon,Rare,Mega,Advanced,Epic,Legendary,Mythic,Insane,Supreme,Extreme,AWESOME};