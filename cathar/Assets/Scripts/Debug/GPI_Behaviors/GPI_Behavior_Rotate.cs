using UnityEngine;
using System.Collections;

public class GPI_Behavior_Rotate : MonoBehaviour {

	public float speed;

	void Update () {
		this.transform.Rotate (new Vector3 (0,0,speed));
	}
}
