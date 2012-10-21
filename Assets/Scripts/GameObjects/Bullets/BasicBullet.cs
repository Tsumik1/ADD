using UnityEngine;
using System.Collections;

public class BasicBullet : MonoBehaviour {
	
	public GameObject explosion; 
	public GameObject shards; 
	public float bulletLife = 1.5f;
	public int damage = 10; 
	
	// Use this for initialization
	void Start () 
	{
		Invoke ("RemoveFromScreen", bulletLife);
		damage = transform.parent.GetComponent<BasicTower>().GetDamage();
		//print(damage);
	}
	
	void RemoveFromScreen()
	{
		if(explosion && shards)
		{
			Instantiate (explosion,transform.position, transform.rotation);
			Instantiate (shards,transform.position, transform.rotation);
		}
		
			Destroy (gameObject);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.parent = null;
	}
	
		void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "enemy")
		{
			if(explosion)
			{
				Instantiate (explosion,transform.position, transform.rotation);
				
			}
			other.gameObject.SendMessage ("TakeDamage", damage);
			
			Destroy (gameObject);
		}
	}
}
