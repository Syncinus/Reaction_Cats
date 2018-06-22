using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;
//This Is The Game Save System, It Uses This To Save Your Cats.
//This One Is Abandoned
public class InventoryData : MonoBehaviour {
	
	public List<Cat> catData = new List<Cat>();

	//public void StartGame() {
	//	load ();
	//}

	public void AddCat(Cat newCat) {
		catData.Add (newCat);
		Save ();
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream gameFile = File.Create (Application.persistentDataPath + "/Cats.dat");

		CatInventory ownedCats = new CatInventory ();

		//ownedCats.cats = this.catData;

		bf.Serialize (gameFile, catData);
		gameFile.Close ();
	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/Cats.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream loadFile = File.Open (Application.persistentDataPath + "/Cats.dat", FileMode.Open);

			CatInventory CatsLoaded = (CatInventory)bf.Deserialize (loadFile);
			loadFile.Close ();

		//	this.catData = CatsLoaded.cats;
		}
	}

	public List<Cat> load() 
	{
		if (File.Exists (Application.persistentDataPath + "/Cats.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream catsFile = File.Open(Application.persistentDataPath + "/Cats.dat", FileMode.Open);

			CatInventory CatsLoaded = (CatInventory)bf.Deserialize(catsFile);
			catsFile.Close();

			//return CatsLoaded.cats;
		}
		return new List<Cat>();
	}

}
