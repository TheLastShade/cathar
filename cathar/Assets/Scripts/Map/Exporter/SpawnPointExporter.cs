using UnityEngine;
using System.Collections;
using System;

public class SpawnPointExporter : BaseExporter {

	public string m_SpawnName;
	public bool m_IsDefault;

	public SpawnPointDataInfo ToSpawnPointDataInfo()
	{
		SpawnPointDataInfo data = new SpawnPointDataInfo ();
		data.m_Position = transform.localPosition;
		data.m_SpawnName = m_SpawnName;
		data.m_IsDefault = m_IsDefault;
		
		return data;
	}
}

[Serializable]
public struct SpawnPointDataInfo
{
	public Vector3 m_Position;
	public string m_SpawnName;
	public bool m_IsDefault;
}