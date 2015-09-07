using UnityEngine;
using System.Collections;
using System;

public class CameraLimitExporter : BaseExporter {
	public CameraLimitType m_CameraLimitType;

	public CameraLimitDataInfo ToCameraLimitDataInfo()
	{
		CameraLimitDataInfo cameraLimitDataInfo = new CameraLimitDataInfo ();

		cameraLimitDataInfo.m_Type = m_CameraLimitType;
		cameraLimitDataInfo.m_Position = transform.localPosition;

		return cameraLimitDataInfo;
	}
}


[Serializable]
public struct CameraLimitDataInfo
{
	public CameraLimitType m_Type;
	public Vector3 m_Position;
}

public enum CameraLimitType
{
	TOP_LEFT,
	BOTTOM_RIGHT
}