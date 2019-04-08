using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
[DefaultExecutionOrder(-200)]
public class PlayerController : MonoBehaviour {

    [SerializeField] CharacterController2D controller;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");
        if (moveVertical > 0)
        {
            moveVertical = moveVertical / Mathf.Max(1, transform.position.y*0.25f);
        }

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        controller.Move(movement);
    }

    private void LateUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10))
        {
            controller.AimAt(hit.point);
        }
        if (Input.GetMouseButtonDown(0))
        {
            controller.Fire1();
        }
        if (Input.GetMouseButtonDown(1))
        {
            controller.Fire2();
        }
    }
}
