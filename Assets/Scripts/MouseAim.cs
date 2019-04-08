using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour {

    [SerializeField] LayerMask layer;
    [SerializeField] public Weapon weapon;
    // Use this for initialization
    void Start () {
		
	}
    Vector3 targetPos;
    public Vector3 rot;
    public Vector3 newRot;
    // Update is called once per frame
    void LateUpdate () {
        
	}
    public void AimAt(Vector3 position)
    {
        targetPos = position;
        if (weapon.reloading)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, -25);
        }
        else if (!weapon.firing)
        {
            rot = this.transform.localEulerAngles;
            this.transform.LookAt(position);

            newRot = this.transform.localEulerAngles;
            if (Mathf.Abs(newRot.y - 90) < 1)
            {
                rot = newRot;
                this.transform.localEulerAngles = new Vector3(0, rot.y - 90, -rot.x);

            }
            else
            {
                this.transform.localEulerAngles = rot;
            }
        }
    }
    private void OnDrawGizmos()
    {

        {
            Gizmos.DrawLine(this.transform.position, targetPos);
        }
    }
}
