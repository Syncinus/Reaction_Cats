using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Transmitter : NetworkBehaviour {
//	public Cat transmitSignal;
//
	//public void Start() {
	//	if (hasAuthority) {
	///		Debug.Log ("Transmitter Has Authority.");
	//	}
	//}

	//public void Transmit(Cat transmit, NetworkInstanceId playerNetworkId) {
	//	transmitSignal = transmit;
	//	CmdTransmit (playerNetworkId);
	//}
	//
//	[Command] public void CmdTransmit(NetworkInstanceId playerNetworkId) {
//		RpcTransmit (playerNetworkId);
///	}
//
//	[ClientRpc] public void RpcTransmit(NetworkInstanceId playerNetworkId) {
//		//if (PlayerAssign.instance.gameObject.GetComponent<NetworkIdentity> ().isLocalPlayer) {
///		//	PlayerAssign.instance.catsToSpawn.Add (transmitSignal);
///	//	}
///		GameObject transmitReciver = ClientScene.FindLocalObject(playerNetworkId);
//		transmitReciver.GetComponent<PlayerAssign> ().catsToSpawn.Add (transmitSignal);
///		//Debug.Log ("Transmitted A Cat!");
//	}
}
