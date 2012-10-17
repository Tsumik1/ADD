using UnityEngine;
using System.Collections;

public class MoveButton : MonoBehaviour {
	
	private ObjectPlacement placer; 
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (TowerManager.GetSelected() != null) {
			placer = TowerManager.selected.GetComponent (typeof(ObjectPlacement)) as ObjectPlacement;
		}
		
	}
	
	void Clicked ()
	{
		if (placer) {
			placer.enabled = true;
			placer.isPlaced = false;
			placer.transform.GetComponent<BasicTower> ().TurnHelperOn ();
		}
		
	}
}
