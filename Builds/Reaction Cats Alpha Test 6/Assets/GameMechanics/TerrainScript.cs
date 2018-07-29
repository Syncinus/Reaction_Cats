using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour {
	public TerrainType typeOfTerrain;
	public bool IsPassable = false;
	public int currentHexRow = 0;
	public int currentHexNum = 0;
	public Transform component;
	public float Durability = 10;
	public int HeightLevel = 0;
	public float yIncrease = 2f;
	public bool InteractionEffect = false;
	public int InteractionTrysToEffect = 0;
	private Hex eHex;
	public Transform hexMap;
	public Sprite[] loadInSprites;
	private Sprite currentLoadInSprite;
	public int TeleportEndHexRow = 0;
	public int TeleportEndHexNum = 0;

	public void Start() {
		component = this.transform;
		currentLoadInSprite = loadInSprites [0];
	}

	public void Interact() {
		if (InteractionEffect == true) {
				if (InteractionTrysToEffect != 0) {
					InteractionTrysToEffect -= 1;
				}
				if (InteractionTrysToEffect == 0) {
				   Effect ();
				}
		}
	}

	public void Effect() {
		if (typeOfTerrain == TerrainType.Door) {
			currentLoadInSprite = loadInSprites [1];
			IsPassable = true;
		}
	}

	public void Update() {
		foreach (Transform theHex in hexMap) {
			string hexName = currentHexRow + " " + currentHexNum;
			if (theHex.name == hexName) {
				component.position = new Vector3 (theHex.position.x, theHex.position.y + yIncrease, theHex.position.z);
				eHex = theHex.GetComponent<Hex> ();
				eHex.OnHex = "Terrain";
				eHex.objectOnhex = component;
				HeightLevel = eHex.HeightLevel;
			}
		}
		this.GetComponent<SpriteRenderer> ().sprite = currentLoadInSprite;

	}
}

public enum TerrainType{Wall,Door,Teleporter}
