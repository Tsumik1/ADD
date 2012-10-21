using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour {
	
	
	public int initialHealth = 100;
	public int health = 100; 
	public bool gotToBase = false; 
	
	public GameObject healthBar;
	
	
	private GameObject healthy;
	// Use this for initialization
	void Start ()
	{
	
		gotToBase = false;
		health = initialHealth;
		healthy = Instantiate (healthBar,transform.position,Quaternion.identity) as GameObject;
		healthy.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void GetLife ()
	{
		Vector2 lifeValues = new Vector2((float)health,(float)initialHealth);
		healthy.SendMessage ("SetLife", lifeValues);
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
