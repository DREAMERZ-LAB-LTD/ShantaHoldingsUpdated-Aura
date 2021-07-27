using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHolder : MonoBehaviour
{
	[SerializeField] string infoString;
	
	public void SetInfo()
	{
		InfoUI.instance.SetInfo(infoString);
	}
}
