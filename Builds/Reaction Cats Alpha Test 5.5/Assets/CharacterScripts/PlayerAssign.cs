using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System.Linq;
using System;
using System.IO;
using System.Globalization;

public struct abilityHolder {
	public string FileName;
	public string Name;
	public int AbilityID;
	public bool isDefault;
	public string AbilityType;

	public abilityHolder(string filename, string name, int abilityid, bool isdefault, string abilitytype) {
		FileName = filename;
		Name = name;
		AbilityID = abilityid;
		isDefault = isdefault;
		AbilityType = abilitytype;
	}
}

public struct attackHolder {
	public string FileName;
	public int Range;
	public int DamageIncrease;
	public int EnemyDefenseReduction;
	public string AttackName;

	public attackHolder(string filename, int range, int damageincrease, int enemydefensereduction, string attackname) {
		FileName = filename;
		Range = range;
		DamageIncrease = damageincrease;
		EnemyDefenseReduction = enemydefensereduction;
		AttackName = attackname;
	}
}

public class PlayerAssign : NetworkBehaviour {

	#region Singleton
	public static PlayerAssign instance;

	void Awake() {
		if (instance != null) {
			Debug.LogWarning ("More Than One Instance Of PlayerAssign Detected On A Single Client, Exiting!");
			return;
		}
		instance = this;
	}
	#endregion

	public override void OnStartClient() {
		if (!TurnSystem.players.Contains (this)) {
			TurnSystem.players.Add (this);
		}
	}

	//Synchronize
	[SyncVar] public NetworkInstanceId parentNetId;
	public List<Player> catlist = new List<Player>();
	[SyncVar] public int NumberOfCats;
	public GameObject catPrefab;
	public Transform spawnlocation;
	NetworkClient myClient;
	public CatSelectionManager cLoader;
	public int currentlyControlling = 1;
	public static Player controllingplayer;
	[SyncVar] public int PlayerID;
	public bool DoingPassive = true;
	public bool DoingActive = false;
	public List<Cat> catsToSpawn = new List<Cat>();
	public bool AssignedParentCallback = false;
	public bool AllPlayersDonePassive = false;
	public bool AllPlayersDoneActive = false;
	public NetworkInstanceId catNetId;
	public bool playerRegistered = false;
	public bool PlayersDoneActiveThings;
	public bool Running = false;
	public bool Prepared = false;
	public bool SpawnedCats = false;
	public Cat catToAdd;
	public int Team;
	public Camera mainCamera;
	public GameObject ThatThing;
	public bool Lock = true;
	public bool NeedingCatSelected = false;
	public bool DoingSomething = false;
	public bool FinishedOfficalPassive = false;
	public bool FinishedOfficalActive = false;
	[SyncVar] public bool finishedPassive = false;
	[SyncVar] public bool finishedActive = false;

//	public override void OnStartClient()
//	{
///		// When we are spawned on the client,
//		// find the parent object using its ID,
//		// and set it to be our transform's parent.
//		//GameObject parentObject = ClientScene.FindLocalObject(parentNetId);
//		//transform.SetParent(parentObject.transform);
//			GameObject catObject = ClientScene.FindLocalObject (catNetId);
//			catObject.transform.SetParent (spawnlocation);
//	}

//	public void SetupSync() {
//		Debug.Log ("Setting Up Sync!");
//		Cmd_SendSyncToServer ();
//	}

//	[Command] public void Cmd_SendSyncToServer() {
//		RpcSyncThings ();
//	}

	//[ClientRpc] public void RpcSyncThings() {
	//	
	//}
	/*
	public void AddCat(Cat CatToRecive, NetworkInstanceId netInstanceId) {
		//catToAdd = CatToRecive;
		ThatThing = Instantiate (catPrefab);
		ThatThing.GetComponent<Player> ().Dummy = true;
		//ClientScene.RegisterPrefab (myCatThing);
		//NetworkServer.Spawn (myCatThing);
		CmdNetworkSpawn(ThatThing);
		CmdMakePlayerDummy (ThatThing);
		ThatThing.GetComponent<Player> ().curcat = CatToRecive;
		//NetworkInstanceId theId = ThatThing.GetComponent<NetworkIdentity> ().netId;
		//Debug.Log (theId.ToString ());
		CmdAddCat (netInstanceId, ThatThing);
	}

	[Command] public void CmdNetworkSpawn(GameObject objectToSpawn) {
		ClientScene.RegisterPrefab (objectToSpawn);
		NetworkServer.Spawn (objectToSpawn);
	}

	[Command] public void CmdMakePlayerDummy(GameObject theDummy) {
		RpcMakePlayerDummy (theDummy);
	}

	[ClientRpc] public void RpcMakePlayerDummy(GameObject theDummy) {
		theDummy.GetComponent<Player> ().Dummy = true;
	}

	[Command] public void CmdAddCat(NetworkInstanceId netInstanceId, NetworkInstanceId dummyCatId) {
		//GameObject obj;
		//if (Network.isClient) {
		//	obj = ClientScene.FindLocalObject (dummyCatId);
		//} else {
		//	obj = NetworkServer.FindLocalObject (dummyCatId);
		//}
		ThatCat.GetComponent<Player>().curcat = catToAdd;
		ThatCat.transform.name = "Dat Boi";
		RpcAddCat (netInstanceId, ThatCat);
		//RpcAddCat (netInstanceId, CatsName, dummyCat);
		//GameObject transmitReciver = ClientScene.FindLocalObject(netInstanceId);
		//if (transmitReciver == null) {
		//	Debug.LogError ("Player That Is To Recive Things Is Null!");
		//}
		//PlayerAssign reciverScript = transmitReciver.GetComponent<PlayerAssign> ();
		//Cat myGoodCat = dummyCat.GetComponent<Player>().curcat;
		//reciverScript.catToAdd = myGoodCat;
	}

	[ClientRpc] public void RpcAddCat(NetworkInstanceId netInstanceId, NetworkInstanceId dummyCatId) {
		GameObject transmitReciver = ClientScene.FindLocalObject(netInstanceId);
		if (transmitReciver == null) {
			Debug.LogError ("Player That Is To Recive Things Is Null!");
		}
		PlayerAssign reciverScript = transmitReciver.GetComponent<PlayerAssign> ();
		//GameObject trueObj;
		//if (Network.isClient) {
		//	trueObj = ClientScene.FindLocalObject (dummyCatId);
		//} else {
		//	trueObj = NetworkServer.FindLocalObject (dummyCatId);
		//}
		Cat myGoodCat = ThatCat.GetComponent<Player> ().curcat;
		reciverScript.catToAdd = myGoodCat;
		//Cat catToRun = Resources.Load<Cat> ("Cats/" + CatsName);
		if (!reciverScript.catsToSpawn.Contains (myGoodCat)) {
			reciverScript.catsToSpawn.Add (myGoodCat);
		}
		NetworkServer.UnSpawn (ThatCat);
		Destroy (ThatCat);
		//if (!reciverScript.catsToSpawn.Contains (catToAdd)) {
		//	reciverScript.catsToSpawn.Add (catToAdd);
		//	Debug.Log ("Player Recived A Cat!");
		//}
	}
	*/

	public void AddCat(Cat CatToRecieve, NetworkInstanceId netInstanceId) {
		float CatDefense = CatToRecieve.Defense;
		float CatAttack = CatToRecieve.Attack;
		float CatTorsoHP = CatToRecieve.TorsoHP;
		float CatMaxSpeed = CatToRecieve.MaxSpeed;
		string TheCatName = CatToRecieve.CatName;
		float CatTravelSpeed = CatToRecieve.TravelSpeed;
		int CatMaxStamina = CatToRecieve.MaxStamina;
		int CatAttackSpeed = CatToRecieve.AttackSpeed;
		int CatAttackRecharge = CatToRecieve.AttackRecharge;
	    //
		//PACKAGING DATA
		//
		List<String> PackagedDataList = new List<String>();
		List<abilityHolder> theAbilitys = new List<abilityHolder> ();
		List<attackHolder> theAttacks = new List<attackHolder> ();
		int AbilityCount = CatToRecieve.abilitys.Count;
		int AttackCount = CatToRecieve.attacks.Count;
		PackagedDataList.Add (CatToRecieve.catRarity.ToString ());
		foreach (Ability theAbility in CatToRecieve.abilitys) {
			abilityHolder holder = new abilityHolder (theAbility.name, theAbility.Name, theAbility.AbilityID, false, theAbility.AbilityType);
			theAbilitys.Add (holder);
		}
		foreach (Attack theAttack in CatToRecieve.attacks) {
			attackHolder holder = new attackHolder (theAttack.name, theAttack.Range, theAttack.DamageIncrease, theAttack.EnemyDefenseReduction, theAttack.AttackName);
			theAttacks.Add (holder);
		}
		//foreach (Attack thatAttack in CatToRecieve.attacks) {
		//	PackagedDataList.Add (thatAttack.name);
		//}
		abilityHolder[] abilitys = theAbilitys.ToArray();
		attackHolder[] attacks = theAttacks.ToArray ();
		String[] PackagedData = PackagedDataList.ToArray ();
		CmdAddCat (CatDefense, CatAttack, CatTorsoHP, CatMaxSpeed, TheCatName, CatTravelSpeed, CatMaxStamina, CatAttackSpeed, CatAttackRecharge, PackagedData, netInstanceId, AbilityCount, abilitys, attacks);
	}

	[Command] public void CmdAddCat(float CDefense, float CAttack, float CTorsoHP, float CMaxSpeed, string CName, float CTravel, int CStamina, int CAtSpeed, int CARecharge, string[] PackagedData, NetworkInstanceId netInstanceId, int AbilityCount, abilityHolder[] abilitys, attackHolder[] attacks) {
		RpcAddCat (CDefense, CAttack, CTorsoHP, CMaxSpeed, CName, CTravel, CStamina, CAtSpeed, CARecharge, PackagedData, netInstanceId, AbilityCount, abilitys, attacks);
	}

	[ClientRpc] public void RpcAddCat(float CDefense, float CAttack, float CTorsoHP, float CMaxSpeed, string CName, float CTravel, int CStamina, int CAtSpeed, int CAtRecharge, string[] PackagedData, NetworkInstanceId netInstanceId, int AbilityCount, abilityHolder[] abilitys, attackHolder[] attacks) {
		GameObject reciever = ClientScene.FindLocalObject (netInstanceId);
		PlayerAssign PlayerScript = reciever.GetComponent<PlayerAssign> ();
		List<String> ExtractedData = PackagedData.ToList ();
		List<abilityHolder> ExtractedAbilitys = abilitys.ToList ();
		List<attackHolder> ExtractedAttacks = attacks.ToList ();
		Rarity CatRarity = (Rarity)System.Enum.Parse (typeof(Rarity), ExtractedData.ElementAt (0));
		Cat ThatCat = (Cat)ScriptableObject.CreateInstance ("Cat") as Cat;
		ThatCat.catRarity = CatRarity;
		ThatCat.Defense = CDefense;
		ThatCat.Attack = CAttack;
		ThatCat.TorsoHP = CTorsoHP;
		ThatCat.MaxSpeed = CMaxSpeed;
		ThatCat.CatName = CName;
		ThatCat.TravelSpeed = CTravel;
		ThatCat.MaxStamina = CStamina;
		ThatCat.AttackSpeed = CAtSpeed;
		ThatCat.AttackRecharge = CAtRecharge;
		foreach (abilityHolder holder in ExtractedAbilitys) {
			Ability theAbility = (Ability)ScriptableObject.CreateInstance ("Ability") as Ability;
			theAbility.name = holder.FileName;
			theAbility.Name = holder.Name;
			theAbility.AbilityID = holder.AbilityID;
			theAbility.isDefault = holder.isDefault;
			theAbility.AbilityType = holder.AbilityType;
			ThatCat.abilitys.Add (theAbility);
		}
		foreach (attackHolder holder in ExtractedAttacks) {
			Attack theAttack = (Attack)ScriptableObject.CreateInstance ("Attack") as Attack;
			theAttack.name = holder.FileName;
			theAttack.Range = holder.Range;
			theAttack.DamageIncrease = holder.DamageIncrease;
			theAttack.EnemyDefenseReduction = holder.EnemyDefenseReduction;
			theAttack.AttackName = holder.AttackName;
			ThatCat.attacks.Add (theAttack);
		}
		//for (int i = 0; i < AttackCount; i++) {
		//	string ThatAttacksName = ExtractedData.ElementAt (i + AbilityCount);
		//	Attack ThatAttack = Resources.Load<Attack> ("Attacks/" + ThatAttacksName);
		//	ThatCat.attacks.Add (ThatAttack);
		//}
		PlayerScript.catToAdd = ThatCat;
		PlayerScript.catsToSpawn.Add (ThatCat);
	}

	// Use this for initialization
	void Start () {
		//if (!isLocalPlayer) {
		//	return;
		//}
		mainCamera = Camera.main;
		StartCoroutine (StartUp ());
		CatSelectionUI.EnabledThings = true;
		parentNetId = this.gameObject.GetComponent<NetworkIdentity> ().netId;
		if (isLocalPlayer) {
			GameStarter.pAssign = this;
			if (GameStarter.pAssign == this) {
				Debug.Log ("Player Has Been Registered With Game Starter!");
			}
		}

		if (this.hasAuthority) {
			Debug.Log ("This Player Has Authority");
		}
		if (isServer) {
			//GameObject transmitter = GameObject.Find ("Transmitter");
			//CmdAssignAuthority (transmitter.GetComponent<NetworkIdentity> (), this.GetComponent<NetworkIdentity> ());
		}
		//if (isLocalPlayer) {
		//	GameObject transmitter = GameObject.Find ("Transmitter");
		//	transmitter.GetComponent<NetworkIdentity> ().AssignClientAuthority (connectionToClient);
		//}


		//if (GameStarter.pAssign == this) {
		//	Debug.Log ("Player Has Been Registered With Game Starter!");
		//}
	}

	IEnumerator StartUp() {
		CmdAddToPlayerList ();
		yield return new WaitUntil (() => Running == true);
		cLoader = CatSelectionManager.instance;
		spawnlocation = this.transform;
		//PlayerID = NetworkManager.singleton.numPlayers;
		//this.transform.name = "Player: " + PlayerID;
		if (!isLocalPlayer) {
			yield return null;
		}

		if (hasAuthority) {
			print ("This Player Is Authorized.");
		}

		//if (this.isServer) {
		//	this.transform.name = "PlayerAssignHost";
		//}

		//	NetworkServer.Listen (7777);
		//	NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
		//if (!isLocalPlayer) {
		//	yield return null;
		//}

		//if (isLocalPlayer) {
		if (SpawnedCats == false) {
			for (int i = 0; i < catsToSpawn.Count; i++) {
				if (isLocalPlayer)
					Cmd_SpawnCat (i);
			}
			CmdFinishedSpawning (true);
		}
		//}

		//currentlyControlling = 1;
		//controllingplayer = catlist [currentlyControlling - 1];
		NeedingCatSelected = true;
		AllPlayersDoneActive = false;
		AllPlayersDonePassive = false;
		//for (int i = 0; i < NumberOfCats; i++) {
		//	GameObject playerCat = Instantiate (catPrefab, this.transform);
		//	NetworkServer.SpawnWithClientAuthority(playerCat, connectionToClient);
		//}

	}

	[Command] public void CmdFinishedSpawning(bool CatsSpawned) {
		RpcFinishedSpawning (CatsSpawned);
	}

	[ClientRpc] public void RpcFinishedSpawning(bool CatsSpawned) {
		this.SpawnedCats = CatsSpawned;
	}

	[Command] public void CmdAddToPlayerList() {
		RpcAddToPlayerList ();
	}


	[ClientRpc] public void RpcAddToPlayerList() {
		if (!TurnSystem.players.Contains (this)) {
			TurnSystem.players.Add (this);
		}
	}

	//[Command] public void CmdSetPlayerId(int SetPlayerID) {
	////	RpcSetPlayerId (SetPlayerID);
	//}

	//[ClientRpc] public void RpcSetPlayerId(int SetPlayerID) {
		//
	//}


	public void SetupPassiveReset() {
		Player.donePassive = false;
		TurnSystem.pasStop = false;
		TurnSystem.passiveAction = true;
		ActionInitilized.disablePassive = false;
	}

	public void DisablePassive() {
		currentlyControlling = 1;
		DoingPassive = false;
		DoingActive = true;
		Player.donePassive = true;
		TurnSystem.pasStop = true;
		TurnSystem.passiveAction = false;
		ActionInitilized.disablePassive = true;
	}

	public void SetupActiveReset() {
		Player.doneActive = false;
		TurnSystem.actStop = false;
		TurnSystem.activeAction = true;
		ActionInitilized.disableActive = false;
	}

	[Command] public void Cmd_SpawnCat(int iThing) {
		if (!SpawnedCats == false) {
			return;
		}
		var TheCat = (GameObject)Instantiate (catPrefab) as GameObject;
		TheCat.GetComponent<Player> ().Dummy = false;
		ClientScene.RegisterPrefab (TheCat);
		TheCat.transform.SetParent (spawnlocation);
		TheCat.GetComponent<Player> ().parentNetId = parentNetId;
		TheCat.GetComponent<Player> ().curcat = catsToSpawn.ElementAt (iThing);
		NetworkServer.SpawnWithClientAuthority (TheCat, connectionToClient);
		//var playerCat = TheCat.GetComponent<Player> () as Player;
		//catlist.Add (playerCat);
		catNetId = TheCat.GetComponent<NetworkIdentity> ().netId;
		Rpc_SyncParent (catNetId, parentNetId, iThing);
		Debug.Log ("Cat Spawning Successful");
	}

	[ClientRpc] public void Rpc_SyncParent(NetworkInstanceId netIdSystem, NetworkInstanceId parentNetworkId, int iThing) {
		Debug.Log ("Syncing Successful");
		catNetId = netIdSystem;
		GameObject catThing = ClientScene.FindLocalObject (netIdSystem);
	//	GameObject parentThing = ClientScene.FindLocalObject (parentNetworkId);
		catThing.transform.SetParent (this.transform);
		catThing.GetComponent<Player> ().curcat = catsToSpawn.ElementAt (iThing);
	//	parentThing.GetComponent<PlayerAssign> ().catlist.Add (catThing.GetComponent<Player> ());
	}
	/*
	//public void SpawnCat() {
	//	//if (!isLocalPlayer) {
	//	//	return;
	//	//}
	//	print("Spawning Cats!");
	///	for (int i = 0; i < NumberOfCats; i++) {
	///		CmdSpawnCat (this.gameObject);
	//	}
	}

	[Command] public void CmdSpawnCat(GameObject CatParent) {
		GameObject spawnedCat = (GameObject)GameObject.Instantiate (catPrefab);
		ClientScene.RegisterPrefab (spawnedCat); 
		spawnedCat.transform.SetParent (CatParent.transform);
		NetworkServer.SpawnWithClientAuthority (spawnedCat, connectionToClient);
		catlist.Add (spawnedCat.GetComponent<Player>());
		if (spawnedCat.transform.parent == this.transform && spawnedCat != null) {
			Debug.Log ("Cat Spawn Successful");
		}

		Debug.Log ("Running Systems");
	  RpcSpawn (CatParent);
	}
		
	//[ClientRpc] public void RpcSync(GameObject PlayerCat, GameObject syncParent) {
	//	if(NetworkServer.active) return;
      //  GameObject catThing = (GameObject)Instantiate(catPrefab);
	//	PlayerCat.transform.SetParent (syncParent.transform);
	//	catThing.transform.SetParent (syncParent.transform);
	//}

	[ClientRpc] public void RpcSpawn(GameObject, catObject, GameObject catParentSystem) {
		ServerSync (spawnedCat, catParentSystem);
		ClientSync (spawnedCat, catParentSystem);
	}
	[ServerCallback] public void ServerSync(GameObject playerCat, GameObject serverParent) {
		playerCat.transform.SetParent (serverParent.transform);
		ClientSync (playerCat, serverParent);
	}
	[ClientCallback] public void ClientSync(GameObject playerCat, GameObject clientParent) {
		playerCat.transform.SetParent (clientParent.transform);
		CmdSyncCallback (playerCat, clientParent);
	}
	[Command] public void CmdSyncCallback(GameObject playerCat, GameObject parentSync) {
		playerCat.transform.SetParent (parentSync.transform);
		Debug.Log ("Syncronization Successful");
	}


	//[Command] public void CmdTheSpawning() {
	//	//if (!isLocalPlayer) {
	//	//	return;
	//	//}
	//	for (int i = 0; i < NumberOfCats; i++) {
	//		GameObject playerCat = Instantiate(catPrefab, this.transform) as GameObject;
	//		playerCat.parentNetId
	//		ClientScene.RegisterPrefab(playerCat);
	///
	//		NetworkServer.SpawnWithClientAuthority (playerCat, connectionToClient);
	//		playerCat.transform.SetParent(this.transform);
	//		catlist.Add (playerCat.GetComponent<Player> ());
	//		//RpcSpawn ();
	//		//NetworkServer.SpawnWithClientAuthority(playerCat, connectionToClient);
	//	}
	//}

	//[ClientRpc] public void RpcSpawn() {
	//	if(NetworkServer.active) {
	//		return;
	//	}
	//	GameObject playerCat = (GameObject)Instantiate (catPrefab);
	//	ClientScene.RegisterPrefab (playerCat);
		//catlist.Add (playerCat.GetComponent<Player> ());
	//	NetworkServer.SpawnWithClientAuthority (playerCat, connectionToClient);
		//
	//	playerCat.transform.SetParent(this.transform);
	//}
    */
    public void SetPrepared(bool State) {
		CmdSetPrepared (State);
	}

    [Command] public void CmdSetPrepared(bool State) {
		RpcSetPrepared (State);
    }  
   
[ClientRpc] public void RpcSetPrepared(bool State) {
		this.Prepared = State;
}

public void FixedUpdate() {
	if(Input.GetMouseButtonDown(0) && DoingSomething == false)
	{
		GameObject mouseSelection = CheckForObject();
		if(mouseSelection == null)
		{
			Debug.Log("Nothing Detected By Mouse");
		}
		else
		{
			Debug.Log(mouseSelection.gameObject + "Was Detected");
			if (mouseSelection.gameObject.GetComponent<Player> ()) {
				Player theCat = mouseSelection.gameObject.GetComponent<Player> ();
				Debug.Log ("A Player Was Detected!");
				if (theCat.Team == Team) {
						FollowScript.GoToPosition = theCat.transform.position;
						FollowScript.selectedObject = theCat.transform;
						if (DoingPassive == true) {
							if (theCat.DonePassiveSelection == false) {
							currentlyControlling = theCat.CatId;
							controllingplayer = theCat;
							NeedingCatSelected = false;
							}
						}
						if (DoingActive == true) {
							if (theCat.DoneActiveSelection == false) {
								currentlyControlling = theCat.CatId;
								controllingplayer = theCat;
								NeedingCatSelected = false;
							}
						}
				}
			}
		}
	}
}

	// Update is called once per frame
	void Update () {

		Team = TurnSystem.players.IndexOf (this) + 1;
		if (TurnSystem.GameBegins == true) {
			Running = true;
		} else {
			Running = false;
		}
		if (Running != true) {
			return;
		}
		if (!isLocalPlayer) {
			return;
		}
		if (TurnSystem.players.Contains (this)) {
			playerRegistered = true;
		} else {
			Debug.Log ("Player: " + PlayerID.ToString () + " Is Not Registered!");
		}
		if (NeedingCatSelected == true) {
			ActionInitilized.WaitingForSelection = true;
		}
		if (NeedingCatSelected == false) {
			ActionInitilized.WaitingForSelection = false;
		}
	//if (catlist.Count > 0) {
	//		foreach (Player properCat in catlist) {
			   
	//		}
			//controllingplayer = catlist [currentlyControlling - 1];
			//if (catlist [currentlyControlling - 1].DonePassiveSelection == true && DoingPassive == true) {
			//	if (currentlyControlling < catlist.Count) {
			//		currentlyControlling += 1;
			//		SetupPassiveReset ();
			//	} else {
			//		StartCoroutine (PassiveWait ());
			//	
			//	}
	//		}

	//if (catlist.Count > 0 && DoingPassive == true) {
	//		finishedPassive = false;
	//		foreach (Player thePlayer in catlist) {
	//			if (thePlayer.DonePassiveSelection == false) {
	//				finishedPassive = false;
	//			}
	//		}
	///		if (finishedPassive == true) {
	//			StartCoroutine (PassiveWait ());
	//		}
	//controllingplayer = catlist [currentlyControlling - 1];
	//if (catlist [currentlyControlling - 1].DonePassiveSelection == true && DoingPassive == true) {
	//			if (currentlyControlling < catlist.Count) {
	///				currentlyControlling += 1;
	//				SetupPassiveReset ();
	///			} else {
	//				StartCoroutine (PassiveWait ());
	//			}
	//		}
		//}
		if (DoingPassive == true) {
		FinishedOfficalPassive = true;
			for (int y = 0; y < catlist.Count; y++) {
				if (catlist [y].DonePassiveSelection == false) {
					FinishedOfficalPassive = false;
					break;
				}
			}
		if (FinishedOfficalPassive == true && Lock == false) {
			Debug.Log ("Done Passive Action.");
				StartCoroutine (PassiveWait ());
		}
		if (FinishedOfficalPassive == true && Lock == true) {
				Debug.Log ("Lock Is Broken!");
				Lock = false;
			}
		}

		//if (DoingActive == true) {
		//	finishedActive = true;
		//	for (int z = 0; z < catlist.Count; z++) {
		//		if (catlist [z].DoneActiveSelection == false) {
		//			finishedActive = false;
		//			break;
		//		}
		//	}
		//	if (finishedActive == true) {
		//		Debug.Log ("Done Active Action.");
		//		StartCoroutine (ActiveWait ());
		//	}
		//	PlayersDoneActiveThings = true;
		//	foreach (Player DiscoverActive in catlist) {
		//		if (DiscoverActive.DoneActiveThing == false) {
		//			PlayersDoneActiveThings = false;
		//		}
		//	}
		//}
	//SetupActiveReset ();

		if (DoingActive == true) {
			FinishedOfficalActive = true;
			for (int z = 0; z < catlist.Count; z++) {
				if (catlist [z].DoneActiveSelection == false) {
					FinishedOfficalActive = false;
					break;
				}
			}
		//	if (catlist [z].DoneActiveSelection == true && catlist[z].DidAReset == false && NeedingCatSelected == false) {
		//			SetupActiveReset ();
		//			catlist [z].DidAReset = true;
		//		}
		//	}
		if (FinishedOfficalActive == true) {
				StartCoroutine (ActiveWait ());
			}
				PlayersDoneActiveThings = true;
				foreach (Player DiscoverActive in catlist) {
					if (DiscoverActive.DoneActiveThing == false) {
						PlayersDoneActiveThings = false;
					}
		 		}
		}
		//if (DoingActive == true) {
		//	controllingplayer = catlist [currentlyControlling - 1];
		//	if (catlist [currentlyControlling - 1].DoneActiveSelection == true && DoingActive == true) {
		//		if (currentlyControlling < catlist.Count) {
		//			currentlyControlling += 1;
		///			SetupActiveReset ();
		//		} else {
		//			StartCoroutine (ActiveWait ());
		//		}
		//		PlayersDoneActiveThings = true;
		//		foreach (Player DiscoverActive in catlist) {
		//			if (DiscoverActive.DoneActiveThing == false) {
		//				PlayersDoneActiveThings = false;
		//			}
		//		}
		//	}
		//}
	}

private GameObject CheckForObject() {
	Vector2 TouchPosition = mainCamera.ScreenToWorldPoint (Input.mousePosition);
	RaycastHit2D[] collidersAtPosition = Physics2D.RaycastAll (TouchPosition, Vector2.zero);

	SpriteRenderer closest = null;
	foreach (RaycastHit2D hit in collidersAtPosition) {
		if (closest == null) {
			closest = hit.collider.gameObject.GetComponent<SpriteRenderer> ();
			continue;
		}

		var hitSprite = hit.collider.gameObject.GetComponent<SpriteRenderer> ();

		if (hitSprite == null)
			continue;

		if (hitSprite.sortingOrder > closest.sortingOrder) {
			closest = hitSprite;
		}
	}

	return closest != null ? closest.gameObject : null;
}

[Command] public void CmdFinishPassive() {
		RpcFinishPassive ();
}


[ClientRpc] public void RpcFinishPassive() {
		finishedPassive = true;
}

[Command] public void CmdFinishActive() {
		RpcFinishActive ();
}

[ClientRpc] public void RpcFinishActive() {
		finishedActive = true;
}

public void RunReset() {
		for (int i = 0; i < catlist.Count; i++) {
			var ResetSystem = catlist [i].reset;
			catlist [i].StartCoroutine (ResetSystem);
		}
		currentlyControlling = 1;
		DoingActive = false;
		DoingPassive = true;
}

IEnumerator ResetThings() {
		yield return new WaitForSeconds (1.265f);
	for (int i = 0; i < catlist.Count; i++) {
		var ResetSystem = catlist [i].reset;
		catlist [i].StartCoroutine (ResetSystem);
	}
	currentlyControlling = 1;
	DoingActive = false;
	DoingPassive = true;
}


IEnumerator PassiveWait() {
		CmdFinishPassive ();
		//Debug.Log ("Things Running...");
	    yield return new WaitUntil(() => TurnSystem.PlayersCompletePassive == true);
		NeedingCatSelected = true;
	    AllPlayersDonePassive = true;
	    DisablePassive ();
		currentlyControlling = 1;
		DoingPassive = false;
		DoingActive = true;
}

IEnumerator ActiveWait() {
		CmdFinishActive ();
		yield return new WaitUntil (() => TurnSystem.PlayersCompleteActive == true);
		AllPlayersDoneActive = true;
	    //NeedingCatSelected = false;
		yield return new WaitUntil (() => PlayersDoneActiveThings == true);
		StartCoroutine (ResetThings ());
		//RunReset ();
	}
}




