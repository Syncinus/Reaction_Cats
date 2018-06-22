using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragSystem : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler {
	public CatEditSlot editorSlot;

	public void OnDrag(PointerEventData eventData) {
		this.transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData) {
		if (Vector2.Distance (this.transform.position, editorSlot.transform.position) > 5) {
			this.transform.localPosition = Vector2.zero;
		}
	}

	public void OnDrop(PointerEventData eventData) {
		if (Vector2.Distance (this.transform.position, editorSlot.transform.position) < 5) {
			editorSlot.catToEdit = this.transform.parent.parent.GetComponent<CatHoldSlot>().currentCat;
			this.transform.parent.parent.GetComponent<CatHoldSlot> ().currentCat = editorSlot.catToEdit;
			Debug.Log ("Cats Swapped!");
			this.transform.localPosition = Vector2.zero;
		}
	}

}
