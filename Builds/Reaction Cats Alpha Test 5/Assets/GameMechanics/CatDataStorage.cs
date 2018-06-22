using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class CatDataStorage : MonoBehaviour {
	
	public List<Cat> CatData = new List<Cat> ();
	public int Derpyness = 1;
	public int PowerLevel = 1;

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/Cats.dat");

		CatInventory data = new CatInventory ();
		foreach (Cat cToSer in CatData) {
			data.cats.Add (new CatSerializer (cToSer));
		}
		//data.cats = SerializedData;

		bf.Serialize (file, data);
		file.Close ();
		Debug.Log ("Data Saved!");
	}

	public void Load() { 
		if (File.Exists (Application.persistentDataPath + "/Cats.dat")) {
			Debug.Log ("DERPMASTER IS HERE!");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/Cats.dat", FileMode.Open);
			CatInventory data = (CatInventory)bf.Deserialize (file);
			file.Close ();
			List<CatSerializer> cSer = data.cats;
			List<Cat> cCats = new List<Cat> ();
			foreach (CatSerializer ser in cSer) {
				Cat myCat = (Cat) ScriptableObject.CreateInstance ("Cat") as Cat;
				foreach (String myCatAbility in ser.PackagedAbilitys) {
					Ability myAbility = Resources.Load<Ability> ("Abilitys/" + myCatAbility);
					myCat.abilitys.Add (myAbility);
				}
				foreach (String myCatAttack in ser.PackagedAttacks) {
					Attack myAttack = Resources.Load<Attack> ("Attacks/" + myCatAttack);
					myCat.attacks.Add (myAttack);
				}
				myCat.name = ser.CatFileName;
				myCat.catRarity = ser.catRarity;
				myCat.CatIconName = ser.CatIconName;
				myCat.Defense = ser.Defense;
				myCat.Attack = ser.Attack;
				myCat.TorsoHP = ser.TorsoHP;
				myCat.MaxSpeed = ser.MaxSpeed;
				myCat.CatName = ser.CatName;
				myCat.TravelSpeed = ser.TravelSpeed;
				myCat.RichtextCatName = ser.RichtextCatName;
				myCat.MaxStamina = ser.MaxStamina;
				myCat.RumbleLevel = ser.RumbleLevel;
				myCat.AttackSpeed = ser.AttackSpeed;
				myCat.AttackRecharge = ser.AttackRecharge;
				cCats.Add (myCat);
			}
			CatData = cCats;
			Debug.Log ("Data Loaded!");
		}
	}

	//public List<Cat> load() {

	//}
}

