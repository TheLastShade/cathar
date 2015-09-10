using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapExporter : MonoBehaviour {
	public string m_MapName;
	public List<string> m_ActivatedColliderLayers;
	
	public MapDataInfo CreateMapDataInfo()
	{
		MapDataInfo mapDataInfo = new MapDataInfo();

		mapDataInfo.m_MapName = m_MapName;
		mapDataInfo.m_ActivatedColliderLayers = m_ActivatedColliderLayers;

		BaseExporter[] baseExporter = gameObject.GetComponentsInChildren<BaseExporter> ();
		
		for (int i = 0; i < baseExporter.Length; i++) {
			HandleExporter(mapDataInfo, baseExporter[i]);
		}

		return mapDataInfo;
	}
	
	void HandleExporter (MapDataInfo aMapDataInfo, BaseExporter aBaseExporter)
	{
		if (aBaseExporter is GroundExporter) {
			GroundExporter typedExporter = (GroundExporter)aBaseExporter;
			aMapDataInfo.m_GroundDataInfo.Add (typedExporter.ToGroundDataInfo ());
		} else if (aBaseExporter is CameraLimitExporter) {
			CameraLimitExporter typedExporter = (CameraLimitExporter)aBaseExporter;
			aMapDataInfo.m_CameraLimitDataInfo.Add (typedExporter.ToCameraLimitDataInfo());
		} else if (aBaseExporter is ColliderExporter) {
			ColliderExporter typedExporter = (ColliderExporter)aBaseExporter;
			aMapDataInfo.m_ColliderDataInfo.Add (typedExporter.ToColliderDataInfo());
		} else if (aBaseExporter is SpawnPointExporter) {
			SpawnPointExporter typedExporter = (SpawnPointExporter)aBaseExporter;
			aMapDataInfo.m_SpawnPointDataInfo.Add (typedExporter.ToSpawnPointDataInfo());
		} else if (aBaseExporter is TeleportExporter) {
			TeleportExporter typedExporter = (TeleportExporter)aBaseExporter;
			aMapDataInfo.m_TeleportDataInfo.Add (typedExporter.ToTeleportDataInfo());
		} else {
			Debug.LogError("UnHandled Type of Exporter :: Name =" +aBaseExporter.name + " :: TypeOf =" + aBaseExporter.GetType());
		}
	}
}


[Serializable]
public class MapDataInfo
{
	public string m_MapName;
	public List<string> m_ActivatedColliderLayers = new List<string>();

	public List<GroundDataInfo> m_GroundDataInfo = new List<GroundDataInfo>();
	
	public List<CameraLimitDataInfo> m_CameraLimitDataInfo = new List<CameraLimitDataInfo>();
	
	public List<ColliderDataInfo> m_ColliderDataInfo = new List<ColliderDataInfo>();
	
	public List<SpawnPointDataInfo> m_SpawnPointDataInfo = new List<SpawnPointDataInfo>();
	
	public List<TeleportDataInfo> m_TeleportDataInfo = new List<TeleportDataInfo>();

}