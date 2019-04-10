using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(200)]
public class AmmoCounter : MonoBehaviour {

    [SerializeField] Weapon weapon;
    [SerializeField] GameObject[] bulletTokens;
    [SerializeField] BulletToken[] bulletTokensSprite;
    [SerializeField] GameObject reloadIcon;
    int ammo;
    // Use this for initialization
    void Start () {
        ammo = weapon.load;
        bulletTokensSprite = new BulletToken[bulletTokens.Length];
        for(int g= 0; g< bulletTokens.Length;++g)
        {
            bulletTokensSprite[g] = bulletTokens[g].GetComponent<BulletToken>();
        }
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (ammo != weapon.load)
        {
            ammo = weapon.load;
            for (int i = 0; i < weapon.capacity; ++i)
            {
                if(i < weapon.load)
                {
                    bulletTokensSprite[i].Show();
                }
                else
                {
                    bulletTokensSprite[i].Hide();
                }
            }
        }
        if (reloadIcon != null) reloadIcon.SetActive(weapon.reloading);
    }
}
