using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public float scrollspeed;
    float targetOffset;

    Renderer m_Renderer;

    private Player player;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    { 
        targetOffset += Time.deltaTime * scrollspeed;
        m_Renderer.material.mainTextureOffset = new Vector2(targetOffset, 0);

        scrollspeed += 0.01f * Time.deltaTime;
    }
}
