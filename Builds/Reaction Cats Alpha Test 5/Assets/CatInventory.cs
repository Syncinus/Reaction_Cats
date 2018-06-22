using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class CatInventory  {
	public List<CatSerializer> cats = new List<CatSerializer>(); //Remove The Equals New List Later Because Of SaveData!
	//public int derpyness;
	//public int powerlevel;
}
