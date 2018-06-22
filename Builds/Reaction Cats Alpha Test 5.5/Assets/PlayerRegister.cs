using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerRegister : NetworkBehaviour {
   /* NetworkManager netMng;
	NetworkClient neClient;
	public int playersConnected = 0;

	void Start() {
		netMng = GameObject.Find ("NetworkManager").GetComponent<NetworkManager> ();
		if (isServer) {
			StartCoroutine (RegisterThings ());
		}
		if (isClient) {
			neClient = netMng.client;
		}
	}

	//Client Things

	[ClientRpc] public void Rpc_Client(string Thing) {
		if (!isClient) {
			return;
		}
	}

	public void ClientSendThing(string ThingData) {
		if (!isClient) {
			return;
		}
		ThingMessage thingMes = new ThingMessage ();
		thingMes.Thing = ThingData;
		neClient.Send (MsgType.Highest + 2, thingMes);
		Debug.Log ("Registered Thing Message On NetworkClient!");
     }

	IEnumerator RegisterThings() {
		if (isServer) {
			yield return new WaitUntil (() => NetworkServer.connections.Count > 0);
			NetworkServer.RegisterHandler (MsgType.Highest + 2, OnMessageRX);

			//GameObject.FindGameObjectWithTag ("PlayerSystem(Clone)").GetComponent<PlayerAssign> ().PlayerID = 1;
			//GameObject.FindGameObjectWithTag ("PlayerAssignHost").GetComponent<PlayerAssign> ().playerRegistered = true;
		}
	}

	//Server Things

	public void Update() {
		if (!isServer) {
			return;
		}

		if (netMng.numPlayers != playersConnected) {
			if (netMng.numPlayers < 2) {
				playersConnected = netMng.numPlayers;
			}

			if (netMng.numPlayers > playersConnected) {
				Debug.Log ("Player Has Connected.");
				StartCoroutine (PlayerConnected (3));
				playersConnected = netMng.numPlayers;
			}

			Debug.Log ("Network MSG: Players Connected To Host: " + (playersConnected - 1).ToString ());
		}
	}

	IEnumerator PlayerConnected(float WaitTime) {
		yield return new WaitForSeconds (WaitTime);
		Debug.Log ("Network Msg: Waited For Client To Connect, To Send Some Kind Of Info For: " + WaitTime.ToString ());

	}

	public void Reset() {
		if (!isServer) {
			return;
		}
	}

	public void OnMessageRX(NetworkMessage netMsg) {
		Debug.Log ("Message Recived!");
		if (!isServer) {
			return;
		}
		ThingMessage msg = netMsg.ReadMessage<ThingMessage> ();
	}
	*/
}

public class ThingMessage : MessageBase 
{
	public string Thing;
}
