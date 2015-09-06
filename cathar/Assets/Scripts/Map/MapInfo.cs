using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapInfo : MonoBehaviour {

	public Transform m_CameraTopLeft;
	public Transform m_CameraBottomRight;

	public List<GameObject> m_ListGpi;

	public void LoadGpi()
	{
		GameObject gpiContainer = new GameObject ();
		gpiContainer.name = "GpiContainer";
		gpiContainer.transform.SetParent (transform, false);

		foreach (GameObject gpi in m_ListGpi) {
			GameObject instanceGpi = Instantiate (gpi);
			instanceGpi.transform.SetParent(gpiContainer.transform, true);
		}
	}
}
