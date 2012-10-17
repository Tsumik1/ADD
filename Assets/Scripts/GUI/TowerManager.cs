using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour {
	
	
	public static GameObject selected;
	// Use this for initialization
	void Start () 
	{
		selected = null;
	}
	
	public static void SetSelected (GameObject current)
	{
		selected = current; 
	}
	
	public static GameObject GetSelected()
	{
		return selected; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
