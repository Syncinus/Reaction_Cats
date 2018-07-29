using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionInfo {
    public List<Action> actions = new List<Action>();
    public int actionPhaseAmount;
    public Player cat;
    public List<Hex> hexesToMoveThrough = new List<Hex>();
}
