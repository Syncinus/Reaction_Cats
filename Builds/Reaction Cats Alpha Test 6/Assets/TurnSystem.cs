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

	public static bool GameBegins = false;
	public static List<Player> cats = new List<Player> ();
	public static List<PlayerAssign> players = new List<PlayerAssign> ();
	public GameObject hexgrid;
	public int playerlist;


    public List<ActionInfo> catActions;
    public static int maxPhases = 10;





        
	public void Start() {
		hexgrid.SetActive (true);
	}
         
    public void LateUpdate()
    {
       
    }

	public void Update() {
		
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

	}
}