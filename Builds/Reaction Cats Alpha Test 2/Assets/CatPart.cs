using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CatPart : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	public CatPartTargeting targetingUI;
	public Transform TargetablePart;
	public Image rend;
	public string PanelName;
	public Random derpmaster;
	public string Type = "Torso";
	public bool Crippled = false;
	public bool AttackHit;
	// Use this for initialization
	void Start () {
		rend = GameObject.Find (PanelName).GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		TargetablePart = targetingUI.Target;
	}
	public void OnPointerEnter(PointerEventData eventdata) {
		rend.color = new Color32 (255, 0, 0, 255);	
	}
	public void OnPointerExit(PointerEventData eventdata) {
		rend.color = new Color32 (255, 255, 255, 100);
	}
	public void OnPointerClick(PointerEventData eventdata) {
		if (Crippled == false) {
			AttackHit = false;
			//targetingUI.gameObject.SetActive (false);
			targetingUI.DestroyMyself();
			rend.color = new Color32 (20, 255, 11, 100);
			//var aSpeedMult = Random.Range (1, 6);
			//var dSpeedMult = Random.Range (1, 6);
			var VisionIncrease = Random.Range (1, 12);
			float Accuracy = targetingUI.Vision + VisionIncrease;
			float attSpeed = targetingUI.attSpeed + Random.Range (1, 20);
			float defSpeed = targetingUI.defSpeed + Random.Range (1, 20);
			print (attSpeed.ToString ());
			print (defSpeed.ToString ());
			//float attSpeed = targetingUI.SpeedValue * aSpeedMult;
			//float defSpeed = targetingUI.TargetSpeed * dSpeedMult;
			float att = targetingUI.AttackValue;
			float def = targetingUI.TargetDefense;
			float attCalc = att * att;
			float defCalc = att + def;
			float DamageCalculation = attCalc / defCalc;
			float Damage = Mathf.Ceil (DamageCalculation) - targetingUI.DamageReduction;
			if (Accuracy <= 4) {
				Damage = Damage / 4;
			}
			if (Accuracy >= 5 && Accuracy <= 8) {
				Damage = Damage / 2;
			}
			if (Accuracy >= 9 && Accuracy <= 10) {
				float QuarterDamage = Damage = Damage / 4;
				Damage = QuarterDamage * 3;
			}
			if (Accuracy >= 11 && Accuracy <= 14) {
				//Damage = Damage;
			}
			if (Accuracy >= 15 && Accuracy <= 16) {
				Damage = Damage + Damage / 4;
			}
			if (Accuracy >= 17 && Accuracy <= 18) {
				Damage = Damage + Damage / 2;
			}
			if (Accuracy >= 19 && Accuracy <= 22) {
				Damage = Damage * 2;
			}
			if (Accuracy > 22) {
				Damage = Damage * 3;
			}
			//	print (aSpeedMult.ToString ());
			//	print (dSpeedMult.ToString ());
			DesComp thingToDamage = TargetablePart.GetComponent<DesComp> ();
			Player playerToDamage = TargetablePart.GetComponent<Player> ();
			//print ("Att Speed: " + attSpeed.ToString () + " Random: " + aSpeedMult);
			//print ("Def Speed: " + defSpeed.ToString () + " Random: " + dSpeedMult);
			if (thingToDamage != null) {
				if (attSpeed >= defSpeed) {
					if (Type == "Torso") {
						thingToDamage.TorsoHP -= Damage;
						print ("Hit!");
						AttackHit = true;
						thingToDamage.TorsoHP = Mathf.Floor (thingToDamage.TorsoHP);
					}
					if (Type == "Tail") {
						if (Accuracy >= 17) {
							thingToDamage.TailHP -= Damage;
							AttackHit = true;
							print ("Hit!");
							thingToDamage.TailHP = Mathf.Floor (thingToDamage.TailHP);

						} else {
							print ("Missed!");

						}
					}
					if (Type == "Leg1") {
						if (Accuracy >= 15) {
							thingToDamage.Leg1HP -= Damage;
							AttackHit = true;
							print ("Hit!");
							thingToDamage.Leg1HP = Mathf.Floor (thingToDamage.Leg1HP);

						} else {
							print ("Missed!");

						}
					}
					if (Type == "Leg2") {
						if (Accuracy >= 15) {
							thingToDamage.Leg2HP -= Damage;
							AttackHit = true;
							print ("Hit!");
							thingToDamage.Leg2HP = Mathf.Floor (thingToDamage.Leg2HP);

						} else {
							print ("Missed!");

						}
					}
					if (Type == "Leg3") {
						if (Accuracy >= 15) {
							thingToDamage.Leg3HP -= Damage;
							AttackHit = true;
							print ("Hit!");
							thingToDamage.Leg3HP = Mathf.Floor (thingToDamage.Leg3HP);

						} else {
							print ("Missed!");

						}
					}
					if (Type == "Leg4") {
						if (Accuracy >= 15) {
							thingToDamage.Leg4HP -= Damage;
							AttackHit = true;
							print ("Hit!");
							thingToDamage.Leg4HP = Mathf.Floor (thingToDamage.Leg4HP);

						} else {
							print ("Missed!");

						}
					}
					if (Type == "Head") {
						if (Accuracy >= 17) {
							thingToDamage.HeadHP -= Damage;
							AttackHit = true;
							print ("Hit!");
							thingToDamage.HeadHP = Mathf.Floor (thingToDamage.HeadHP);
						} else {
							print ("Missed!");
						}
					}
				} else {
					AttackHit = false;
					print ("Missed!");
				}
			}
			if (playerToDamage != null) {
				if (attSpeed >= defSpeed) {
					if (Type == "Torso") {
						playerToDamage.TorsoHP -= Damage;
						print ("Hit!");
						AttackHit = true;
						playerToDamage.TorsoHP = Mathf.Floor (playerToDamage.TorsoHP);
					}
					if (Type == "Tail") {
						if (Accuracy >= 17) {
							playerToDamage.TailHP -= Damage;
							AttackHit = true;
							print ("Hit!");
							playerToDamage.TailHP = Mathf.Floor (playerToDamage.TailHP);

						} else {
							print ("Missed!");

						}
					}
					if (Type == "Leg1") {
						if (Accuracy >= 15) {
							playerToDamage.Leg1HP -= Damage;
							AttackHit = true;
							print ("Hit!");
							playerToDamage.Leg1HP = Mathf.Floor (playerToDamage.Leg1HP);

						} else {
							print ("Missed!");

						}
					}
					if (Type == "Leg2") {
						if (Accuracy >= 15) {
							playerToDamage.Leg2HP -= Damage;
							AttackHit = true;
							print ("Hit!");
							playerToDamage.Leg2HP = Mathf.Floor (playerToDamage.Leg2HP);

						} else {
							print ("Missed!");

						}
					}
					if (Type == "Leg3") {
						if (Accuracy >= 15) {
							playerToDamage.Leg3HP -= Damage;
							AttackHit = true;
							print ("Hit!");
							playerToDamage.Leg3HP = Mathf.Floor (playerToDamage.Leg3HP);

						} else {
							print ("Missed!");

						}
					}
					if (Type == "Leg4") {
						if (Accuracy >= 15) {
							playerToDamage.Leg4HP -= Damage;
							AttackHit = true;
							print ("Hit!");
							playerToDamage.Leg4HP = Mathf.Floor (playerToDamage.Leg4HP);

						} else {
							print ("Missed!");

						}
					}
					if (Type == "Head") {
						if (Accuracy >= 17) {
							playerToDamage.HeadHP -= Damage;
							AttackHit = true;
							print ("Hit!");
							playerToDamage.HeadHP = Mathf.Floor (playerToDamage.HeadHP);
						} else {
							print ("Missed!");
						}
					}
				} else {
					AttackHit = false;
					print ("Missed!");
				}
			}
		}
	}
}
