/* 
 * Esse Script movimenta o GameObject quando você clica ou
 * mantém o botão esquerdo do mouse apertado.
 * 
 * Para usá-lo, adicione esse script ao gameObject que você quer mover
 * seja o Player ou outro
 * 
 * Autor: Vinicius Rezendrix - Brasil
 * Data: 11/08/2012
 * 
 * This script moves the GameObeject when you
 * click or click and hold the LeftMouseButton
 * 
 * Simply attach it to the gameObject you wanna move (player or not)
 * 
 * Autor: Vinicius Rezendrix - Brazil
 * Data: 11/08/2012 
 *
 */
 
using UnityEngine;
using System.Collections;
 
public class ObjectPlacement : MonoBehaviour {
	private Transform myTransform;				// this transform
	private Vector3 destinationPosition;		// The destination Point
	private float destinationDistance;			// The distance between myTransform and destinationPosition
 	private bool validPlace = true; 
	public bool isPlaced = false; 
	private Color defaultColour;
	//private Component script;
	public float moveSpeed;						// The Speed the character will move
 	
 
 
	void Start () {
		myTransform = transform;							// sets myTransform to this GameObject.transform
		destinationPosition = myTransform.position;			// prevents myTransform reset
		defaultColour = renderer.material.color; 
	}
 
	void Update () {
 		if(!isPlaced)
		{
			// keep track of the distance between this gameObject and destinationPosition
			destinationDistance = Vector3.Distance(destinationPosition, myTransform.position);
	 
			if(destinationDistance < .5f){		// To prevent shakin behavior when near destination
				moveSpeed = 0;
			}
			else if(destinationDistance > .5f){			// To Reset Speed to default
				moveSpeed = 100;
			}
	 
	
			// Moves the Player if the Left Mouse Button was clicked
	
				Plane playerPlane = new Plane(Vector3.up, myTransform.position);
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				float hitdist = 0.0f;
	 
				if (playerPlane.Raycast(ray, out hitdist)) {
					Vector3 targetPoint = ray.GetPoint(hitdist);
					destinationPosition = ray.GetPoint(hitdist);
					Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
					//myTransform.rotation = targetRotation;
				}
	//		// Moves the player if the mouse button is hold down
	//		else if (Input.GetMouseButton(0)&& GUIUtility.hotControl ==0) {
	// 
	//			Plane playerPlane = new Plane(Vector3.up, myTransform.position);
	//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	//			float hitdist = 0.0f;
	// 
	//			if (playerPlane.Raycast(ray, out hitdist)) {
	//				Vector3 targetPoint = ray.GetPoint(hitdist);
	//				destinationPosition = ray.GetPoint(hitdist);
	//				Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
	//				myTransform.rotation = targetRotation;
	//			}
	//		//	myTransform.position = Vector3.MoveTowards(myTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
	//		}
	 
			// To prevent code from running if not needed
			if(destinationDistance > .2f){
				myTransform.position = Vector3.MoveTowards(myTransform.position, destinationPosition, (moveSpeed * Time.deltaTime) * 2);
			}
			
		if(validPlace)
			{
			  renderer.material.color = Color.green; 
//			foreach (Transform child in transform) 
//			{
//            	child.renderer.material.color = Color.green; 
//        	}
			//transform.Find("Turret").renderer.material.color = Color.green;
			}
		else
			{
				renderer.material.color = Color.red; 
							//transform.Find("Turret").renderer.material.color = Color.red;
			}
		}
		
		if(Input.GetMouseButtonDown(0) && validPlace)
		{
			renderer.material.color = defaultColour; 
			//transform.Find("Turret").renderer.material.color = defaultColour;
			isPlaced = true;
			//BasicTower script = GetComponent ("BasicTower") as BasicTower;
			SendMessage("EnableFiring");
			//Destroy (this);
			this.enabled = false;
			
		}
		
		if(Input.GetMouseButtonDown (1))
		{
			Destroy (gameObject);
		}
	}
	
		void OnCollisionEnter(Collision collision)
	{
			validPlace = false; 
	}
	 
	void OnCollisionStay(Collision collision)
	{
			validPlace = false; 
	}
	
	void OnCollisionExit(Collision collision) {

			validPlace = true;
    }
	
	public void InvalidLocation(bool valid)
	{
		print("GOT HERE");
		validPlace = valid; 
	}
}