using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureChange : MonoBehaviour
{
	[SerializeField] GameObject OnObject;
	[SerializeField] GameObject OffObject;
	public void Clicked()
	{
		OnObject.SetActive(true);
		OffObject.SetActive(false);
	}
}
