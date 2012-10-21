using UnityEngine;
using System.Collections;
using System;

public class BasicTower : MonoBehaviour {
	
	public float fireRate = 1.0f; 
	public GameObject bullet; 
	public GameObject rangeHelper; 
	public GameObject pip;
	public float bulletSpeed = 1.0f;
	public float range = 10.0f;
	public int initialDamage = 10;
	public int initialUpgradeCost;
	public int startLevel;
	public int maxLevel;
	
	public bool isOn = false; 
	
	private GameObject helper; 
	private int currentLevel;
	private int damage;
	private int upgradeCost;
	private GameObject[] pips;
	private float[] pipPositions;
	private float initialX = 0f; 
	public float lobAmount = 14f;
	public float lobSpeed = 200f;
	public bool selected = false; 
	
   public enum type
	{
		basic = 0,
		mortar = 1
	}
	public type towerType;
	// Use this for initialization
	void Start ()
	{
		pips = new GameObject[maxLevel];
		pipPositions = new float[maxLevel];
		InvokeRepeating ("SpawnBullet", fireRate, fireRate);
		currentLevel= startLevel;
		CreateRange ();
		damage = initialDamage;
		upgradeCost = initialUpgradeCost;
		AddPip(currentLevel);
	}
	void AddPip(int level)
	{
		level--;
		if(level == 0)
		{
			pips[level] = Instantiate (pip,transform.position, Quaternion.identity) as GameObject;
			pips[level].transform.parent = transform;
			BoxCollider col = GetComponent (typeof(BoxCollider)) as BoxCollider;
			//print (col.size.z/2);
			initialX = col.size.x / (float)maxLevel;
			print (initialX);
			print (col.size.x/2);
			Vector3 pipPosition = new Vector3(0, 0, col.transform.position.z); //- (col.size.z/2));

			float newPositionX = initialX;
			pipPositions[0] = 0f;
			for(int i=0; i < maxLevel; i++)
			{
				print(initialX);
				pipPositions[i] = newPositionX - col.size.x/1.5f;
				newPositionX += initialX;
			}
			//Array.Reverse(pipPositions);
			//Altering to factor for differnet mesh sizes/
			pipPosition.z -= col.size.z/2 + 0.2f;
			pipPosition.x = pipPositions[0];
			pipPosition.y -= col.size.y/2;
			//print(pipPosition.z);
			pips[level].transform.localPosition = pipPosition;
		}
		else
		{
			pips[level] = Instantiate (pip,pips[0].transform.position, Quaternion.identity) as GameObject;
			pips[level].transform.parent = transform;
			Vector3 newPosition = pips[level].transform.localPosition;
			newPosition.x = pipPositions[level];
			pips[level].transform.localPosition = newPosition;
		}
	}
	void CreateRange()
	{
	if(rangeHelper)
		{
		helper = Instantiate (rangeHelper, transform.position, Quaternion.identity) as GameObject;;
		//Object.CreatePrimitive(PrimitiveType.Sphere);
		Vector3 rangeSphere = new Vector3(range*2 , 0.1f,range*2f);	
		helper.transform.localScale = rangeSphere;
		helper.transform.position = new Vector3(transform.position.x,transform.position.y+0.6f,transform.position.z);
		helper.transform.parent = transform;
		helper.collider.enabled = false;
		}
	}
	
	public int GetDamage()
	{
		return damage;
	}
	public void Upgrade ()
	{
	  if(MoneyManager.money - upgradeCost < 0)
		{
			print("InsufficientFunds");
		}
		else
		{
			if(currentLevel < maxLevel)
			{
				Destroy(helper);
				range *= 1.1f;
				bulletSpeed *= 1.3f;
				//bullet.GetComponent<BasicBullet>().damage *= 2;
				damage *= 2; 
				currentLevel++;
				CreateRange ();
				AddPip (currentLevel);
				MoneyManager.money -= upgradeCost;
				upgradeCost *=2;
			}
			else
			{
				print ("Is At Max Level");
			}
		}
	}
	
	public int GetCurrentLevel()
	{
		return currentLevel;
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
				newBullet.transform.parent = transform;
				
				if(towerType == type.basic)
				{
					newBullet.transform.eulerAngles = new Vector3(90,newBullet.transform.eulerAngles.y,newBullet.transform.eulerAngles.z);
					newBullet.rigidbody.AddForce ((target.transform.position - transform.position).normalized * bulletSpeed, ForceMode.VelocityChange);	
				}
				else if(towerType == type.mortar)
				{
					Vector3 yForce = new Vector3(0,lobAmount,0);
					Vector3 xForce = (target.transform.position - transform.position).normalized;		
					lobSpeed = lobAmount/-Physics.gravity.y;	
					newBullet.rigidbody.AddForce(yForce + (xForce * lobSpeed), ForceMode.VelocityChange );
				}
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
				if(towers[i].helper)
				{
					towers [i].helper.renderer.enabled = false;
				}
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

