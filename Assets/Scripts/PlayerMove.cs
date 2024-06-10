using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float MaxSpeed;
    public float JumpPower;
    public Transform respawnPoint; // 부활 지점

    public FadeInEffect fadeEffect; // FadeInEffect 스크립트 참조
    private float fadeTime; // FadeInEffect의 fadeTime 변수를 저장하기 위한 변수

    private bool IsJumping;
    private Rigidbody2D PlayerRigid;

    public static bool Getitem { get; private set; }  // 아이템 획득 상태
    public static bool Finish { get; private set; }   // 목표 도착 상태

    private void Awake()
    {
        PlayerRigid = GetComponent<Rigidbody2D>();
        IsJumping = false;
        Getitem = false;  // 초기화
        Finish = false;   // 초기화
    }

    private void Start()
    {
        // FadeInEffect에서 fadeTime 변수 가져오기
        fadeTime = fadeEffect.fadeTime;

        // 부활 지점이 할당되지 않았을 경우, 플레이어의 현재 위치를 부활 지점으로 설정
        if (respawnPoint == null)
        {
            respawnPoint = transform;
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        PlayerRigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (PlayerRigid.velocity.x > MaxSpeed)
            PlayerRigid.velocity = new Vector2(MaxSpeed, PlayerRigid.velocity.y);
        else if (PlayerRigid.velocity.x < MaxSpeed * (-1))
            PlayerRigid.velocity = new Vector2(MaxSpeed * (-1), PlayerRigid.velocity.y);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !IsJumping)
        {
            PlayerRigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            IsJumping = true;
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            PlayerRigid.velocity = new Vector2(PlayerRigid.velocity.normalized.x * 0.5f, PlayerRigid.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            Getitem = true;
        }
        else if (collision.gameObject.tag == "Finish")
        {
            Finish = true;
        }
        else if (collision.gameObject.tag == "Water")
        {
            Debug.Log("플레이어가 물에 닿았음");
            StartCoroutine(HandlePlayerDeath());
        }
    }

    private IEnumerator HandlePlayerDeath()
    {
        Debug.Log("페이드 아웃 시작");
        // 페이드 아웃 시작
        fadeEffect.OnFade(FadeState.FadeOut);

        // 페이드 아웃이 완료될 때까지 대기
        yield return new WaitForSeconds(fadeTime);

        // 플레이어 위치를 부활 지점으로 설정
        transform.position = respawnPoint.position;
        PlayerRigid.velocity = Vector2.zero;

        Debug.Log("페이드 인 시작");
        // 페이드 인 시작
        fadeEffect.OnFade(FadeState.FadeIn);
    }

}



