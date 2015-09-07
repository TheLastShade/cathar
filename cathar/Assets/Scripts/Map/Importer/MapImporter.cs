using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapImporter : MonoBehaviour {

	const string GROUND_NAME = "Ground_";
	const string CAMERA_LIMIT_NAME = "CameraLimit_";
	const string COLLIDER_NAME = "Collider_";

	[ContextMenu("DebugMapLoad")]
	public void DebugMapLoad()
	{
		LoadMap ("Gym");
	}

	public MapInfo LoadMap(string aMapName)
	{
		string path = ResourcePaths.GetMapPath (aMapName);
		MapDataSO mapDataSO = (MapDataSO)Resources.Load(path, typeof(MapDataSO));

		MapInfo mapInfo = gameObject.AddComponent<MapInfo> ();

		CreateGround (mapDataSO.m_GroundDataInfo);
		CreateCameraLimit (mapDataSO.m_CameraLimitDataInfo, mapInfo);
		CreateCollider (mapDataSO.m_ColliderDataInfo);

		return mapInfo;
	}

	void CreateGround (List<GroundDataInfo> aGroundDataInfo)
	{
		GameObject container = new GameObject ();
		container.name = "GroundContainer";
		container.transform.SetParent (transform, false);

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

	void CreateCameraLimit (List<CameraLimitDataInfo> aCameraLimitDataInfo, MapInfo aMapInfo)
	{
		GameObject container = new GameObject ();
		container.name = "CameraLimitContainer";
		container.transform.SetParent (transform, false);

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

	void CreateCollider (List<ColliderDataInfo> aColliderDataInfo)
	{
		GameObject container = new GameObject ();
		container.name = "ColliderContainer";
		container.transform.SetParent (transform, false);
		
		for (int i = 0; i < aColliderDataInfo.Count; i++) {
			ColliderDataInfo colliderDataInfo = aColliderDataInfo[i];
			
			GameObject colliderGo = new GameObject ();
			colliderGo.name = CAMERA_LIMIT_NAME+i;
			colliderGo.transform.SetParent (container.transform, false);
			
			colliderGo.AddComponent<BoxCollider2D>();
			
			colliderGo.transform.localPosition = colliderDataInfo.m_Position;
			colliderGo.transform.localScale = colliderDataInfo.m_Scale;
			colliderGo.transform.localRotation = colliderDataInfo.m_Rotation;
		}
	}
}
