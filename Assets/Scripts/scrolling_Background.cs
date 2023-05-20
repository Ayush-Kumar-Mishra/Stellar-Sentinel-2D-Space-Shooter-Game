using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrolling_Background : MonoBehaviour
{
    public MeshRenderer mr;
    public float speed;

    void Update()
    {
        mr.material.mainTextureOffset = new Vector2(0, speed * Time.time);
    }
}
