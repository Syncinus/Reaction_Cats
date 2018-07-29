using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBS
{
	namespace SpriteLighting
	{

public class CameraRenderDepthTexture : MonoBehaviour {
			[SerializeField] protected new Camera camera;


			// Use this for initialization
			void Start ()
			{
				SetDepthMode();
			}


			private void Reset ()
			{
				if (camera == null)
				{
					camera = GetComponent<Camera>();

					if (camera == null)
					{
						camera = Camera.main;
					}
				}
			}

			[ContextMenu("Set depth mode")]
			public void SetDepthMode ()
			{
				if (camera == null) return;

				camera.depthTextureMode = DepthTextureMode.Depth;
			}
}
	}
}
