using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(200)]
public class AmmoPanel : MonoBehaviour {

    [SerializeField] CharacterController2D player;
    [SerializeField] GameObject pistol1Ammo;
    [SerializeField] GameObject pistol2Ammo;
    [SerializeField] GameObject shotgunAmmo;
    [SerializeField] GameObject rifleAmmo;

    int weapon = -1;
    void Start()
    {
        weapon = player.currentWeapon;
        UpdateUI();
    }
        
    private void LateUpdate()
    {
        if(weapon!= player.currentWeapon)
        {
            weapon = player.currentWeapon;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        pistol1Ammo.SetActive(weapon == 0 || weapon == 1);
        pistol2Ammo.SetActive(weapon == 1);
        shotgunAmmo.SetActive(weapon == 2);
        rifleAmmo.SetActive(weapon == 3);
    }
}

