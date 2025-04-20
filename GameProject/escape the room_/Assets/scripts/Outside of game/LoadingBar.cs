using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    public static string MainScene;

    [SerializeField] private Image progressBar;

    void Start()
    {
        if (string.IsNullOrEmpty(MainScene))
        {
            Debug.LogError("[LoadingBar] MainScene 값이 비어 있습니다. LoadScene(MainScene)을 먼저 호출해야 합니다.");
            return;
        }

        StartCoroutine(LoadSceneCoroutine());
    }

    // LoadScene 메서드에서 씬 이름을 받아오도록 수정
    public static void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("[LoadingBar] LoadScene 호출 시 sceneName이 비어 있습니다.");
            return;
        }

        MainScene = sceneName;  // MainScene을 여기서 설정
        SceneManager.LoadScene("LoadScene");  // 로딩 씬을 먼저 로드
    }

    IEnumerator LoadSceneCoroutine()
    {
        yield return null;

        // MainScene을 비동기로 로드
        AsyncOperation op = SceneManager.LoadSceneAsync(MainScene);
        op.allowSceneActivation = false;  // 씬 자동 활성화를 막음

        float timer = 0f;

        // 씬이 로드될 때까지 진행 상태를 갱신
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if (op.progress < 0.9f)  // 로딩 중인 상태
            {
                if (progressBar != null)
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                    if (Mathf.Abs(progressBar.fillAmount - op.progress) < 0.01f)
                        timer = 0f;
                }
            }
            else  // 로딩이 거의 끝날 때
            {
                if (progressBar != null)
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                    if (progressBar.fillAmount >= 0.99f)
                    {
                        op.allowSceneActivation = true;  // 씬을 활성화하여 로딩을 마침
                    }
                }
                else
                {
                    op.allowSceneActivation = true;  // 로딩 바가 없다면 바로 활성화
                }
            }
        }
    }
}
