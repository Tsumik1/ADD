using UnityEngine;
using System.Collections;

public class BasicTower : MonoBehaviour {
	
	public float fireRate = 1.0f; 
	public GameObject bullet; 
	public GameObject rangeHelper; 
	public float bulletSpeed = 1.0f;
	public float range = 10.0f;
	
	public bool isOn = false; 
	
	private GameObject helper; 
	
	public bool selected = false; 
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
			
		//transform.Find("Turret").LookAt(target.transform);
		transform.LookAt (target.transform);
				transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
		GameObject newBullet = Instantiate(bullet, transform.position, lookingAt) as GameObject;
				newBullet.transform.eulerAngles = new Vector3(90,newBullet.transform.eulerAngles.y,newBullet.transform.eulerAngles.z);
		newBullet.rigidbody.AddForce ((target.transform.position - transform.position).normalized * bulletSpeed, ForceMode.VelocityChange);	
		}
		}
		else if(!isOn)
		{
			//Might need this some time. 
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(isOn)
		{
		 if(helper)
			{
				//helper.renderer.enabled = false; //Destroy (helper);
			}
		}
	}
	
	public void EnableFiring ()
	{
		isOn = true; 
		helper.renderer.enabled = false;
	}
	
	public void TurnHelperOn()
	{
		helper.renderer.enabled = true;
	}
	
	
	public bool GetSelected()
	{
		return selected;
	}
	
	void Clicked ()
	{
		BasicTower[] towers = FindObjectsOfType (typeof(BasicTower)) as BasicTower[];
		
		for (int i = 0; i< towers.Length; i++) {
			if (towers [i].transform != transform) {
				towers [i].helper.renderer.enabled = false; 
				towers [i].selected = false;
			}
		}
		
		selected = true; 
		TowerManager.SetSelected (this.gameObject);
		helper.renderer.enabled = true; 

	}
//		 void OnDrawGizmos() {
//	   	 Gizmos.color = Color.red;
//		
//			Gizmos.DrawSphere (transform.position, range);
//		}
}

