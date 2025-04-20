using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextScene : MonoBehaviour
{
    [SerializeField] private float delayBeforeStart = 3f; // 몇 초 뒤에 씬 전환할지 설정

    void Start()
    {
        // 일정 시간 후 씬을 전환하는 함수 호출
        Invoke(nameof(LoadNextScene), delayBeforeStart);
    }

    void LoadNextScene()
    {
        LoadingBar.LoadScene("MainScene");
    }
}
