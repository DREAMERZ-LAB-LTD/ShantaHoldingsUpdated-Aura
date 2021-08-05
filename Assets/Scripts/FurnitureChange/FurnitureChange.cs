using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureChange : MonoBehaviour
{
	[SerializeField] GameObject OnObject;
	[SerializeField] GameObject OffObject;
    [SerializeField] string typeObject;
    [SerializeField] int type;
    
    [SerializeField] FurnitureChange otherMetrialObject;
    public GameObject hilight;

    private void ShowHilight()
    {
        hilight.SetActive(true);
        otherMetrialObject.hilight.SetActive(false);
    }

    private void Awake()
    {
        PlayerPrefs.SetInt(typeObject, 1);
        Debug.Log(typeObject+PlayerPrefs.GetInt(typeObject, 1).ToString());
    }

    private void OnEnable()
    {
        if(type==PlayerPrefs.GetInt(typeObject, 1))
        {
            Debug.Log(typeObject + type.ToString());
            ShowHilight();
        }
    }

    

    public void Clicked()
	{
		OnObject.SetActive(true);
		OffObject.SetActive(false);
        PlayerPrefs.SetInt(typeObject, type);
        ShowHilight();
	}
}
