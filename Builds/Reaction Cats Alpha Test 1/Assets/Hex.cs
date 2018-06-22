using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class Hex : NetworkBehaviour {
	public string ShownUpdate = "Update 3";
    public float speed = 10.0f; //how fast it shakes
    public float amount = 100.0f; //how much it shakes
	public Sprite overHexSprite;
	public Sprite defaultSprite;
	public Sprite selectedForMoveSprite;
	public Sprite selectedForAttackSprite;
	public Sprite attackedSprite;
	public Sprite movedThroughSprite;
    public static Transform hexq;
	public static Transform oldhexq;
	public static int hexrowid;
	public static int hexnumid;
    public static int attackrowid;
    public static int attacknumid;
	public NetworkIdentity ExampleIdentify; // this one that i mention on down topic.
	public int rowid;
	public int numberid;
	public int datarowid;
	public int datanumid;
    public static Hex attackhex;
	public SpriteRenderer rend;
	public static Hex thishex;
    public Transform hextransform;
	public Hex thisishex;
	public bool seter = false;
	public bool forceseter = false;
	public static bool lightup;
	public GameObject player;
	public static Player playerscript;
    public bool overhex;
    public GameObject GameObjectToShake;
    public static GameObject hexshaker;
    bool shaking = false;
	[SyncVar]
	public string OnHex = "";
	[SyncVar]
	public Transform objectOnhex;



	IEnumerator shakeGameObjectCOR(GameObject objectToShake, float totalShakeDuration, float decreasePoint, bool objectIs2D = false)
    {
        if (decreasePoint >= totalShakeDuration)
        {
            Debug.LogError("decreasePoint must be less than totalShakeDuration...Exiting");
            yield break; //Exit!
        }

        //Get Original Pos and rot
        Transform objTransform = objectToShake.transform;
        Vector3 defaultPos = objTransform.position;
        Quaternion defaultRot = objTransform.rotation;

        float counter = 0f;

        //Shake Speed
        //const float speed = 0.007f;
		const float speed = 0.01f;
		//const float speed = 10.0f;


        //Angle Rotation(Optional)
		const float angleRot = 6;

        //Do the actual shaking
        while (counter < totalShakeDuration)
        {
            counter += Time.deltaTime;
            float decreaseSpeed = speed;
            float decreaseAngle = angleRot;

            //Shake GameObject
            if (objectIs2D)
            {
                //Don't Translate the Z Axis if 2D Object
                Vector3 tempPos = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                tempPos.z = defaultPos.z;
                objTransform.position = tempPos;

                //Only Rotate the Z axis if 2D
                objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-angleRot, angleRot), new Vector3(0f, 0f, 1f));
            }
            else
            {
                objTransform.position = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-angleRot, angleRot), new Vector3(1f, 1f, 1f));
            }
            yield return null;


            //Check if we have reached the decreasePoint then start decreasing decreaseSpeed value
            if (counter >= decreasePoint)
            {

                //Reset counter to 0 
                counter = 0f;
                while (counter <= decreasePoint)
                {
                    counter += Time.deltaTime;
                    decreaseSpeed = Mathf.Lerp(speed, 0, counter / decreasePoint);
                    decreaseAngle = Mathf.Lerp(angleRot, 0, counter / decreasePoint);

                    //Shake GameObject
                    if (objectIs2D)
                    {
                        //Don't Translate the Z Axis if 2D Object
                        Vector3 tempPos = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                        tempPos.z = defaultPos.z;
                        objTransform.position = tempPos;

                        //Only Rotate the Z axis if 2D
                        objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-decreaseAngle, decreaseAngle), new Vector3(0f, 0f, 1f));
                    }
                    else
                    {
                        objTransform.position = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                        objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-decreaseAngle, decreaseAngle), new Vector3(1f, 1f, 1f));
                    }
                    yield return null;
                }

                //Break from the outer loop
                break;
            }
        }
        objTransform.position = defaultPos; //Reset to original postion
        objTransform.rotation = defaultRot;//Reset to original rotation

        shaking = false; //So that we can call this function next time
    }

	//void OnChangeOnHex (string NewOnHex)
	//{
	//	OnHex = NewOnHex;
	//}

	//void OnChangeObjectOnHex(Transform NewObjectOnHex) {
	//	objectOnhex = NewObjectOnHex;
	//}

	//[ClientRpc]
	//void ChangeThings() {
	//	OnHex = OnHex;
	//	objectOnhex = objectOnhex;
	//}

	public override void OnStartServer() {
		ExampleIdentify = this.gameObject.GetComponent <NetworkIdentity>(); // it will identify as uniq on network and will be easy to look up to it.
	}

	void shakeGameObject(GameObject objectToShake, float shakeDuration, float decreasePoint, bool objectIs2D = false)
    {
        if (shaking)
        {
            return;
        }
        shaking = true;
		StartCoroutine(shakeGameObjectCOR(objectToShake, shakeDuration, decreasePoint, objectIs2D));
    }

    #region Singleton

    public static Hex instance;

	void Awake ()
	{

		GetComponent<Renderer>().shadowCastingMode =  UnityEngine.Rendering.ShadowCastingMode.On;
		GetComponent<Renderer>().receiveShadows = true;
		GetComponent<SpriteRenderer>().shadowCastingMode =  UnityEngine.Rendering.ShadowCastingMode.On;
		GetComponent<SpriteRenderer>().receiveShadows = true;

		if (instance != null)
		{
			//Debug.LogWarning("More Than One Instance Of A Single Hex found!");
			return;
		}

		instance = this;
	}

	#endregion
    public void WobbleLikeTheWeebles()
    {
     shakeGameObject(GameObjectToShake, 0.5f, 0.26f, true);
	 //shakeGameObject(GameObjectToShake, 1000f, 0.5f, true);
    }

    void Start() {
		rend = GetComponent<SpriteRenderer> ();
		player = GameObject.Find ("Player 1");
		defaultSprite = rend.sprite;
		if (player != null) {
			playerscript = player.GetComponent<Player> ();
		}
        //shakeGameObject(GameObjectToShake, 5, 3f, false);
    }
	void OnMouseEnter() {
		if (forceseter == false) {
			//rend.color = new Color32 (255, 0, 0, 255);	
			rend.sprite = overHexSprite;
			seter = true;
            overhex = true;
		}
		if (playerscript != null) {
			if (playerscript.CATATTACKING == true && forceseter == false) {
				//rend.color = new Color32 (255, 69, 242, 255);	
				rend.sprite = selectedForAttackSprite;
				seter = true;
				overhex = true;
			}
		}
	}
    void OnMouseExit() {
		if (!forceseter == true) {
			//rend.color = new Color32 (255, 255, 255, 255);
			rend.sprite = defaultSprite;
			seter = false;
            overhex = false;
		}
	}

	public void OnMouseDown() {
		
		if (playerscript != null) {
			hexq = this.transform;
			if (playerscript.banlist.Contains (thisishex)) {
				playerscript.banlist.Remove (thisishex);
			}
			if (playerscript.MOVINGCAT == true || playerscript.CATJUMPING == true) {
				hexrowid = rowid;			
				hexnumid = numberid;
			}
			if (playerscript.CATATTACKING == true) {
				attackrowid = rowid;
				attacknumid = numberid;
				playerscript.attackhexnum = playerscript.currenthexnum;
				playerscript.attackhexrow = playerscript.currenthexrow;
				playerscript.attackBlocker = false;
			}
			attackhex = thisishex;
			if (playerscript.currenthexnum == numberid && playerscript.currenthexrow == rowid) {
				seter = false;
				forceseter = true;
				rend.sprite = selectedForMoveSprite;
				//rend.color = new Color32 (255, 0, 185, 255);
			}
		} else {
			Debug.LogWarning ("Player Not Detected!");
		}
	}
		
//	public void NewObjectOnHex(Transform NoOH) {

	//	objectOnhex = NoOH;
	//}
//	public void NewOnHex(string NoH) {
		
//		OnHex = NoH;
//	}

	public void Reset() {
		forceseter = false;
		lightup = false;
		rend.color = new Color32 (255, 255, 255, 255);
		seter = false;
	}

	//[ServerCallback]
	//public void FixedUpdate()
	//{
	//	if (!isServer) {
///			return;
////		}
	//	if (OnHex == "Player") {
	//		ShownUpdate = "Quantum Plasma";
	//	}
	//}
    public void Update()
    {
		//ChangeThings ();
		//player = GameObject.Find ("Player");
		//if (player != null) {
		//	playerscript = player.GetComponent<Player> ();
		//}

		if (playerscript != null) {
			hexshaker = GameObjectToShake;
			if (playerscript.hexes.Contains (thisishex) && forceseter == false && playerscript.MOVINGCAT == true) {
				//rend.color = new Color32 (255, 0, 185, 255);
				rend.sprite = selectedForMoveSprite;
			} else {
				if (overhex == false && forceseter == false) {
				//rend.color = new Color32 (255, 255, 255, 255);
					rend.sprite = defaultSprite;
				}
			}
			if (playerscript.attackedhexes.Contains (thisishex) && forceseter == false) {
				//rend.color = new Color32 (0, 235, 255, 255);
				rend.sprite = selectedForAttackSprite;
			}
		}
    }

    void LateUpdate() {
        datarowid = rowid;
		datanumid = numberid;
	}


	//[Command] public void CmdSetPlayer(GameObject playerTransform) {
	//	RpcSetPlayer (playerTransform);
	//}

	//[Command] public void CmdRemPlayer() {
	//	RpcRemPlayer ();
	//}

	///[ClientRpc] public void RpcSetPlayer(GameObject PlayerTransform) {
	//	OnHex = "Player";
	//	Debug.Log ("Setting Player With RPC");
	//	objectOnhex = PlayerTransform.transform;
	//	//objectOnhex = playerObject;
	//}

	//[ClientRpc] public void RpcRemPlayer() {
	//	if (OnHex == "Player") {
	//		OnHex = " ";
	//		Debug.Log ("Removing Player From Hex With RPC");
	//		objectOnhex = null;
	//	}
	//}
		
}
