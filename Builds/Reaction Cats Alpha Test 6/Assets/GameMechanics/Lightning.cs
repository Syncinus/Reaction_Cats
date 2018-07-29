using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {

	public Vector2 pointA;

	public Vector2 pointB;

	public float lightningThickness;

	public GameObject StartCapChild, LineChild, EndCapChild;

	public Lightning(Vector2 PointA, Vector2 PointB, float LightningThickness) {
		pointA = PointA;
		pointB = PointB;
		lightningThickness = LightningThickness;
	}

	public void SetColor(Color color) {
		StartCapChild.GetComponent<SpriteRenderer> ().color = color;
		StartCapChild.GetComponent<Light> ().color = color;
		LineChild.GetComponent<SpriteRenderer> ().color = color;
		LineChild.GetComponent<Light>().color = color;
		EndCapChild.GetComponent<SpriteRenderer> ().color = color;
		EndCapChild.GetComponent<Light> ().color = color;
//		this.GetComponent<Light> ().color = color;
	}

	public void Draw() {
		Vector2 difference = pointB - pointA;
		float Rotation = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;

		LineChild.transform.localScale = new Vector3 (100 * (difference.magnitude / LineChild.GetComponent<SpriteRenderer> ().sprite.rect.width), lightningThickness, LineChild.transform.localScale.z);
		StartCapChild.transform.localScale = new Vector3 (StartCapChild.transform.localScale.x, lightningThickness, StartCapChild.transform.localScale.z);
		EndCapChild.transform.localScale = new Vector3 (EndCapChild.transform.localScale.x, lightningThickness, EndCapChild.transform.localScale.z);

		LineChild.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Rotation));
		StartCapChild.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Rotation));
		EndCapChild.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Rotation + 180));

		LineChild.transform.position = new Vector3 (pointA.x, pointA.y, LineChild.transform.position.z);
		StartCapChild.transform.position = new Vector3 (pointA.x, pointA.y, StartCapChild.transform.position.z);
		EndCapChild.transform.position = new Vector3 (pointA.x, pointA.y, EndCapChild.transform.position.z);

		Rotation *= Mathf.Deg2Rad;

		float LineChildWorldAdjust = LineChild.transform.localScale.x * LineChild.GetComponent<SpriteRenderer> ().sprite.rect.width / 2f;
		float StartCapChildWorldAdjust = StartCapChild.transform.localScale.x * StartCapChild.GetComponent<SpriteRenderer> ().sprite.rect.width / 2f;
		float EndCapChildWorldAdjust = EndCapChild.transform.localScale.x * EndCapChild.GetComponent<SpriteRenderer> ().sprite.rect.width / 2f;

		LineChild.transform.position += new Vector3 (0.01f * Mathf.Cos (Rotation) * LineChildWorldAdjust, 0.01f * Mathf.Sin (Rotation) * LineChildWorldAdjust, 0);
		StartCapChild.transform.position -= new Vector3 (0.01f * Mathf.Cos (Rotation) * StartCapChildWorldAdjust, 0.01f * Mathf.Sin (Rotation) * StartCapChildWorldAdjust, 0);
		EndCapChild.transform.position += new Vector3 (0.01f * Mathf.Cos (Rotation) * LineChildWorldAdjust * 2, 0.01f * Mathf.Sin (Rotation) * LineChildWorldAdjust * 2, 0);
		EndCapChild.transform.position += new Vector3 (0.01f * Mathf.Cos (Rotation) * EndCapChildWorldAdjust, 0.01f * Mathf.Sin (Rotation) * EndCapChildWorldAdjust, 0);
	}
}
