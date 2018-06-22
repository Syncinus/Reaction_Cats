using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

//
//SYSTEM OF CATS, WIP, WILL SERVE AS PLACE OF BASES OF CATS.
//
public class Cats : MonoBehaviour {
	public List<Ability> catAbilitys = new List<Ability>();
	public static List<BaseCat> cats = new List<BaseCat> ();

	public void Start() {
		Ability[] abilitys = (Ability[])Resources.LoadAll<Ability>("Abilitys");
		catAbilitys = abilitys.ToList ();
	}

	public void MakeCats() {
		
	}
		

}

public struct BaseCat {
	Sprite icon;
	Rarity rarity;
	int iconName;
	List<Ability> abilitys;
	float defense;
	float attack;
	float torsoHP;
	float maxSpeed;
	string catName;
	float travelSpeed;
	string richTextCatName;
	int maxStamina;
	int attackSpeed;
	int attackRecharge;
}
