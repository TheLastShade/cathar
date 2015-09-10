using UnityEngine;
using System.Collections;
using System;

public class ColliderExporter : BaseExporter {
	
	public string m_ColliderMapLayer;

	public ColliderDataInfo ToColliderDataInfo()
	{
		ColliderDataInfo data = new ColliderDataInfo ();
		data.m_Layer = gameObject.layer;
		data.m_Position = transform.localPosition;
		data.m_Scale = transform.localScale;
		data.m_Rotation = transform.rotation;
		data.m_ColliderMapLayer = m_ColliderMapLayer;
		
		return data;
	}

}

[Serializable]
public struct ColliderDataInfo
{
	public Vector3 m_Position;
	public Vector3 m_Scale;
	public Quaternion m_Rotation;
	public int m_Layer;
	public string m_ColliderMapLayer;
}