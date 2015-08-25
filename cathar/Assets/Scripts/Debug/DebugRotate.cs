using UnityEngine;
using System.Collections;

public class DebugRotate : MonoBehaviour {

	public Vector3 m_RotateToApply;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (m_RotateToApply);
	}
}
