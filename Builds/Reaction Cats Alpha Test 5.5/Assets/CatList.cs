using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Catlist", menuName = "Characters/Catlist")]
public class CatList : ScriptableObject {
	public Cat[] loadedCats;
}
