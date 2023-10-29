using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animatorControllers;
    public Rigidbody2D target;
    
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
        Vector2 dirVector = target.position - rigidBody.position;
        spriteRenderer.flipX = dirVector.x < 0;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        collid.enabled = true;
        rigidBody.simulated = true;
        spriteRenderer.sortingOrder = 2;
        animator.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData spawnData)
    {
        animator.runtimeAnimatorController = animatorControllers[spawnData.spriteType];
        speed = spawnData.speed;
        maxHealth = spawnData.health;
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isLive)
            return;
        if(!collision.CompareTag("Bullet"))
            return;
        
        
        float receiveDamage= collision.GetComponent<Bullet>().damage;
        health -= receiveDamage;
        StartCoroutine(KnockBack());

        // health -= collision.GetComponent<Bullet6Explosion>().damage;

        if(FloatingTextPrefab){
            ShowFloatingText(receiveDamage);
        } 

        if(health <= 0){
            Dead();
        }else{
            animator.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
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
