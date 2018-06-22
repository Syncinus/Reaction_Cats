using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPartTargeting : MonoBehaviour {
	public GameObject torso;
	public GameObject head;
	public GameObject leg1;
	public GameObject leg2;
	public GameObject leg3;
	public GameObject leg4;
	public GameObject tail;
	public Transform Target;
	public float attSpeed;
	public float defSpeed;
	public float AttackValue;
	public float TargetDefense;
	public float SpeedValue;
	public float TargetSpeed;
	public float SpeedIncreaseA = 0;
	public float SpeedIncreaseD = 0;
	public float DamageReduction;
	public float Vision;
	// Use this for initialization
	void Start () {
		
	}

	public void DestroyMyself() {
		Destroy (this.gameObject);
	}
	// Update is called once per frame
	void Update () {
		float IntoThingRdA = SpeedValue / 3;
		float IntoThingRdD = TargetSpeed / 3;
		float attBattleSpeed = SpeedValue - IntoThingRdA;
		float defBattleSpeed = TargetSpeed - IntoThingRdD;
		float IntoThingInA = 10 - attBattleSpeed / 2;
		float IntoThingInD = 10 - defBattleSpeed / 2;
		attSpeed = Mathf.Round(attBattleSpeed + IntoThingInA) + SpeedIncreaseA;
		defSpeed = Mathf.Round(defBattleSpeed + IntoThingInD) + SpeedIncreaseD;
	    var TargetScript = Target.GetComponent<DesComp> ();
		var PlayerScript = Target.GetComponent<Player> ();
		if (TargetScript != null) {
			if (TargetScript.TorsoHP <= 0) {
				var TorsoScripter = torso.GetComponent<CatPart> ();
				TorsoScripter.rend.color = new Color32 (0, 0, 0, 255);
				TorsoScripter.Crippled = true;
			}
			if (TargetScript.Leg1HP <= 0) {
				var TorsoScripter = leg1.GetComponent<CatPart> ();
				TorsoScripter.rend.color = new Color32 (0, 0, 0, 255);
				TorsoScripter.Crippled = true;
			}
			if (TargetScript.Leg2HP <= 0) {
				var TorsoScripter = leg2.GetComponent<CatPart> ();
				TorsoScripter.rend.color = new Color32 (0, 0, 0, 255);
				TorsoScripter.Crippled = true;
			}
			if (TargetScript.TailHP <= 0) {
				var TorsoScripter = tail.GetComponent<CatPart> ();
				TorsoScripter.rend.color = new Color32 (0, 0, 0, 255);
				TorsoScripter.Crippled = true;
			}
			if (TargetScript.HeadHP <= 0) {
				var TorsoScripter = head.GetComponent<CatPart> ();
				TorsoScripter.rend.color = new Color32 (0, 0, 0, 255);
				TorsoScripter.Crippled = true;
			}
		}
		if (PlayerScript != null) {
			if (PlayerScript.TorsoHP <= 0) {
				var TorsoScripter = torso.GetComponent<CatPart> ();
				TorsoScripter.rend.color = new Color32 (0, 0, 0, 255);
				TorsoScripter.Crippled = true;
			}
			if (PlayerScript.Leg1HP <= 0) {
				var TorsoScripter = leg1.GetComponent<CatPart> ();
				TorsoScripter.rend.color = new Color32 (0, 0, 0, 255);
				TorsoScripter.Crippled = true;
			}
			if (PlayerScript.Leg2HP <= 0) {
				var TorsoScripter = leg2.GetComponent<CatPart> ();
				TorsoScripter.rend.color = new Color32 (0, 0, 0, 255);
				TorsoScripter.Crippled = true;
			}
			if (PlayerScript.TailHP <= 0) {
				var TorsoScripter = tail.GetComponent<CatPart> ();
				TorsoScripter.rend.color = new Color32 (0, 0, 0, 255);
				TorsoScripter.Crippled = true;
			}
			if (PlayerScript.HeadHP <= 0) {
				var TorsoScripter = head.GetComponent<CatPart> ();
				TorsoScripter.rend.color = new Color32 (0, 0, 0, 255);
				TorsoScripter.Crippled = true;
			}
		}
	}
}
