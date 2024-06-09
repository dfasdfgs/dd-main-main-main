using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System.Diagnostics;

public enum FadeState { FadeIn = 0, FadeOut, FadeInOut, FadeLoop }
public class FadeInEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    public float fadeTime; // fadeSpeed 값이 10이면 1초 (값이 클수록 빠름)

    [SerializeField]
    private AnimationCurve fadeCurve; // 페이드 효과가 적용되는 알파 값을 곡선의 값으로 설정
    private Image image;  //페이드 효과에 사용되는 검은 바탕 이미지
    private FadeState fadeState; // 페이드 효과 상태

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnFade(FadeState State)
    {
        fadeState = State;

        switch (fadeState)
        {
            case FadeState.FadeIn: // Fade In. 배경의 알파값이 1에서 0으로 (화면이 점점 밝아진다)
                StartCoroutine(Fade(1, 0));
                break;
            case FadeState.FadeOut: //Fade Out. 배경의 알파값이 0에서1로 (화면이 점점 어두워진다)
                StartCoroutine(Fade(0, 1));
                break;
            case FadeState.FadeInOut: //Fade 효과를 In -> Out 1회 반복한다
            case FadeState.FadeLoop: //Fade 효과를 In -> Out 무한 반복한다
                StartCoroutine(FadeInOut());
                break;
        }
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            // 코루틴 내부에서 코루틴 함수를 호출하면 해당 코루틴 함수가 종료되어야 다음 문장 실행
            yield return StartCoroutine(Fade(1, 0));  // Fade In

            yield return StartCoroutine(Fade(0, 1));  //Fade Out

            //1회만 재생하는 상태일 때 break;
            if (fadeState == FadeState.FadeInOut)
            {
                break;
            }
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            // fadeTime으로 나누어서 fadeTime 시간 동안
            // percent 값이 0에서 1로 증가하도록 함
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // 알파값을 start부터 end까지 fadeTime 시간 동안 변화시킨다
            Color color = image.color;
            //color.a = Mathf.Lerp(start,end, percent);
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color;

            yield return null;
        }
    }

}
