using UnityEngine;
using System.Collections;

public class Follow2DTransform : MonoBehaviour {

	public Transform m_ToFollow;

	public Transform m_TopLeftLimit;
	public Transform m_BottomRightLimit;

	private Transform m_MyTransform;
	private Camera m_Camera;


	// Use this for initialization
	void Start () {
		m_MyTransform = gameObject.transform;
		m_Camera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		float tempX = m_ToFollow.position.x;
		float tempY = m_ToFollow.position.y;

		float height = m_Camera.orthographicSize;
		float width = height * m_Camera.aspect;

		if (m_TopLeftLimit != null) {
			tempX = Mathf.Max (tempX, (m_TopLeftLimit.position.x+width));
			tempY = Mathf.Min (tempY, (m_TopLeftLimit.position.y-height));
		}

		if (m_BottomRightLimit != null) {
			tempX = Mathf.Min (tempX, (m_BottomRightLimit.position.x-width));
			tempY = Mathf.Max (tempY, (m_BottomRightLimit.position.y+height));
		}

		Vector2 newPosition = new Vector2 (tempX, tempY);
		m_MyTransform.position = newPosition;
	}
}
