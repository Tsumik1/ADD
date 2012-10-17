using UnityEngine;
using System.Collections;

public class MortarTower : MonoBehaviour {
	
	public float fireRate = 1.0f; 
	public GameObject bullet; 
	public float bulletSpeed = 1.0f;
	public float range = 10.0f;
	public float lobAmount = 1.0f; 
	public float speed = 1.0f; 
	public bool isOn = false; 
	public GameObject rangeHelper; 
	
		private GameObject helper;
	// Use this for initialization
	void Start () 
	{
		InvokeRepeating ("SpawnBullet", fireRate, fireRate);
		helper = Instantiate (rangeHelper, transform.position, Quaternion.identity) as GameObject;;
			//GameObject.CreatePrimitive (PrimitiveType.Sphere);
		Vector3 rangeSphere = new Vector3(range*2,0.1f,range*2);
		helper.transform.localScale = rangeSphere;
		helper.transform.position = new Vector3(transform.position.x,transform.position.y+0.6f,transform.position.z);
		helper.transform.parent = transform;
		helper.collider.enabled = false;
	}
	
	void SpawnBullet()
	{
		if(isOn)
		{
		GameObject target = null;
		foreach(Collider col in Physics.OverlapSphere (transform.position, range))
		{
			if(col.tag == "enemy")
			{
				target = col.gameObject;
				break;
			}
		}
		if(target != null)
		{
			Quaternion lookingAt = Quaternion.LookRotation (target.transform.position - transform.position);
		GameObject newBullet = Instantiate(bullet, transform.position, lookingAt) as GameObject;
		Vector3 yForce = new Vector3(0,lobAmount,0);
		Vector3 xForce = (target.transform.position - transform.position).normalized;		
				speed = lobAmount/-Physics.gravity.y;
		//newBullet.rigidbody.AddForce ((target.transform.position - transform.position).normalized * bulletSpeed, ForceMode.VelocityChange);	
				
				newBullet.rigidbody.AddForce(yForce + (xForce * speed), ForceMode.VelocityChange );
		}
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(isOn)
		{
		 if(helper)
			{
				Destroy (helper);
			}
		}
	}
	
	public void EnableFiring()
	{
		isOn = true; 
	}
}
