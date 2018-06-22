using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class CatSelectionSlot : MonoBehaviour {
	//public static const struct Cat;

	public Image icon;
	public CatSelectionManager cSelectionManager;
	public Cat currentCat;
	public Transmitter transmitDevice;

	public void RemoveCat() {
		icon.sprite = null;
		icon.enabled = false;
		currentCat = null;
	}

	public void Update() {
		if (currentCat != null) {
			icon.sprite = currentCat.CatIcon;
			icon.enabled = true;
		}
	}

	public void Select() {
		if (currentCat != null) {
			//Send A Signal To Transmitter To Transmit Cat.
			NetworkInstanceId playerNetworkId;
			foreach (PlayerAssign cur in Object.FindObjectsOfType<PlayerAssign>()) {
				if (cur.gameObject.GetComponent<NetworkIdentity> ().isLocalPlayer) {
					playerNetworkId = cur.gameObject.GetComponent<NetworkIdentity> ().netId;
					cur.AddCat (currentCat, playerNetworkId);
					RemoveCat ();
				}
			}
		}
	}

	public void AddCat(Cat newCat) {
		currentCat = newCat;
		icon.sprite = newCat.CatIcon;
		icon.enabled = true;
	}

	//[Command] public void CmdSelect() {
	//	RpcSelect ();
	//	//Cat newCatToLoad = new Cat ();
	//	//PlayerAssign.instance.catsToSpawn.Add (newCatToLoad);
//	}
		
//	[ClientRpc] public void RpcSelect() {
//		PlayerAssign.instance.catsToSpawn.Add (currentCat);
//	}
}
