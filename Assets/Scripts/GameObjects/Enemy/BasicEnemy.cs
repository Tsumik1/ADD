using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour {

	public int health = 100; 
	public bool gotToBase = false; 
	// Use this for initialization
	void Start () {
	
		gotToBase = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "bullet")
		{
			Destroy (other.gameObject);
		}
	}
	
void TakeDamage(int damage)
	{
		health -=damage; 
			if(health <=0)
			{
				Destroy (gameObject);
			}
	}
	
	void GotToBase()
	{
		gotToBase = true; 
		
	}
}
