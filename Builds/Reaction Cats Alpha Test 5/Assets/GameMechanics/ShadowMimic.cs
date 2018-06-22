using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBS
{
	namespace SpriteLighting
	{

[RequireComponent(typeof(SpriteRenderer))]
public class ShadowMimic : MonoBehaviour {
			/// <summary>
			/// The material to the copy spriteRenderer.
			/// </summary>
			[SerializeField] protected Material mimicMaterial;
			[SerializeField] protected int layerChange;
			[SerializeField] protected SpriteRenderer originalRenderer;
			protected SpriteRenderer mimicRenderer;

			/// <summary>
			/// Mark as true if the sprite won't change over time, so the copy will not
			/// be updated.
			/// </summary>


			// Use this for initialization
			void Start ()
			{
				if (originalRenderer == null)
				{
					// Create a copy of the sprite renderer in a new child gameObject
					originalRenderer = GetComponent<SpriteRenderer>();
				}

				GameObject copyGameObject = new GameObject();

				Transform mimicTransform = copyGameObject.transform;
				mimicTransform.SetParent(this.transform);
				mimicTransform.localScale = Vector3.one;
				mimicTransform.localRotation = Quaternion.identity;
				mimicTransform.position = originalRenderer.transform.position;

				mimicRenderer = copyGameObject.AddComponent<SpriteRenderer>();
				mimicRenderer.material = mimicMaterial;
				mimicRenderer.sprite = originalRenderer.sprite;
				mimicRenderer.sortingLayerID = originalRenderer.sortingLayerID;
				mimicRenderer.sortingOrder = originalRenderer.sortingOrder + layerChange;
				mimicTransform.name = "Mimic";
			}

			void Reset ()
			{
				if (originalRenderer == null)
				{
					originalRenderer = GetComponent<SpriteRenderer>();
				}
			}

			public void FixedUpdate() {
				mimicRenderer.sprite = originalRenderer.sprite;
			}

			void LateUpdate ()
			{
				mimicRenderer.sprite = originalRenderer.sprite;
			}


}
	}
}
