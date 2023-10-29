using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inferno : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite fireSprite;
    public float fireSpeed = 5f;
    public float spawnRange = 1f;
    public Vector2 fireSizeRange = new Vector2(0.5f, 1.5f); // 불꽃의 최소 및 최대 크기
    Vector2 direction;

    // private void Start()
    // {
    //     PlayerDirection = FindObjectOfType<PlayerDirectionAngle>();
    // }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            direction = PlayerDirectionAngle.instance.InputAngleVector2;
            SpawnFire(direction);
        }
    }

    void SpawnFire(Vector2 direction)
    {
        GameObject fire = new GameObject("Fire");
        SpriteRenderer sr = fire.AddComponent<SpriteRenderer>();
        sr.sprite = fireSprite;

        // 노란색에서 붉은색까지의 랜덤 색상 설정
        sr.color = new Color(1, Random.Range(0.5f, 1f), 0);

        fire.transform.position = transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);

        // 불꽃의 크기 랜덤 설정
        float randomScale = Random.Range(fireSizeRange.x, fireSizeRange.y);
        fire.transform.localScale = new Vector3(randomScale, randomScale, 1);

        Rigidbody2D rb = fire.AddComponent<Rigidbody2D>();
        rb.velocity = direction * fireSpeed;

        Destroy(fire, 2f);
    }

}
