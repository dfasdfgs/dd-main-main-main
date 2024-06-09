using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTransition : MonoBehaviour
{
    // 다음 스테이지의 씬 인덱스
    public int nextStageIndex;

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거에 진입했을 때
        if (other.CompareTag("Player"))
        {
            // 다음 스테이지로 씬 인덱스를 이용하여 전환
            SceneManager.LoadScene(nextStageIndex);
        }
    }
}
