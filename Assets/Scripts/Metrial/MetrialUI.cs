using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetrialUI : MonoBehaviour
{
	public static MetrialUI instance;
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
	
	public GameObject metrailPenel;
	public GameObject ObjectPenel;
	[SerializeField] GameObject[] metrialList;
	
	private Material model;
	
	public void SelectAMetrial(Color[] clr, Texture[] tex , Material obj)
	{
		foreach( GameObject ob in metrialList)
		{
			ob.SetActive(false);
		}
		model = obj;
		metrailPenel.SetActive(true);
		int i=0;
		foreach(Color c in clr)
		{
			if(i >= metrialList.Length) return;
			
			metrialList[i].SetActive(true);
			metrialList[i].GetComponent<MetrialSetUp>().GetMetrial(c);
			i++;
		}
		foreach(Texture t in tex)
		{
			if(i >= metrialList.Length) return;
			
			metrialList[i].SetActive(true);
			metrialList[i].GetComponent<MetrialSetUp>().GetMetrial(t);
			i++;
		}
		
	}
	
	public void SetMetrial(Color clr, Texture tex)
	{
		if(model == null) return;
	
		model.color = clr;
		if(tex != null) 
		{
			model.color = Color.white;
			model.mainTexture = tex;
		}
	}
	
	
}

