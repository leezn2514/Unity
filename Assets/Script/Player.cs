using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Stop,   // ����, �Ͻ�����
        Run,    // �޸���
        Jump,   // ����
        Die     // ����
    }
    
    private PlayerState playerState;

    private float g_acceleration;   // �߷� ���ӵ� (���� �߷� ���ӵ�: 9.8f)
    private float jumpPower;        // ������
    private float jumpTime;         // ���� ���� ����ð�
    private float InitHeight_Y;     //������Ʈ�� �ʱ� ����

    public int health;
    public int score;

    private SpriteRenderer player_sprite_rend;

    public float player_min_x, player_max_x, player_min_y, player_max_y; // �÷��̾��� �ִ�, �ּڰ�

    void Start()
    {
        player_sprite_rend = GetComponent<SpriteRenderer>();

        InitHeight_Y = transform.position.y;
        g_acceleration = 60.0f; 
        jumpPower = 25.0f;
        jumpTime = 0.0f;

        playerState = PlayerState.Run;

        health = 1;
        score = 0;
    }

    void Update()
    {
        Vector2 playerSize = player_sprite_rend.bounds.size;
        Vector2 playerPosition = transform.position;

        switch (playerState)
        {
            case PlayerState.Stop:
                break;
            case PlayerState.Run:
                if (Input.GetKeyDown(KeyCode.Space)) playerState = PlayerState.Jump;
                break;
            case PlayerState.Jump:
                Jump();
                break;
            case PlayerState.Die:
                break;
        }

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
