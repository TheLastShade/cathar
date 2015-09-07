using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MapDataSO : ScriptableObject 
{
	public List<GroundDataInfo> m_GroundDataInfo;

	public List<CameraLimitDataInfo> m_CameraLimitDataInfo;

	public List<ColliderDataInfo> m_ColliderDataInfo;

	public void ClearData ()
	{
		m_GroundDataInfo = new List<GroundDataInfo>();
		m_CameraLimitDataInfo = new List<CameraLimitDataInfo>();
		m_ColliderDataInfo = new List<ColliderDataInfo>();
	}
}
