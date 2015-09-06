using UnityEngine;
using System.Collections;
using System;

public class PlayerAction : MonoBehaviour {

	bool isActioning = false;

	void Update () {
		float action = Input.GetAxis (ControllerSave.m_ActionButton);

		if (action > 0) {
			this.isActioning = true;
		} else {
			this.isActioning = false;
		}
	}

	public bool getIsActioning(){
		return isActioning;
	}
}
