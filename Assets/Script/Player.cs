using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer player_sprite_rend;

    public enum PlayerState
    {
        Run,    // 달리기
        Jump,   // 점프
    }
    
    private PlayerState playerState;

    // 플레이어의 체력, 점수
    public int health, score;

    // 플레이어의 최댓값, 최솟값
    public float player_min_x, player_max_x, player_min_y, player_max_y;

    // 중력 가속도 (현실 중력 가속도: 9.8f), 점프력, 점프 이후 경과시간, 오브젝트의 초기 높이
    private float g_acceleration, jumpPower, jumpTime, InitHeight_Y;

    void Start()
    {
        player_sprite_rend = GetComponent<SpriteRenderer>();

        InitHeight_Y = transform.position.y;
        g_acceleration = 60.0f; 
        jumpPower = 25.0f;
        jumpTime = 0.0f;

        health = 1;
        score = 0;

        playerState = PlayerState.Run;
    }

    void Update()
    {
        Vector2 playerSize = player_sprite_rend.bounds.size;
        Vector2 playerPosition = transform.position;

        switch (playerState)
        {
            case PlayerState.Run:
                if (Input.GetKeyDown(KeyCode.Space)) playerState = PlayerState.Jump;
                break;
            case PlayerState.Jump:
                Jump();
                break;
        }

        // Collider
        player_min_x = playerPosition.x - (playerSize.x / 2);
        player_max_x = playerPosition.x + (playerSize.x / 2);
        player_min_y = playerPosition.y - (playerSize.y / 2);
        player_max_y = playerPosition.y + (playerSize.y / 2);
    }

   void Jump()
   {
        float height = (jumpTime * jumpTime * (-g_acceleration) / 2) + (jumpTime * jumpPower);
        transform.position = new Vector3(transform.position.x, InitHeight_Y + height, transform.position.z);

        jumpTime += Time.deltaTime;

        if (height < 0.0f)
        {
            jumpTime = 0.0f;
            transform.position = new Vector3(transform.position.x, InitHeight_Y, transform.position.z);

            playerState = PlayerState.Run;
        }
    } 
}
