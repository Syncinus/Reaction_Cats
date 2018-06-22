using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameStarter : MonoBehaviour {

	public CatSelectionManager cSelectManager;
	public GameObject cSelectPanel;
	public static PlayerAssign pAssign;

	public void BeginGame() {
		//CmdBeginGame ();
		Debug.Log ("Beginning Game!");
//		CmdSetCats ();
		if (pAssign.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) {
			//pAssign.Prepared = true;
			pAssign.SetPrepared(true);
			cSelectPanel.SetActive (false);
		}
	}

//	[Command] public void CmdSetCats() {
//		RpcSetCats ();
//	}

	//[ClientRpc] public void RpcSetCats() {
	//	pAssign.NumberOfCats = cSelectManager.selectedCats.Count;
	//	//pAssign.catsToSpawn = cSelectManager.selectedCats;
	//}



//	[Command] public void CmdBeginGame() {
//		RpcBeginGame ();
//	}

//	[ClientRpc] public void RpcBeginGame() {
  //     //	pAssign.NumberOfCats = cSelectManager.selectedCats.Count;
	//	pAssign.catsToSpawn = cSelectManager.selectedCats;
	//	pAssign.Ready = true;
	//	cSelectPanel.SetActive (false);
	//}
}
