using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchLightning : MonoBehaviour {
	List<GameObject> boltsObj = new List<GameObject>();
	public bool IsComplete { get { return boltsObj.Count == 0; } }
	public Vector2 Start { get; private set; }
	public Vector2 End { get; private set; }
	static Random rand = new Random();

	public void Initilize(Vector2 start, Vector2 end, GameObject boltPrefab) {
		Start = start;
		End = end;
		GameObject mainBoltObj = (GameObject)GameObject.Instantiate (boltPrefab);
		LightningBolt mainBoltComponent = mainBoltObj.GetComponent<LightningBolt> ();
		mainBoltComponent.Initilize (5);
		mainBoltComponent.ActivateBolt (start, end, Color.white, 1f);
		boltsObj.Add (mainBoltObj);
		int numBranches = Random.Range (3, 6);
		Vector2 diff = end - start;
		List<float> branchPoints = new List<float> ();
		for (int i = 0; i < numBranches; i++) branchPoints.Add (Random.value);
		branchPoints.Sort ();
		for (int i = 0; i < branchPoints.Count; i++) {
			Vector2 boltStart = mainBoltComponent.GetPoint (branchPoints [i]);
			Quaternion rot = Quaternion.AngleAxis(30 * ((i & 1) == 0 ? 1 : -1), new Vector3(0, 0, 1));
			Vector2 adjust = rot * (Random.Range (0.5f, 0.75f) * diff * (1 - branchPoints [i]));
			Vector2 boltEnd = adjust + boltStart;
			GameObject boltObj = (GameObject)GameObject.Instantiate (boltPrefab);
			LightningBolt boltComponent = boltObj.GetComponent<LightningBolt> ();
			boltObj.transform.SetParent (this.transform);
			boltComponent.Initilize(5);
			Color color;
			color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), 1);
			boltComponent.ActivateBolt (boltStart, boltEnd, color, 1f);
			boltsObj.Add (boltObj);
		}
	}

	public void UpdateBranch() {
		for (int i = boltsObj.Count - 1; i >= 0; i--) {
			GameObject boltObj = boltsObj [i];
			LightningBolt boltComp = boltObj.GetComponent<LightningBolt> ();
			boltComp.UpdateBolt ();
			if (boltComp.IsComplete) {
				boltsObj.RemoveAt (i);
				Destroy (boltObj);
			}
		}
	}

	public void Draw() {
		foreach (GameObject boltObj in boltsObj) {
			boltObj.GetComponent<LightningBolt> ().Draw ();
		}
	}
}
