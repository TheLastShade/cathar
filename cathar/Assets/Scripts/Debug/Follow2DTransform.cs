using UnityEngine;
using System.Collections;

public class Follow2DTransform : MonoBehaviour {

	public Transform m_ToFollow;
	private Transform m_MyTransform;
	// Use this for initialization
	void Start () {
		m_MyTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 newPosition = new Vector2 (m_ToFollow.position.x, m_ToFollow.position.y);
		m_MyTransform.position = newPosition;
	}
}
