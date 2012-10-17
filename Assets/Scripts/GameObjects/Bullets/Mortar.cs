using UnityEngine;
using System.Collections;

public class Mortar : MonoBehaviour {
	
	public GameObject explosion; 
	public float bulletLife = 1.5f;
	public int damage = 10; 
	// Use this for initialization
	void Start () 
	{
		Invoke ("RemoveFromScreen", bulletLife);
	}
	
	void RemoveFromScreen()
	{
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
		void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "enemy")
		{
			if(explosion)
			{
				Instantiate (explosion,transform.position, transform.rotation);
				other.gameObject.SendMessage ("TakeDamage", damage);
			}
			Destroy (gameObject);
		}
	}
}
