using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetrialHolder : MonoBehaviour
{

	//[SerializeField] Transform cam;
	public Color[] colors;
	public Texture[] texture;
	public Material metrial;
    public string metrialType;
    public int selectMetrialNo;

    private void Start()
    {
        if (0 == PlayerPrefs.GetInt("JustInstall", 0))
        {
            PlayerPrefs.SetInt(metrialType, selectMetrialNo);
            PlayerPrefs.SetInt("JustInstall", 1);
        }
    }

    public void SelectMetrial()
	{
		MetrialUI.instance.SelectAMetrial(colors, texture, metrial, metrialType);
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		//transform.LookAt(cam);
	}
}
