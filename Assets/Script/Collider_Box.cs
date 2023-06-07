using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Box : MonoBehaviour
{
    ObjectPool objectPool;
    Player player;

    private SpriteRenderer obstacle_sprite_rend;

    public float speed = 10.0f; // 이동 속도

    private float posX, posY, p_posY; // 현재 위치 저장
    private float min_x, max_x, min_y, max_y; // 장애물의 최댓값, 최솟값

    void Start()
    {
        posX = transform.position.x;
        posY = transform.position.y;
        p_posY = 1;

        objectPool = GameObject.Find("ObstacleManager").GetComponent<ObjectPool>();
        obstacle_sprite_rend = GetComponent<SpriteRenderer>();

        player = GameObject.FindObjectOfType<Player>();
    }

    void Update()
    {
        // 이동
        if(this.gameObject.tag == "Bush") transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (this.gameObject.tag == "Pteranodon")
        {
            transform.Translate(new Vector2(-1, p_posY).normalized * speed * Time.deltaTime);

            if (transform.position.y >= 3.0f) p_posY = -1;
            else if (transform.position.y <= 0) p_posY = 1;
            
        }


        // 삭제
        if (this.gameObject.transform.position.x <= -10.0f)
        {
            objectPool.objectPool.Add(this.gameObject);
            transform.position = new Vector2(posX, posY);
            player.score += 1;

            this.gameObject.SetActive(false);
        }

        // 충돌 감지
        Collision_Detection();
    }

    void Collision_Detection()
    {
        Vector2 obstacleSize = obstacle_sprite_rend.bounds.size;
        Vector2 obstaclePosition = transform.position;

        min_x = obstaclePosition.x - (obstacleSize.x / 2);
        max_x = obstaclePosition.x + (obstacleSize.x / 2);
        min_y = obstaclePosition.y - (obstacleSize.y / 2);
        max_y = obstaclePosition.y + (obstacleSize.y / 2);

        if (player.player_max_x > min_x && player.player_min_x < max_x && player.player_max_y > min_y && player.player_min_y < max_y) player.health -= 1;
    }
}
