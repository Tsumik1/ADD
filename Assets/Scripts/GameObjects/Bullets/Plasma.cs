using UnityEngine;
using System.Collections;

public class Plasma : MonoBehaviour {
	
	public GameObject explosion; 
	public float bulletLife = 1.5f;
	public int damage = 10; 
	
	public float slowAmount = 0.5f;
	public float slowTime; 
	
	public GameObject restoreSpeed;
	// Use this for initialization
	void Start () 
	{
		Invoke ("RemoveFromScreen", bulletLife);
	}
	
	void RemoveFromScreen()
	{
		Destroy (gameObject);
		//CancelInvoke ("RemoveFromScreen");
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "enemy")
		{
			if(!other.collider.GetComponentInChildren<RestoreCondition>())
			{
				
				
				if(explosion )
				{
					Instantiate (explosion,transform.position, transform.rotation);
					GameObject restoreSpeedInstance = Instantiate(restoreSpeed) as GameObject;
					PathThroughObjects scriptInstance = other.collider.GetComponent<PathThroughObjects>(); 
					RestoreCondition script2 = restoreSpeedInstance.GetComponent<RestoreCondition>();				
					script2.time = slowTime; 
					script2.originalSpeed = scriptInstance.speed;
					scriptInstance.speed *= slowAmount;
					restoreSpeedInstance.transform.parent = other.collider.transform;


					//other.gameObject.SendMessage ("TakeDamage", damage);
				}
				else
				{
					
					GameObject restoreSpeedInstance = Instantiate(restoreSpeed, other.collider.transform.position, Quaternion.identity) as GameObject;
					restoreSpeedInstance.transform.parent = other.collider.transform;
					PathThroughObjects scriptInstance = other.collider.GetComponent<PathThroughObjects>(); 
										RestoreCondition script2 = restoreSpeedInstance.GetComponent<RestoreCondition>();
					script2.time = slowTime; 
					script2.originalSpeed = scriptInstance.speed;
					scriptInstance.speed *= slowAmount;

				}
				Destroy (gameObject);
			}
		other.gameObject.SendMessage ("TakeDamage", damage);
		}
	}
}