using UnityEngine;
using System.Collections;

public class PlayerAnimationHandler : MonoBehaviour 
{
	public float m_MagnitureMultiplicator = 1f;
	public float m_MagnitureMin = 0.1f;
	public float m_MagnitureMax = 1f;
	public Animator m_Animator;
	
	private Rigidbody2D m_RigidBody;
	// Use this for initialization
	void Start () 
	{
		m_RigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 velocity = m_RigidBody.velocity.normalized;
		float magnitude = m_RigidBody.velocity.magnitude;
		bool isWalking = magnitude > 0f;
		if (isWalking) 
		{
			m_Animator.SetFloat ("x", velocity.x);
			m_Animator.SetFloat ("y", velocity.y);
		}

		float walkingSpeed = magnitude * m_MagnitureMultiplicator;
		walkingSpeed = Mathf.Min (m_MagnitureMax, walkingSpeed);
		walkingSpeed = Mathf.Max (m_MagnitureMin, walkingSpeed);
		m_Animator.SetFloat ("walkingSpeed", walkingSpeed);
		m_Animator.SetBool ("isWalking", isWalking);
	}
}
