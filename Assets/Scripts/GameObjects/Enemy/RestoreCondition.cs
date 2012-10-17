using UnityEngine;
using System.Collections;

public class RestoreCondition : MonoBehaviour {
	
	
	public float originalSpeed; 
	public float time; 
	
	// Use this for initialization
	void Start () 
	{
		Invoke ("RestoreConditionFunction", time);
	}
	
	void RestoreConditionFunction()
	{
		PathThroughObjects script = gameObject.transform.parent.GetComponent<PathThroughObjects>();
		script.speed = originalSpeed; 
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
