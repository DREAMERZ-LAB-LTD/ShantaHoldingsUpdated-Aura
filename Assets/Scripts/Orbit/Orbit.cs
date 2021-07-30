using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Orbit : MonoBehaviour
{
	
	[SerializeField] float sencativity = 5f;
    [SerializeField] float min = 10f;
    [SerializeField] float max = 60f;
	Vector3 privpos = Vector3.zero;
	
	// This function is called when the object becomes enabled and active.
	protected void OnEnable()
	{
		isDown = false;
	}
	bool isDown;
    void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			privpos = Input.mousePosition;
			isDown = true;
		}		
		if(Input.GetMouseButtonUp(0))
		{
			isDown = false;
		}
		
		
	}
	
	// LateUpdate is called every frame, if the Behaviour is enabled.
	protected void LateUpdate()
	{
		if(isDown)
		{
			
			
			Vector3 vec = transform.eulerAngles;
			//Debug.Log(vec.x);
			//Left
			if(Input.GetKey(KeyCode.A) || privpos.x > Input.mousePosition.x && 0.1< Mathf.Abs(privpos.x - Input.mousePosition.x))
			{
			   
				transform.Rotate(Vector3.down * sencativity * Time.deltaTime, Space.World);
			
			    
			}
			//Right
			else if(Input.GetKey(KeyCode.D) || privpos.x < Input.mousePosition.x&& 0.1< Mathf.Abs(privpos.x - Input.mousePosition.x))
			{
			    
			
				transform.Rotate(Vector3.up * sencativity * Time.deltaTime, Space.World);
			   
			}
		    
			//Up
			if(Input.GetKey(KeyCode.W) || privpos.y > Input.mousePosition.y&& 0.1 < Mathf.Abs(privpos.y - Input.mousePosition.y) && vec.x > min )
			{
			   
			
				transform.Rotate(Vector3.left * sencativity/3 * Time.deltaTime, Space.Self);
			  
			}
			//Down
			else if(Input.GetKey(KeyCode.S) || privpos.y < Input.mousePosition.y && 1< Mathf.Abs(privpos.y - Input.mousePosition.y) && vec.x < max)
			{
			  
			
				transform.Rotate(Vector3.right * sencativity/3 * Time.deltaTime, Space.Self);
			   
			}
			
			
			
		}
		
		privpos = Input.mousePosition;
	}

}
