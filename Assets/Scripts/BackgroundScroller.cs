using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 1f;
    Material material;
    Vector2 offSet;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        offSet = new Vector2(0, scrollSpeed);
    }


    void Update()
    {
        material.mainTextureOffset += offSet * Time.deltaTime;
    }
}
