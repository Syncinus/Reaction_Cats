using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningPoolGenerator : MonoBehaviour {

	public GameObject boltPrefab;
	public int BoltCount;
	public int BoltsPerFrame = 5;
	public GameObject enemyObject;

	List<GameObject> activeBoltsObj;
	List<GameObject> inactiveBoltsObj;

	int MaxBolts = 1000;

	int clicks = 0;
	Vector2 pos1, pos2;

	public void Start() {
		activeBoltsObj = new List<GameObject> ();
		inactiveBoltsObj = new List<GameObject> ();

		GameObject p = GameObject.Find ("LightningPoolHandler");

		for (int i = 0; i < MaxBolts; i++) {
			GameObject bolt = (GameObject)Instantiate (boltPrefab);
			bolt.transform.SetParent (p.transform);
			bolt.GetComponent<LightningBolt> ().Initilize (25);
			bolt.SetActive (false);
			inactiveBoltsObj.Add (bolt);
		}
	}

	public void Update() {
		GameObject boltObj;
		LightningBolt boltComponent;

		int activeLineCount = activeBoltsObj.Count;

		for (int i = activeLineCount - 1; i >= 0; i--) {
			boltObj = activeBoltsObj [i];
			boltComponent = boltObj.GetComponent<LightningBolt> ();

			if (boltComponent.IsComplete) {
				boltComponent.DeactivateSegments ();
				boltObj.SetActive (false);
				activeBoltsObj.RemoveAt (i);
				inactiveBoltsObj.Add (boltObj);
			}
		}

		if (Input.GetMouseButtonDown(0)) {
			if (clicks == 0) {
				Vector3 temp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				pos1 = new Vector2(temp.x, temp.y);
			}
			if (clicks == 1) {
				Vector3 temp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				//pos2 = new Vector2 (temp.x, temp.y);
				pos2 = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 1f);
				pos2 = enemyObject.transform.TransformPoint (pos2 * 0.5f);
				//GameObject branchObj = (GameObject)GameObject.Instantiate (branchPrefab);
				//BranchLightning branchComponent = branchObj.GetComponent<BranchLightning> ();
				//branchComponent.Initilize (pos1, pos2, boltPrefab);
				//branchesObj.Add (branchObj);
			 // for (int i = 0; i < BoltCount; i++) {
				//CreatePooledBolt (pos1, pos2, 0.5f);
			//  }
					StartCoroutine(ElectronicDestruction(pos1, pos2, 0.15f));
			}
			clicks++;

			if (clicks > 1) clicks = 0;
		}

		for (int i = 0; i < activeBoltsObj.Count; i++) {
			activeBoltsObj [i].GetComponent<LightningBolt> ().UpdateBolt ();
			activeBoltsObj [i].GetComponent<LightningBolt> ().Draw ();
		}
	}

	public void CreatePooledBolt(Vector2 source, Vector2 dest, float thickness) {
	 //for (int i = 0; i < BoltCount; i++) {
		if (inactiveBoltsObj.Count > 0) {
			GameObject boltObj = inactiveBoltsObj [inactiveBoltsObj.Count - 1];
			boltObj.SetActive (true);
			activeBoltsObj.Add (boltObj);
			inactiveBoltsObj.RemoveAt (inactiveBoltsObj.Count - 1);
			LightningBolt boltComponent = boltObj.GetComponent<LightningBolt> ();
			Color color;
			color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range(0.4f, 0.6f));
			boltComponent.Tint = color;
			boltComponent.ActivateBolt (source, dest, color, thickness);
		}
	 //}
	}

	IEnumerator ElectronicDestruction(Vector2 source, Vector2 dest, float thickness) {
		int Points = BoltCount;
		for (int i = 0; i < Points; i++) {
			for (int z = 0; z < BoltsPerFrame; z++) {
				dest = new Vector2 (Random.Range (-0.85f, 0.85f), Random.Range (-0.85f, 0.85f));
				dest = enemyObject.transform.TransformPoint (dest * 0.5f);
				CreatePooledBolt (source, dest, thickness);
			}
			yield return new WaitForEndOfFrame ();
		}
		yield return null;
	}



}
