using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetrialSetUp : MonoBehaviour
{
	public Material material;
    public int typeNo;
    public string type;


    [SerializeField] MetrialSetUp otherMetrial;
    public GameObject hilight;

    private void ShowHilight()
    {
        hilight.SetActive(true);
        otherMetrial.hilight.SetActive(false);
    }

    public void GetMetrial(Color clr)
	{
		material.color = clr;
		material.mainTexture = null;
	}
	public void GetMetrial(Texture tex, string matrialType)
	{
		material.color = Color.white;
		material.mainTexture = tex;
        type = matrialType;

        if(typeNo == PlayerPrefs.GetInt(matrialType, 0))
        {
            Debug.Log(matrialType + typeNo.ToString());
            ShowHilight();
        }
	}
	
	public void SetMetrialToObject()
	{
		MetrialUI.instance.SetMetrial(material.color, material.mainTexture);
        PlayerPrefs.SetInt(type, typeNo);
        ShowHilight();
    }
	
}
