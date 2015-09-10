using UnityEngine;
using System.Collections;

public class GPI_Behavior_Homing : MonoBehaviour {

	public float moveSpeed;
	public float rotateSpeed;
	private GameObject player;

	
	void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		} else {
			Vector2 dir = transform.position;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		}

	}
}
