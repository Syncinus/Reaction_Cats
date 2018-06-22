using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attacks/Attack")]
public class Attack : ScriptableObject {
	public int Range;
	public int DamageIncrease;
	public int EnemyDefenseReduction;
	public string AttackName;
}
