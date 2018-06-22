using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PassiveSlot : NetworkBehaviour {
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
	//	if (!isLocalPlayer) {
	//		return;
	//	}

		//cPlayer = PlayerAssign.controllingplayer;
        if (action != null)
        {
            if (action.type != "Passive")
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
            TurnSystem.actStop = false;
            TurnSystem.passiveAction = false;
            //ActionInitilized.disablePassive = true;
            ActionInitilized.instance.passivePanel.SetActive(false);
            ActionInitilized.instance.activePanel.SetActive(false);
			action.controlledplayer = PlayerAssign.controllingplayer;
			action.UseAction();
        }
    }    
}
