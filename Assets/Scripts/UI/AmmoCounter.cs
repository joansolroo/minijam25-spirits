using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(200)]
public class AmmoCounter : MonoBehaviour {

    [SerializeField] Weapon weapon;
    [SerializeField] GameObject[] bulletTokens;

    [SerializeField] GameObject reloadIcon;
    int ammo;
    // Use this for initialization
    void Start () {
        ammo = weapon.load;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (ammo != weapon.load)
        {
            ammo = weapon.load;
            for (int i = 0; i < weapon.capacity; ++i)
            {
                bulletTokens[i].SetActive(i < weapon.load);
            }

            if (reloadIcon != null) reloadIcon.SetActive(weapon.load == 0);
        }
	}
}
