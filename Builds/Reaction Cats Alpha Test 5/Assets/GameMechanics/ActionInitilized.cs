using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActionInitilized : MonoBehaviour {
    public GameObject passivePanel;
    public GameObject activePanel;
	public static bool disablePassive = false;
	public static bool disableActive = false;
	public static bool WaitingForSelection = false;
    #region Singleton

    public static ActionInitilized instance;

    void Awake()
    {
        if (instance != null)
        {
            //Debug.LogWarning("More Than One Instance Of A Single Hex found!");
            return;
        }

        instance = this;
    }

    #endregion
    public void Update()
    {
		if (TurnSystem.cats.Count != 0) {
			if (TurnSystem.passiveAction == true && disablePassive == false) {
				passivePanel.SetActive (true);
			} else {
				passivePanel.SetActive (false); //Should Be False //It IS
			}
			if (TurnSystem.activeAction == true && disableActive == false) {
				activePanel.SetActive (true);
			} else {
				activePanel.SetActive (false); //Should Be False //It IS
			}
		}
		if (WaitingForSelection == true) {
			activePanel.SetActive (false);
			passivePanel.SetActive (false);
		}
    }
}
