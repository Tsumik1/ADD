using UnityEngine;
using System.Collections;

public class BaseScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		

	}
	
	
	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "enemy")
		{
			other.gameObject.SendMessage ("GotToBase");
			
			print ("GotToBase");
			Destroy (other.gameObject);
			
			HealthManager.health -= 1;
		}
	}
	}
