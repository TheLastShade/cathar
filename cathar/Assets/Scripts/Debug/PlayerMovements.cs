using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour {

	public float m_Speed;
	private Rigidbody2D m_RigidBody;
	// Use this for initialization
	void Start () {
		m_RigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Vector2 velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal"), 0.8f),
		                                   Mathf.Lerp(0, Input.GetAxis("Vertical"), 0.8f));

		m_RigidBody.velocity = velocity.normalized * m_Speed;
	}
}
