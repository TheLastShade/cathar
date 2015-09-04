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
		float axisX = Input.GetAxis (ControllerSave.m_HorizontalAxis);
		float axisY = Input.GetAxis(ControllerSave.m_VerticalAxis);

		Vector2 velocity = new Vector2(Mathf.Lerp(0, axisX, 0.8f),
		                               Mathf.Lerp(0, axisY, 0.8f));

		float stickSpeed = Mathf.Max (Mathf.Abs (axisX), Mathf.Abs(axisY));
		m_RigidBody.velocity = velocity.normalized * m_Speed*stickSpeed;
	}
}
