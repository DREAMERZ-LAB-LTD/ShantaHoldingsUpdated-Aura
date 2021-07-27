using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
	public static InfoUI instance;
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		if(instance !=this)
		{
			Destroy(gameObject);
		}
	}
	
	public GameObject infoPenel;
	[SerializeField] Text text;
	
	public void SetInfo(string str)
	{
		infoPenel.SetActive(true);
		text.text = str;
	}
}
