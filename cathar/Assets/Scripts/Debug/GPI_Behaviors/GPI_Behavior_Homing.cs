using UnityEngine;
using System.Collections;

public class GPI_Behavior_Homing : MonoBehaviour {

	public float moveSpeed;
	public float rotateSpeed;
	private GameObject player;

	
	// Update is called once per frame
	void Update () {
	
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		} else {

			Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position, transform.TransformDirection(Vector3.up));
			transform.rotation = new Quaternion (0,0,rotation.z, rotation.w);

		}

	}
}
