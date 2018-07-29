using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesComp : MonoBehaviour {
	//Note, This Is Being Treated Like A Cat For Testing Purposes, This Will Be Coded Into Player Aswell For Multiplayer PVP.
	public int curHexRow;
	public int curHexNum;
	public Transform component;
	public float TorsoHP = 30;
	public float TailHP = 5;
	public float Leg1HP = 5;
	public float Leg2HP = 5;
	public float Leg3HP = 5;
	public float Leg4HP = 5;
	public float HeadHP = 5;
	public float speed = 10;
	//private float yIncrease = 10f;
	public int HeightLevel;
	private Hex edit;
	public Transform hexMap;
	public string CatName = "Zeus"; //Temporary
	public float DefenseValue = 10;

	// Use this for initialization
	void Start () {
		component = this.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform hex in hexMap) {
			string selector = curHexRow.ToString () + " " + curHexNum.ToString ();
			if (hex.name == selector) {
				component.position = hex.position;
				edit = hex.GetComponent<Hex> ();
				edit.OnHex = "DesComp";
				edit.objectOnhex = component;
				HeightLevel = edit.HeightLevel;
			}
		}
		//if (TorsoHP <= 0) {
		//	edit.OnHex = "";
		///	edit.objectOnhex = null;
		//	Destroy (gameObject);
		//}
	}

	public void TakeDamage(float Damage) {
		TorsoHP -= Mathf.Floor (Damage);
		if (System.Single.IsNaN(TorsoHP)) {
			TorsoHP = 0;
		}
		if (TorsoHP <= 0) {
			TorsoHP = 0;
			edit.OnHex = "";
			edit.objectOnhex = null;
			this.gameObject.SetActive (false);
		}
	}
}
