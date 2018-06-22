
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilitys/Ability")]
public class Ability : ScriptableObject {
	public string Name = "Default";
	public Sprite Icon;
	public int AbilityID = 0;
	public bool isDefault = false;
	public string AbilityType = "Default";
	public Player player;
	//Things!
	public void PreformAbility() {
		if (AbilityID == 1) {
			player.SpeedIncrease = 1;
		}
		if (AbilityID == 2) {
			player.DamageReduction = 2;
		}
		Debug.Log ("Ability Initilization Succeded");

	}
}
