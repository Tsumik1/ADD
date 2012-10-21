using UnityEngine;
using System.Collections;

public class UpgradeButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Clicked()
	{
		BasicTower tower = TowerManager.GetSelected().GetComponent (typeof(BasicTower)) as BasicTower;
		tower.Upgrade();
		
	}
}
