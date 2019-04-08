using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class Follow : MonoBehaviour {

    [SerializeField] public Transform target;
    // Update is called once per frame
	void LateUpdate () {
        this.transform.position = target.position;
	}
}
