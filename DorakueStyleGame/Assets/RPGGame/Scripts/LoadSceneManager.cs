using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 各シーンに空のオブジェクト（SceneManager）を作りアタッチ
/// </summary>
public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager m_loadSceneManager;
    [SerializeField]
    [Header("プレイヤーのシーンデータを入れる")]
    private SceneTransitionData m_sceneMovementData = null;
    //フェードプレハブ
    [SerializeField]
    private GameObject m_fadePrefab = null;
    //フェードインスタンス
    private GameObject m_fadeInstance;
    //フェードの画像
    private Image m_fadeImage;
    [SerializeField]
    private float m_fadeSpeed = 5.0f;
    //　シーン遷移中かどうか
    private bool m_isTransition;

    private void Awake()
    {
        // LoadSceneMangerは常に一つだけにする
        if (m_loadSceneManager == null)
        {
            m_loadSceneManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //　次のシーンを呼び出す
    public void GoToNextScene(SceneTransitionData.SceneType scene)
    {
        m_isTransition = true;
        m_sceneMovementData.SetSceneType(scene);
        StartCoroutine(FadeAndLoadScene(scene));
    }

    //　フェードをした後にシーン読み込み
    IEnumerator FadeAndLoadScene(SceneTransitionData.SceneType scene)
    {
        //　フェードUIのインスタンス化
        m_fadeInstance = Instantiate<GameObject>(m_fadePrefab);
        m_fadeImage = m_fadeInstance.GetComponentInChildren<Image>();
        //　フェードアウト処理
        yield return StartCoroutine(Fade(1.0f));

        //　シーンの読み込み
        if (scene == SceneTransitionData.SceneType.CastleTown)
        {
            yield return StartCoroutine(LoadScene("CastleTown"));
        }
        else if (scene == SceneTransitionData.SceneType.CastleTownToWorldMap)
        {
            yield return StartCoroutine(LoadScene("WorldMap"));
        }

        //　フェードUIのインスタンス化
        m_fadeInstance = Instantiate<GameObject>(m_fadePrefab);
        m_fadeImage = m_fadeInstance.GetComponentInChildren<Image>();
        m_fadeImage.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

        //　フェードイン処理
        yield return StartCoroutine(Fade(0.0f));

        Destroy(m_fadeInstance);
    }

    //　フェード処理
    IEnumerator Fade(float alpha)
    {
        var fadeImageAlpha = m_fadeImage.color.a;

        while (Mathf.Abs(fadeImageAlpha - alpha) > 0.01f)
        {
            fadeImageAlpha = Mathf.Lerp(fadeImageAlpha, alpha, m_fadeSpeed * Time.deltaTime);
            m_fadeImage.color = new Color(0f, 0f, 0f, fadeImageAlpha);
            yield return null;
        }
    }

    //　実際にシーンを読み込む処理
    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }
    }

    public bool IsTransition()
    {
        return m_isTransition;
    }
}
