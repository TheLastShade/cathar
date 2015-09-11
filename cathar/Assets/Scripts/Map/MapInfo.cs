using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapInfo : MonoBehaviour {
	
	public Action<string, string> OnTriggerTeleport = delegate {};
	public Transform m_CameraTopLeft;
	public Transform m_CameraBottomRight;

	public List<MapColliderLayer> m_ListMapColliderLayer = new List<MapColliderLayer>();

	public List<SpawnPointDataInfo> m_ListSpawnPoint;

	public List<GameObject> m_ListGpi;

	private List<MapTeleport> m_ListMapTeleport = new List<MapTeleport>();

	public void UnLoad ()
	{
		foreach (MapTeleport mapTeleport in m_ListMapTeleport) {
			mapTeleport.OnTriggerTeleport -= OnTriggerTeleport_Internal;
		}
		m_ListMapColliderLayer = null;
	}

	public void LoadGpi()
	{
		GameObject gpiContainer = new GameObject ();
		gpiContainer.name = "GpiContainer";
		gpiContainer.transform.SetParent (transform, false);

		if (m_ListGpi != null) {
			foreach (GameObject gpi in m_ListGpi) {
				GameObject instanceGpi = Instantiate (gpi);
				instanceGpi.transform.SetParent(gpiContainer.transform, true);
			}
		}
	}

	public Vector3 GetSpawnPoint (string aSpawnPointName)
	{
		if (string.IsNullOrEmpty (aSpawnPointName)) {
			if(m_ListSpawnPoint != null){
				foreach (SpawnPointDataInfo spawn in m_ListSpawnPoint) {
					if(spawn.m_IsDefault){
						return spawn.m_Position;
					}
				}
			}


			return Vector3.zero;
		}
		else{
			if(m_ListSpawnPoint != null){
				foreach (SpawnPointDataInfo spawn in m_ListSpawnPoint) {
					if(spawn.m_SpawnName == aSpawnPointName){
						return spawn.m_Position;
					}
				}
			}
		}

		return Vector3.zero;
	}

	public void RegisterTeleportEvent (MapTeleport aMapTeleport)
	{
		aMapTeleport.OnTriggerTeleport += OnTriggerTeleport_Internal;
		m_ListMapTeleport.Add(aMapTeleport);
	}

	void OnTriggerTeleport_Internal (string aMapToLoad, string aSpawnPoint)
	{
		OnTriggerTeleport (aMapToLoad, aSpawnPoint);
	}

	//TODO Have better way to change layer
	public void ChangeCollisionLayer (List<string> aLayerToActivate, List<string> aLayerToDeactivate)
	{
		foreach (MapColliderLayer mapColliderLayer in m_ListMapColliderLayer) {
			if(aLayerToActivate.Contains(mapColliderLayer.m_ColliderLayer)){
				mapColliderLayer.gameObject.SetActive(true);
			}else if(aLayerToDeactivate.Contains(mapColliderLayer.m_ColliderLayer)){
				mapColliderLayer.gameObject.SetActive(false);
			}
		}
	}
}
