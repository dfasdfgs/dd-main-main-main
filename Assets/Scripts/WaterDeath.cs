using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaterDeath : MonoBehaviour
{
    public Transform respawnPoint; // 부활 지점
    public FadeInEffect fadeEffect; // 페이드 효과

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어 죽음 처리
            PlayerDie(other.gameObject);
        }
    }

    void PlayerDie(GameObject player)
    {
        // 플레이어 위치 초기화
        player.transform.position = respawnPoint.position;
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero;
        }

        // 페이드 아웃 효과 적용
        if (fadeEffect != null)
        {
            StartCoroutine(HandlePlayerRespawn());
            SceneLoad();
        }
        
    }

    private IEnumerator HandlePlayerRespawn()
    {
        // 페이드 아웃 효과 시작
        fadeEffect.OnFade(FadeState.FadeOut);

        // 페이드 아웃이 완료될 때까지 대기
        yield return new WaitForSeconds(fadeEffect.fadeTime);

        // 플레이어가 부활할 때 페이드 인 효과 적용
        if (fadeEffect != null)
        {
            // 페이드 인 효과 시작
            fadeEffect.OnFade(FadeState.FadeIn);

            // 페이드 인이 완료될 때까지 대기
            yield return new WaitForSeconds(fadeEffect.fadeTime);
        }
    }
            

    private static void SceneLoad()
    {
        SceneManager.LoadScene("Stage");
        Debug.Log("Stage");
    }

}

