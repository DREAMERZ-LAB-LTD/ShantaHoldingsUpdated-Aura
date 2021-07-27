using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetrialSetUp : MonoBehaviour
{
	public Material material;
	public void GetMetrial(Color clr)
	{
		material.color = clr;
		material.mainTexture = null;
	}
	public void GetMetrial(Texture tex)
	{
		material.color = Color.white;
		material.mainTexture = tex;
	}
	
	public void SetMetrialToObject()
	{
		MetrialUI.instance.SetMetrial(material.color, material.mainTexture);
	}
	
}
