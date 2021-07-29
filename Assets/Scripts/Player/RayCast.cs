using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
	[SerializeField] PlayerMovement playerMove;
	[SerializeField] Camera camera;
	public void Ray()
	{
        Debug.Log("Ray");
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);
        
		if (Physics.Raycast(ray, out hit)) {
			// Do something with the object that was hit by the raycast.
			Debug.Log(hit.point + " " + hit.transform.name);
			if(hit.transform.tag == "Ground")
			{
				if(MetrialUI.instance.metrailPenel.active == true)
				{
					MetrialUI.instance.metrailPenel.SetActive(false);
					return;
				}
				if(MetrialUI.instance.ObjectPenel.active == true)
				{
					MetrialUI.instance.ObjectPenel.SetActive(false);
					return;
				}
				/*if(InfoUI.instance.infoPenel.active == true)
				{
					InfoUI.instance.infoPenel.SetActive(false);
					return;
				}*/
				playerMove.MoveByPoint(hit.point);
				
			}
			else if(hit.transform.tag == "Info")
			{
				
				if(MetrialUI.instance.metrailPenel.active == true)
				{
					MetrialUI.instance.metrailPenel.SetActive(false);
					return;
				}
				
				Debug.Log("Show Info");
				hit.transform.GetComponent<InfoHolder>().SetInfo();
			}
			else if(hit.transform.tag == "Metrial")
			{
				
				/*if(InfoUI.instance.infoPenel.active == true)
				{
					InfoUI.instance.infoPenel.SetActive(false);
					return;
				}*/
				Debug.Log("Show Metrial");	
				hit.transform.GetComponent<MetrialHolder>().SelectMetrial();
			}
			else if(hit.transform.tag == "MetrialSetUp")
			{
				
				/*if(InfoUI.instance.infoPenel.active == true)
				{
					InfoUI.instance.infoPenel.SetActive(false);
					return;
				}*/
				Debug.Log("Metrial set up");
				hit.transform.GetComponent<MetrialSetUp>().SetMetrialToObject();
			}
			else if(hit.transform.tag == "FurnitureChange")
			{
				if(hit.transform.GetComponent<FurnitureChange>() != null)
				{
					hit.transform.GetComponent<FurnitureChange>().Clicked();
				}
            }
            else
            {
                MetrialUI.instance.metrailPenel.SetActive(false);
            }
			
		}
	}
}
