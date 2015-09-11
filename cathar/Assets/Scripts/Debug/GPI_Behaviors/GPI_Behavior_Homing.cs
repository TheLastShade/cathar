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

			Vector3 diff =  player.transform.position - transform.position;
			diff.Normalize();
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90f);


		}

	}
}
