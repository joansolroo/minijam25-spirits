using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class Follow : MonoBehaviour {

    [SerializeField] public CharacterController2D target;
    // Update is called once per frame
	void LateUpdate () {
        this.transform.position = target.transform.position + new Vector3(target.aimAt.x, target.aimAt.y);
	}
}
