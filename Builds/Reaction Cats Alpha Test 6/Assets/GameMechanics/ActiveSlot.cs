using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ActiveSlot : NetworkBehaviour {
    public Image icon;
    public Action action;
	public static Player cPlayer;

    public void NewAction(Action newAction)
    {
        action = newAction;
        icon.sprite = newAction.icon;
        icon.enabled = true;
    }

    public void Start()
    {
        if (action != null)
        {
            icon.sprite = action.icon;
            icon.enabled = true;
        }
    }
    public void Update()
    {

		//if (!isLocalPlayer) {
		//	return;
		//}
		//cPlayer = PlayerAssign.controllingplayer;
        if (action != null)
        {
            if (action.type != "Active")
            {
                action = null;
                icon.sprite = null;
                icon.enabled = false;
                Debug.LogWarning("That Shouldnt Be There!");
            }
        }
    }
    public void Use()
    {
        if (action != null)
        {

			action.controlledplayer = PlayerAssign.controllingplayer;
			//action.UseAction();
            //ActionInitilized.stopTheActive = true;
            //ActionInitilized.instance.activePanel.SetActive(false);
            //ActionInitilized.instance.passivePanel.SetActive(false);
           // TurnSystem.instance.actionsDone.Add(action);    
        }
    }
}
