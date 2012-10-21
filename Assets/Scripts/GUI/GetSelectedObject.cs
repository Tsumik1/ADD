using UnityEngine;
using System.Collections;

public class GetSelectedObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		
		BasicTower[] towers = FindObjectsOfType (typeof(BasicTower)) as BasicTower[];
		
			for(int i = 0; i< towers.Length;i++)
			{
				if(towers[i].selected)
				{
					//transform.GetComponent (MeshFilter).mesh = towers[i].GetComponent (MeshFilter).mesh;
					MeshFilter current = GetComponent (typeof(MeshFilter)) as MeshFilter; 
					MeshFilter replacement = towers[i].GetComponent (typeof(MeshFilter)) as MeshFilter; 
					current.mesh = replacement.mesh; 
					Vector3 newScale = new Vector3(0.09f,0.09f,0.09f);
					transform.localScale = newScale; 
					Vector3 newRotation = new Vector3(0f,90f,0f);
					transform.eulerAngles = newRotation;
					transform.renderer.material = towers[i].renderer.material; 
				}
			}
	}
}
