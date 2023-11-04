using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public RuntimeAnimatorController[] animatorControllers;
    [HideInInspector]
    public float speed;
    public int health;
    public int maxHealth;
    public int damage;
    public Rigidbody2D target;
    //when enemy enable, target will set player
    bool isLive;

    Animator animator;
    Rigidbody2D rigidBody;
    Collider2D collid;
    SpriteRenderer spriteRenderer;
    WaitForFixedUpdate wait;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collid = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();      
    }
    private void FixedUpdate()
    {
        if(!GameManager.instance.isLive)
            return;
        if(!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        //이동함수 기능 수행 
        Vector2 dirVector = target.position - rigidBody.position;
        Vector2 moveVector = dirVector.normalized * speed * Time.fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + moveVector);
        rigidBody.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        if(!GameManager.instance.isLive)
            return;
        if(!isLive)
            return;
        //스프라이트 방향전환
        Vector2 dirVector = target.position - rigidBody.position;
        spriteRenderer.flipX = dirVector.x < 0;
    }

    public void Init(MonsterData monsterData)
    {
        // 스폰데이터 초기화 
        animator.runtimeAnimatorController = animatorControllers[monsterData.spriteType];
        speed = monsterData.speed;
        maxHealth = monsterData.health;
        health = maxHealth;
        damage = monsterData.damage;
        
    }
    private void OnEnable()
    {
        //각종 초기화 수행 
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        collid.enabled = true;
        rigidBody.simulated = true;
        spriteRenderer.sortingOrder = 2;
        animator.SetBool("Dead", false);
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isLive)
            return;
        if(!collision.CompareTag("Bullet"))
            return;
        
        // Bullet damage 받기
        int receiveDamage = (int)collision.GetComponent<Bullet>().damage;
        health -= receiveDamage;
        StartCoroutine(KnockBack());


        // FloatingText 생성
        if(FloatingTextPrefab){
            ShowFloatingText(receiveDamage);
        } 

        // sfx 재생
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);

        // 죽음 판정
        if(health <= 0){
            Dead();
        }else{
            animator.SetTrigger("Hit");
        }
    }
    protected void ShowFloatingText(float damage){

        Vector3 textPosition = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f),UnityEngine.Random.Range(-0.2f, 0.2f),0 );
        GameObject floatingText = Instantiate(FloatingTextPrefab, transform.position + textPosition, Quaternion.identity, transform);

        // Rect Transform 때문에 변경 안되는 듯 ... 
        // TextPMeshPro가 일반 transform 에 잘동작하는지
        // 애니메이션 새로 만들어야 하는지;

        floatingText.GetComponent<TextMeshPro>().text = damage.ToString();
        Destroy(floatingText, 1);
    }

    IEnumerator KnockBack()
    {
        yield return wait; // 하나의 물리 프레임 쉬기
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVector = transform.position - playerPos;
        rigidBody.AddForce(dirVector.normalized * 3f, ForceMode2D.Impulse);

        // yield return new WaitForSeconds(0.1f); // 0.1초 쉬기
        // yield return null; // 하나의 프레임 쉬기 
    }

    void Dead()
    {
        isLive = false;
        collid.enabled = false;
        rigidBody.simulated = false;
        spriteRenderer.sortingOrder = 1;
        animator.SetBool("Dead", true);;
        GameManager.instance.kill++;
        GameManager.instance.GetExp();
        if(GameManager.instance.isLive)
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
        // gameObject.SetActive(false);  -> Animator 에서 Dead 애니메이션 끝나면 비활성화
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
