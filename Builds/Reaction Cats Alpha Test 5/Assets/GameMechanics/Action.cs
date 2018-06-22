using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "New Action", menuName = "Actions/Action")]
public class Action : ScriptableObject {
    new public string name = "Default";
	public Player controlledplayer;
    public Sprite icon;
    public EffectTypes effect, effect2, effect3;
    public bool isDefault = false;
    public string type = "Default";

    public virtual void UseAction()
    {
        //Action Preformace
        if (effect == EffectTypes.Move || effect2 == EffectTypes.Move || effect3 == EffectTypes.Move)
        {
            Debug.Log("Action Effect: " + "MOVING CAT...");
            Player.donePassive = false;
			TurnSystem.pasStop = true;
			TurnSystem.passiveAction = false;
			controlledplayer.MOVINGCAT = true;
			ActionInitilized.disablePassive = true;
			ActionInitilized.instance.passivePanel.SetActive (false);
        }
        if (effect == EffectTypes.Attack || effect2 == EffectTypes.Attack || effect3 == EffectTypes.Attack)
        {
			//if (controlledplayer.AbleToAttack == true) {
				Debug.Log ("Action Effect: " + "CAT ATTACKING...");
				Player.doneActive = false;
				TurnSystem.actStop = true;
				TurnSystem.activeAction = false;
				controlledplayer.CATATTACKING = true;
				ActionInitilized.disableActive = true;
			//} else {
			//	Player.doneActive = false;
			//	TurnSystem.actStop = false;
			//	controlledplayer.CATATTACKING = false;
			//	ActionInitilized.disableActive = false;
		//	}
        }
		if (effect == EffectTypes.SkipPassive || effect2 == EffectTypes.SkipPassive || effect3 == EffectTypes.SkipPassive)
		{
			Debug.Log("Action Effect: " + "SKIPPING 'PASSIVE' ACTION...");
			Player.donePassive = false;
			TurnSystem.pasStop = true;
			TurnSystem.passiveAction = false;
			controlledplayer.CATSKIPPINGPASSIVE = true;
			ActionInitilized.disablePassive = true;
		}
		if (effect == EffectTypes.SkipActive || effect2 == EffectTypes.SkipActive || effect3 == EffectTypes.SkipActive)
		{
			Debug.Log("Action Effect: " + "SKIPPING 'ACTIVE' ACTION...");
			Player.doneActive = false;
			TurnSystem.actStop = true;
			TurnSystem.activeAction = false;
			controlledplayer.CATSKIPPINGACTIVE = true;
			ActionInitilized.disableActive = true;
		}
		if (effect == EffectTypes.Interact || effect2 == EffectTypes.Interact || effect3 == EffectTypes.Interact) {
			Debug.Log ("Action Effect: " + "CAT INTERACTING");
			Player.doneActive = false;
			TurnSystem.actStop = true;
			TurnSystem.activeAction = false;
			controlledplayer.CATINTERACTING = true;
			ActionInitilized.disableActive = true;
		}
			
        Debug.Log("Using Action: " + name + "...");
    }
	public virtual void Update ()
	{
		Debug.Log ("DERP!");
	}
    //Use Passive Or Active Type
}
public enum EffectTypes { Attack, Defend, Move, PointBlank, SkipActive, SkipPassive, Interact }
