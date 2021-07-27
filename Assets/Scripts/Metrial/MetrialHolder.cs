using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetrialHolder : MonoBehaviour
{

	[SerializeField] Transform cam;
	public Color[] colors;
	public Texture[] texture;
	public Material metrial;
	
	public void SelectMetrial()
	{
		MetrialUI.instance.SelectAMetrial(colors, texture, metrial);
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		transform.LookAt(cam);
	}
}
