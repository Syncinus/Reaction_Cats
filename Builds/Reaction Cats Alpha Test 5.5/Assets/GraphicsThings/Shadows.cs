using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour {

	public Vector3 offset = new Vector3(-0.1f, -0.1f);
	public Material mat;

	GameObject shadow;

	public void Start() {
		shadow = new GameObject ("Shadow");
		shadow.transform.parent = this.transform;

		shadow.transform.localPosition = offset;
		shadow.transform.localRotation = Quaternion.identity;

		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		SpriteRenderer sr = shadow.AddComponent<SpriteRenderer> ();
		sr.sprite = renderer.sprite;
		sr.material = mat;

		sr.sortingLayerName = renderer.sortingLayerName;
		sr.sortingOrder = sr.sortingOrder - 1;
	}

	public void LateUpdate() {
		shadow.transform.localPosition = offset;
	}
}
