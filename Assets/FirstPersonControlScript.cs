using UnityEngine;
using System.Collections;

public class FirstPersonControlScript : MonoBehaviour {
							// The transform used for camera rotatio

	private Transform thisTransform;
	public CharacterController character;
	private Vector3 cameraVelocity;
	private Vector3 velocity;						// Used for continuing momentum while in air
	private bool canJump = true;

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W)) {
			character.Move(character.transform.forward*2f);
				}

		if (Input.GetKey (KeyCode.S)) {

			character.Move(character.transform.forward*-2f);
				}

		
		if (Input.GetKey (KeyCode.A)) {
			
			character.Move(character.transform.right*-2f);
		}

		
		if (Input.GetKey (KeyCode.D)) {
			
			character.Move(character.transform.right*2f);
		}





			float h = 6.0f * Input.GetAxis ("Mouse X");
			transform.Rotate (0, h, 0);
		    float v = 6.0f * Input.GetAxis ("Mouse Y");

			Camera.main.transform.Rotate (-v, 0, 0);
			
	

	}
}
