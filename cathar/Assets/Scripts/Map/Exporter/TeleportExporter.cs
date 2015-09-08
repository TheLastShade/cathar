using UnityEngine;
using System.Collections;
using System;

public class TeleportExporter : BaseExporter {
	
	public string m_MapToTeleport;
	public string m_SpawnPointToTeleport;

	public TeleportDataInfo ToTeleportDataInfo()
	{
		TeleportDataInfo data = new TeleportDataInfo ();
		data.m_Layer = gameObject.layer;
		data.m_Position = transform.localPosition;
		data.m_Scale = transform.localScale;
		data.m_Rotation = transform.rotation;

		data.m_MapToTeleport = m_MapToTeleport;
		data.m_SpawnPointToTeleport = m_SpawnPointToTeleport;
		
		return data;
	}
}

[Serializable]
public struct TeleportDataInfo
{
	public Vector3 m_Position;
	public Vector3 m_Scale;
	public Quaternion m_Rotation;
	public int m_Layer;
	public string m_MapToTeleport;
	public string m_SpawnPointToTeleport;
}