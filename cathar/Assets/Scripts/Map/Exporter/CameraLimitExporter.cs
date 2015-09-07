using UnityEngine;
using System.Collections;
using System;

public class CameraLimitExporter : BaseExporter {
	public CameraLimitDataInfo m_CameraLimitDataInfo;
}


[Serializable]
public struct CameraLimitDataInfo
{
	public CameraLimitType m_Type;
}

public enum CameraLimitType
{
	TOP_LEFT,
	BOTTOM_RIGHT
}