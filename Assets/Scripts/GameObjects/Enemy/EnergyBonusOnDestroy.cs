using UnityEngine;
using System.Collections;

public class EnergyBonusOnDestroy : MonoBehaviour {
	
	
	public int moneyBonus; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDestroy()
	{
	    BasicEnemy owner = GetComponent("BasicEnemy") as BasicEnemy;
		if(owner)
		{
			if(owner.gotToBase)
			{
				//Do Nothing :). 
			}	
			else
			{
				MoneyManager.money += moneyBonus; 
			}
		}
		else
		{
			//Add cash bonus 
			MoneyManager.money += moneyBonus; 
		}
	}
}
