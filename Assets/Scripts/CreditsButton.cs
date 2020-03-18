using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreditsButton : MonoBehaviour
{


    public Sprite normal;
    public Sprite pressed;

    private SpriteRenderer sr;

    public GameObject credits;

    public bool mouseOn = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

    }

    void OnMouseOver()
    {
        mouseOn = true;
    }

    void OnMouseExit()
    {
        mouseOn = false;
    }

    void OnMouseDown()
    {
        if (mouseOn)
        {
            sr.sprite = pressed;
            credits.SetActive(true);
        }
    }

    void OnMouseUp()
    {
        sr.sprite = normal;
    }
}
