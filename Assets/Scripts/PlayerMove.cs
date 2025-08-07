using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public static PlayerMove instance;    //
    public static int Questnumber = 1; //임의적인

    public float maxSpeed;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
     void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject); //

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Move By Key Control
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.linearVelocity.x > maxSpeed) //오른쪽 최대 스피드
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);

        if (rigid.linearVelocity.x < (-1) * maxSpeed) // 왼쪽 최대 스피드
            rigid.linearVelocity = new Vector2((-1) * maxSpeed, rigid.linearVelocity.y);


    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {



}

// Update is called once per frame
public void Update()
    {

        //stop speed 
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.linearVelocity =
                new Vector2(rigid.linearVelocity.normalized.x * 0.5f, rigid.linearVelocity.y);
        }

        //방향 sprite 



        //버튼이 동시에 눌렸을 때 방향 안바뀌는 오류 수정
        if (Input.GetKeyUp(KeyCode.A) && Input.GetKey(KeyCode.D))
            spriteRenderer.flipX = true;

        else if (Input.GetKeyUp(KeyCode.D) && Input.GetKey(KeyCode.A))
            spriteRenderer.flipX = false;

        // 평소 눌렀을 때도 방향 반영
        else if (Input.GetKeyDown(KeyCode.A))
            spriteRenderer.flipX = false;

        else if (Input.GetKeyDown(KeyCode.D))
            spriteRenderer.flipX = true;


        //Animation
        if (Mathf.Abs(rigid.linearVelocity.normalized.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);


    }


}





