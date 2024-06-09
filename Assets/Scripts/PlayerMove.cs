using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float MaxSpeed;
    public float JumpPower;
    public Transform respawnPoint; // ��Ȱ ����

    public FadeInEffect fadeEffect; // FadeInEffect ��ũ��Ʈ ����
    private float fadeTime; // FadeInEffect�� fadeTime ������ �����ϱ� ���� ����

    private bool IsJumping;
    private Rigidbody2D PlayerRigid;

    public static bool Getitem { get; private set; }  // ������ ȹ�� ����
    public static bool Finish { get; private set; }   // ��ǥ ���� ����

    private void Awake()
    {
        PlayerRigid = GetComponent<Rigidbody2D>();
        IsJumping = false;
        Getitem = false;  // �ʱ�ȭ
        Finish = false;   // �ʱ�ȭ
    }

    private void Start()
    {
        // FadeInEffect���� fadeTime ���� ��������
        fadeTime = fadeEffect.fadeTime;

        // ��Ȱ ������ �Ҵ���� �ʾ��� ���, �÷��̾��� ���� ��ġ�� ��Ȱ �������� ����
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
            Debug.Log("�÷��̾ ���� �����");
            StartCoroutine(HandlePlayerDeath());
        }
    }

    private IEnumerator HandlePlayerDeath()
    {
        Debug.Log("���̵� �ƿ� ����");
        // ���̵� �ƿ� ����
        fadeEffect.OnFade(FadeState.FadeOut);

        // ���̵� �ƿ��� �Ϸ�� ������ ���
        yield return new WaitForSeconds(fadeTime);

        // �÷��̾� ��ġ�� ��Ȱ �������� ����
        transform.position = respawnPoint.position;
        PlayerRigid.velocity = Vector2.zero;

        Debug.Log("���̵� �� ����");
        // ���̵� �� ����
        fadeEffect.OnFade(FadeState.FadeIn);
    }

}



