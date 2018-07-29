using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableAction : MonoBehaviour {

    public Action actionAttached;

	public void SelectAction()
    {
        Debug.Log("Selected Action: " + actionAttached.name);
        this.transform.GetComponentInParent<ActionInitilized>().SelectAction(actionAttached);
    }
}
