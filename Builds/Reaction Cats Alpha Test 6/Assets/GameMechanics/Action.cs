using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "New Action", menuName = "Actions/Action")]
public class Action : ScriptableObject {
    new public string name = "Default";
	public Player controlledplayer;
    public Sprite icon;
    public int phaseCount = 1;
    public int phaseToRunAt = 0;
    public EffectTypes[] effects;
    public bool isDefault = false;
    public string type = "Default";
}
public enum EffectTypes { Attack, Defend, Move, PointBlank, SkipActive, SkipPassive, Interact }
