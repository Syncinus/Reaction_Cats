using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LightningBolt : MonoBehaviour {

	public List<GameObject> ActiveLineObj;
	public List<GameObject> InactiveLineObj;

	public GameObject LinePrefab;

	public float Alpha { get; set; }
	public float FadeOutRate { get; set; }
	public Color Tint { get; set; }
	public Vector2 Start { get { return ActiveLineObj [0].GetComponent<Lightning> ().pointA; } }
	public Vector2 End { get {return ActiveLineObj [ActiveLineObj.Count - 1].GetComponent<Lightning>().pointB; } }
	public bool IsComplete { get { return Alpha <= 0; } }

	public void Initilize(int MaxSegs) {
		ActiveLineObj = new List<GameObject> ();
		InactiveLineObj = new List<GameObject> ();

		for (int i = 0; i < MaxSegs; i++) {
			GameObject line = (GameObject)GameObject.Instantiate (LinePrefab);

			line.transform.SetParent (this.transform);

			line.SetActive (false);

			InactiveLineObj.Add (line);
		}
	}

	public void ActivateBolt(Vector2 source, Vector2 dest, Color color, float thickness) {
		Tint = color;
		Alpha = 1.5f;
		FadeOutRate = 0.03f;

		if (Vector2.Distance (dest, source) <= 0) {
			Vector2 adjust = UnityEngine.Random.insideUnitCircle;
			if (adjust.magnitude <= 0) adjust.x += 0.1f;
			dest += adjust;
		}

		Vector2 slope = dest - source;
		Vector2 normal = (new Vector2 (slope.y, -slope.x)).normalized;

		float distance = slope.magnitude;

		List<float> positions = new List<float> ();
		positions.Add (0);

		for (int i = 0; i < distance / 2; i++) {
			positions.Add (UnityEngine.Random.Range (0.25f, 0.75f));
		}

		positions.Sort ();

		const float Sway = 80;
		const float Jagedness = 1 / Sway;

		float Spread = 0.35f;

		Vector2 prevPoint = source;

		float prevDisplacement = 0;

		for (int i = 1; i < positions.Count; i++) {
			int inactiveCount = InactiveLineObj.Count;
			if (inactiveCount <= 0) break;

			float pos = positions[i];
			float scale = (distance * Jagedness) * (pos - positions[i - 1]);
			float envelope = pos > 0.95f ? 20 * (1 - pos) : Spread;
			float Displacement = UnityEngine.Random.Range(-Sway, Sway);
			Displacement -= (Displacement - prevDisplacement) * (1 - scale);
			Displacement *= envelope;

			Vector2 point = source + (pos * slope) + (Displacement * normal);

			activateLine (prevPoint, point, color, thickness);
			prevPoint = point;
			prevDisplacement = Displacement;
		}

		activateLine (prevPoint, dest, color, thickness);
	}

	public void DeactivateSegments() {
		for (int i = ActiveLineObj.Count - 1; i >= 0; i--) {
			GameObject line = ActiveLineObj [i];
			line.SetActive (false);
			ActiveLineObj.RemoveAt (i);
			InactiveLineObj.Add (line);
		}
	}

	public void activateLine(Vector2 A, Vector2 B, Color color, float thickness) {
		int inactiveCount = InactiveLineObj.Count;
		if (inactiveCount <= 0) return;
		GameObject line = InactiveLineObj [inactiveCount - 1];
		line.SetActive (true);
		Lightning lineComponent = line.GetComponent<Lightning> ();
		lineComponent.SetColor (color);
		lineComponent.pointA = A;
		lineComponent.pointB = B;
		lineComponent.lightningThickness = thickness;
		InactiveLineObj.RemoveAt (inactiveCount - 1);
		ActiveLineObj.Add (line);
	}

	public void Draw() {
		if (Alpha <= 0) return;

		foreach (GameObject obj in ActiveLineObj) {
			Lightning lineComponent = obj.GetComponent<Lightning> ();
			lineComponent.SetColor (Tint * (Alpha * 0.6f));
			lineComponent.Draw ();
		}
	}

	public void UpdateBolt() {
		Alpha -= FadeOutRate;
	}

	public Vector2 GetPoint(float position) {
		Vector2 start = Start;
		float length = Vector2.Distance (start, End);
		Vector2 dir = (End - start) / length;
		position *= length;
		Lightning line = ActiveLineObj.Find (x => Vector2.Dot (x.GetComponent<Lightning> ().pointB - start, dir) >= position).GetComponent<Lightning> ();
		float lineStartPos = Vector2.Dot (line.pointA - start, dir);
		float lineEndPos = Vector2.Dot (line.pointB - start, dir);
		float linePos = (position - lineStartPos) / (lineEndPos - lineStartPos);

		return Vector2.Lerp (line.pointA, line.pointB, linePos);
	}
}
