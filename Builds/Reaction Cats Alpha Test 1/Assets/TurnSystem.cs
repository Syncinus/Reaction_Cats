using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;

public class TurnSystem : NetworkBehaviour
{
	#region Singleton

	public static TurnSystem instance;

	void Awake ()
	{
		if (instance != null)
		{
			//Debug.LogWarning("More Than One Instance Of A Single Hex found!");
			return;
		}

		instance = this;
	}

	#endregion

	//public Vector3 yesItWorks;
    public static bool activeAction = false;
    public static bool passiveAction = false;
	//[SyncVar] public List<Transform> OccupiedHexes = new List<Transform> ();
    public IEnumerator derp;
    public IEnumerator herp;
    public static bool Locked = false;
    public static bool actLocked = false;
    public bool activeTurn = false;
    public bool passiveTurn = false;
    public static bool actStop = true;
    public static bool pasStop = false;
	public static int CatRunning;
	public static bool PlayersCompletePassive = false;
	public bool PCP;
	public static bool PlayersCompleteActive = false;
	public bool PCA;
	public static bool GameBegins = false;
	//public bool SetP1Finish = false;
	//public bool SetP2Finish = false;
	public static List<Player> cats = new List<Player> ();
	public static List<PlayerAssign> players = new List<PlayerAssign> ();
	public GameObject hexgrid;
	public int playerlist;

	public void Start() {
		hexgrid.SetActive (true);
	}

    public void LateUpdate()
    {
        if (passiveAction == false && Locked == false && pasStop == false)
        {
            derp = Derp();
           StartCoroutine(derp);
        }

		if (activeAction == false && actLocked == false && actStop == false && Player.donePassive == true)
        {
			//if (cats.Count > 1) {
			//	if (P1PassiveFinish == true && P2PassiveFinish == true) {
			//		herp = Herp ();
			//		StartCoroutine (herp);
			//	}
			//} else {
				herp = Herp ();
				StartCoroutine (herp);
			//}
        }
    }

	public void Update() {
		PCA = PlayersCompleteActive;
		PCP = PlayersCompletePassive;
		playerlist = players.Count ();

		for (var x = 0; x < cats.Count; x++) {
			var SpecialX = x + 1;
		//	cats [x].CatId = SpecialX;
			cats [x].transform.name = "Cat: " + SpecialX + ", Name: " + cats[x].CatName;
		}

		GameBegins = true;
		for (var q = 0; q < players.Count; q++) {
			var SpecialQ = q + 1;
			players [q].PlayerID = SpecialQ;
		    players [q].transform.name = "Player: " + SpecialQ;
			if (players [q].Prepared == false) {
				GameBegins = false;
			}
		}


			PlayersCompletePassive = true;
			for (var p = 0; p < players.Count; p++) {
				PlayerAssign ThingPlayer = players [p];
				if (ThingPlayer.finishedPassive != true) {
					PlayersCompletePassive = false;
				}
			}

			PlayersCompleteActive = true;
			for (var p = 0; p < players.Count; p++) {
				PlayerAssign ThingPlayer = players [p];
				if (ThingPlayer.finishedActive != true) {
					PlayersCompleteActive = false;
				}
			}
	}
    public IEnumerator Derp()
    {
        yield return new WaitForSeconds(1.0f);
        passiveAction = true;
        Locked = true;
    }
    public IEnumerator Herp()
    {
        yield return new WaitForSeconds(1.0f);
        activeAction = true;
        actLocked = true;
    }
}