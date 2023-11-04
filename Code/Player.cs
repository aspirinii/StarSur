using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class Player : MonoBehaviour
{
    static public Player instance;
    public Vector2 inputVec;
    public float moveSpeed = 3f;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animatorController;
    
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public GameObject FloatingTextPrefab;


    void Awake()
    {
        instance = this;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true); //Inactive 도 가져오려면 ture 
    }

    void OnEnable()
    {
        moveSpeed = CharacterStatus.MoveSpeed * 3f;
        animator.runtimeAnimatorController = animatorController[GameManager.instance.playerId];
    }

    void Update()
    {
        // // 아래는 InputSystemPackage 미사용 예시  -> OnMove() 사용
        // inputVec.x = Input.GetAxisRaw("Horizontal");// 미끄러지지않고 Raw 
        // inputVec.y = Input.GetAxisRaw("Vertical");
        // inputVec.x = Input.GetAxis("Horizontal");// 미끄러지듯이 
    }

    void FixedUpdate()
    {
        if(!GameManager.instance.isLive)
            return;
        inputVec.x = Input.GetAxisRaw("Horizontal");// 미끄러지지않고 Raw 
        inputVec.y = Input.GetAxisRaw("Vertical");
        Vector2 moveVec = inputVec * moveSpeed * Time.fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + moveVec);

    }

    // InputSystem Package 사용`
    void OnMove(InputValue value){
        if(!GameManager.instance.isLive)
            return;
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);
        if(inputVec.x != 0) {
            spriteRenderer.flipX = inputVec.x < 0;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(!GameManager.instance.isLive)
            return;
        // check other is enemy 
        if(other == null || other.transform.tag != "Enemy")
            return;
        int receiveDamage = other.gameObject.GetComponent<Enemy>().damage;
        GameManager.instance.health -= Time.deltaTime * receiveDamage; // 프레임당 데미지 들어감.. 초당 ReceiveDamage 들어감
        
        // FloatingText 생성
        if(FloatingTextPrefab){
            ShowFloatingText( Time.deltaTime * receiveDamage);
        }  
        
        if(GameManager.instance.health <= 0){
            for(int index=2; index<transform.childCount; index++){
                transform.GetChild(index).gameObject.SetActive(false);
            }
            animator.SetTrigger("Dead");
            // GameManager.instance.isLive = false;
            GameManager.instance.GameOver();
        }
    }

    protected void ShowFloatingText(float damage){
        Vector3 textPosition = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f),UnityEngine.Random.Range(-0.2f, 0.2f),0 );
        GameObject floatingText = Instantiate(FloatingTextPrefab, transform.position + textPosition, Quaternion.identity, transform);

        TextMeshPro textMeshPro = floatingText.GetComponent<TextMeshPro>();
        textMeshPro.text = damage.ToString();
        textMeshPro.color = Color.red;  // Set the color to red

        Destroy(floatingText, 1);
    }


}
