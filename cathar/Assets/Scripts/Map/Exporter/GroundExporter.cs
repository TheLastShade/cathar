using UnityEngine;
using System.Collections;
using System;

public class GroundExporter : BaseExporter {

	private const float TILE_SIZE = 64f;
	private const float RELATIVE_SIZE = 100f;
	private const float FORCE_SCALE = 1.012f;

	public void SnapGround ()
	{
		float newPositionX = Mathf.Round(transform.position.x/TILE_SIZE*RELATIVE_SIZE)*TILE_SIZE/RELATIVE_SIZE;
		float newPositionY = Mathf.Round(transform.position.y/TILE_SIZE*RELATIVE_SIZE)*TILE_SIZE/RELATIVE_SIZE;

		transform.localPosition = new Vector3 (newPositionX, newPositionY, 0);
		transform.localScale = new Vector3 (FORCE_SCALE, FORCE_SCALE, 1f);
	}

	public GroundDataInfo ToGroundDataInfo()
	{
		GroundDataInfo data = new GroundDataInfo ();
		data.m_Sprite = GetComponent<SpriteRenderer> ().sprite;
		data.m_Position = transform.localPosition;
		data.m_Scale = transform.localScale;
		data.m_Rotation = transform.localRotation;

		return data;
	}
}

[Serializable]
public struct GroundDataInfo
{
	public Vector3 m_Position;
	public Vector3 m_Scale;
	public Quaternion m_Rotation;
	public Sprite m_Sprite;
}