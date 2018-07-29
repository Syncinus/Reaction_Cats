using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;


[Serializable]
public class CatSerializer {
	//public Sprite CatIcon;
	public string CatFileName;
	public int CatIconName;
	public List<String> PackagedAbilitys = new List<String> ();
	public List<String> PackagedAttacks = new List<String> ();
	public Rarity catRarity;
	//public List<Ability> abilitys = new List<Ability>();
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

	public CatSerializer(Cat saveCat) {
		//CatIcon = saveCat.CatIcon;
		CatFileName = saveCat.name;
		//CatIconName = saveCat.CatIcon.name;
		catRarity = saveCat.catRarity;
		//abilitys = saveCat.abilitys;
		foreach (Ability thingAbility in saveCat.abilitys) {
			PackagedAbilitys.Add (thingAbility.name.ToString());
		}
		foreach (Attack thingAttack in saveCat.attacks) {
			PackagedAttacks.Add (thingAttack.name.ToString ());
		}
		Defense = saveCat.Defense;
		Attack = saveCat.Attack;
		TorsoHP = saveCat.TorsoHP;
		MaxSpeed = saveCat.MaxSpeed;
		CatName = saveCat.CatName;
		TravelSpeed = saveCat.TravelSpeed;
		RichtextCatName = saveCat.RichtextCatName;
		MaxStamina = saveCat.MaxStamina;
		RumbleLevel = saveCat.RumbleLevel;
		AttackSpeed = saveCat.AttackSpeed;
		AttackRecharge = saveCat.AttackRecharge;
	}
}
