using UnityEngine;
using System.Collections;

public class CreateTowerOnClicked : MonoBehaviour {

	public TowerSelect towerSelector;
	public Transform spawnNode; 
	void Clicked(Vector3 position)
	{
		if(MoneyManager.money >= towerSelector.GetSelectedTowerCost())
		{
					GameObject tower = towerSelector.GetSelectedTower();
		Instantiate (tower, spawnNode.position + Vector3.up, tower.transform.rotation);
			MoneyManager.money -= towerSelector.GetSelectedTowerCost ();
		}

	}
}
