﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BulletToken : MonoBehaviour {

    [SerializeField] Image token;
    bool visible = true;
    
	// Use this for initialization
	void Start () {
        token = GetComponent<Image>();
	}

    public void Show()
    {
        if(!visible)
        {
            StartCoroutine(DoShow());
            visible = true;
        }
    }
    public void Hide()
    {
        if (visible)
        {
            StartCoroutine(DoHide());
            visible = false;
        }
    }

    static Color hideColor = new Color(1, 0, 0, 0);
    IEnumerator DoShow()
    {
        token.enabled = true;

        for (float t = 0; t < 1; t += Time.deltaTime * 10)
        {
            token.color = Color.Lerp(hideColor, Color.white, t);
            yield return new WaitForEndOfFrame();
        }
        token.color = Color.white;
    }
    IEnumerator DoHide()
    {
        for (float t = 0; t < 1; t += Time.deltaTime * 20)
        {
            token.color = Color.Lerp(Color.white, Color.red, t);
            yield return new WaitForEndOfFrame();
        }
        for (float t = 0; t < 1; t += Time.deltaTime * 10) {
            token.color = Color.Lerp(Color.red, hideColor, t);
            yield return new WaitForEndOfFrame();
        }
        token.color = hideColor;
        token.enabled = false;
    }
}
