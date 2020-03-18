using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{


    public Sprite normal;
    public Sprite pressed;

    private SpriteRenderer sr;

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
            SceneManager.LoadScene("MainScene");
        }
    }

    void OnMouseUp()
    {
        sr.sprite = normal;
    }
}