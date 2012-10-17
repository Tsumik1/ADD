using UnityEngine;
using System.Collections;

public class MortarAI : MonoBehaviour {
	
	public GameObject explosion; 
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision other)
	{
		Instantiate (explosion, other.contacts[0].point, Quaternion.identity);
		Destroy (gameObject);
	}
}
