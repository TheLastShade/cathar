using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapImporter : MonoBehaviour {

	
	const string MAP_NAME = "Map_";
	const string GROUND_NAME = "Ground_";
	const string CAMERA_LIMIT_NAME = "CameraLimit_";
	const string COLLIDER_NAME = "Collider_";
	const string TELEPORT_NAME = "Teleport_";


	[ContextMenu("DebugMapLoad")]
	public void DebugMapLoad()
	{
		LoadMap ("Gym");
	}

	public void UnloadCurrentMap (MapInfo aMapInfoToUnload)
	{
		aMapInfoToUnload.UnLoad ();
		Destroy (aMapInfoToUnload.gameObject);
	}

	public MapInfo LoadMap(string  aMapName)
	{
		string path = ResourcePaths.GetMapPath (aMapName);
		MapDataSO mapDataSO = (MapDataSO)Resources.Load(path, typeof(MapDataSO));

		GameObject mapContainer = new GameObject ();
		mapContainer.name = MAP_NAME + aMapName;
		mapContainer.transform.SetParent (transform, false);

		MapInfo mapInfo = mapContainer.AddComponent<MapInfo> ();

		CreateGround (mapContainer, mapDataSO.m_MapDataInfo.m_GroundDataInfo);
		CreateCameraLimit (mapContainer, mapDataSO.m_MapDataInfo.m_CameraLimitDataInfo, mapInfo);
		CreateCollider (mapContainer, mapDataSO.m_MapDataInfo.m_ColliderDataInfo, mapInfo, mapDataSO.m_MapDataInfo.m_ActivatedColliderLayers);
		mapInfo.m_ListSpawnPoint = mapDataSO.m_MapDataInfo.m_SpawnPointDataInfo;
		CreateTeleport (mapContainer, mapDataSO.m_MapDataInfo.m_TeleportDataInfo, mapInfo);

		return mapInfo;
	}

	void CreateGround (GameObject mapContainer, List<GroundDataInfo> aGroundDataInfo)
	{
		GameObject container = new GameObject ();
		container.name = "GroundContainer";
		container.transform.SetParent (mapContainer.transform, false);

		for (int i = 0; i < aGroundDataInfo.Count; i++) {
			GroundDataInfo groundDataInfo = aGroundDataInfo[i];

			GameObject ground = new GameObject ();
			ground.name = GROUND_NAME+i;
			ground.transform.SetParent (container.transform, false);
			
			SpriteRenderer spriteRenderer = ground.AddComponent<SpriteRenderer>();
			spriteRenderer.sprite = groundDataInfo.m_Sprite;
			
			ground.transform.localPosition = groundDataInfo.m_Position;
			ground.transform.localScale = groundDataInfo.m_Scale;
			ground.transform.localRotation = groundDataInfo.m_Rotation;
		}
	}

	void CreateCameraLimit (GameObject mapContainer, List<CameraLimitDataInfo> aCameraLimitDataInfo, MapInfo aMapInfo)
	{
		GameObject container = new GameObject ();
		container.name = "CameraLimitContainer";
		container.transform.SetParent (mapContainer.transform, false);

		for (int i = 0; i < aCameraLimitDataInfo.Count; i++) {
			CameraLimitDataInfo cameraLimitDataInfo = aCameraLimitDataInfo[i];
			
			GameObject cameraLimit = new GameObject ();
			cameraLimit.name = CAMERA_LIMIT_NAME+i;
			cameraLimit.transform.SetParent (container.transform, false);
			
			cameraLimit.transform.localPosition = cameraLimitDataInfo.m_Position;

			if(cameraLimitDataInfo.m_Type == CameraLimitType.TOP_LEFT)
			{
				aMapInfo.m_CameraTopLeft = cameraLimit.transform;
			}else if(cameraLimitDataInfo.m_Type == CameraLimitType.BOTTOM_RIGHT){
				aMapInfo.m_CameraBottomRight = cameraLimit.transform;
			}
		}
	}

	void CreateCollider (GameObject mapContainer, List<ColliderDataInfo> aColliderDataInfo, MapInfo aMapInfo, List<string> aActivatedColliderLayers)
	{
		GameObject container = new GameObject ();
		container.name = "ColliderContainer";
		container.transform.SetParent (mapContainer.transform, false);
		
		for (int i = 0; i < aColliderDataInfo.Count; i++) {
			ColliderDataInfo colliderDataInfo = aColliderDataInfo[i];
			
			GameObject colliderGo = new GameObject ();
			colliderGo.name = CAMERA_LIMIT_NAME+i;
			colliderGo.transform.SetParent (container.transform, false);
			
			colliderGo.AddComponent<BoxCollider2D>();
			colliderGo.layer = colliderDataInfo.m_Layer;
			colliderGo.transform.localPosition = colliderDataInfo.m_Position;
			colliderGo.transform.localScale = colliderDataInfo.m_Scale;
			colliderGo.transform.localRotation = colliderDataInfo.m_Rotation;

			MapColliderLayer mapColliderLayer = colliderGo.AddComponent<MapColliderLayer>();
			mapColliderLayer.m_ColliderLayer = colliderDataInfo.m_ColliderMapLayer;

			aMapInfo.m_ListMapColliderLayer.Add(mapColliderLayer);
			if(!string.IsNullOrEmpty(colliderDataInfo.m_ColliderMapLayer) && !aActivatedColliderLayers.Contains(colliderDataInfo.m_ColliderMapLayer)){
				colliderGo.SetActive(false);
			}
			
		}
	}

	void CreateTeleport (GameObject mapContainer, List<TeleportDataInfo> a_TeleportDataInfo, MapInfo aMapInfo)
	{
		GameObject container = new GameObject ();
		container.name = "TeleportContainer";
		container.transform.SetParent (mapContainer.transform, false);
		
		for (int i = 0; i < a_TeleportDataInfo.Count; i++) {
			TeleportDataInfo teleportDataInfo = a_TeleportDataInfo[i];
			
			GameObject teleportGo = new GameObject ();
			teleportGo.name = TELEPORT_NAME+i;
			teleportGo.transform.SetParent (container.transform, false);
			
			BoxCollider2D boxCollider = teleportGo.AddComponent<BoxCollider2D>();
			boxCollider.isTrigger = true;
			teleportGo.layer = teleportDataInfo.m_Layer;
			teleportGo.transform.localPosition = teleportDataInfo.m_Position;
			teleportGo.transform.localScale = teleportDataInfo.m_Scale;
			teleportGo.transform.localRotation = teleportDataInfo.m_Rotation;

			MapTeleport mapTeleport = teleportGo.AddComponent<MapTeleport>();
			mapTeleport.m_MapToTeleport = teleportDataInfo.m_MapToTeleport;
			mapTeleport.m_SpawnPointToTeleport = teleportDataInfo.m_SpawnPointToTeleport;

			aMapInfo.RegisterTeleportEvent(mapTeleport);
		}
	}

}
