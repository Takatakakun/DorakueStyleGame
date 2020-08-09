using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 空オブジェクトにコライダーをつけて配置。IsTriggerにチェック。アタッチする。
/// </summary>
public class GoToOtherScene : MonoBehaviour
{
    private LoadSceneManager m_sceneManager;
    //どのシーンへ遷移するか
    [SerializeField][Header("どのシーンに遷移したいかえらぶ")]
    private SceneTransitionData.SceneType scene = SceneTransitionData.SceneType.CastleTown;
    //シーン遷移中かどうか
    private bool isTransition = false;

    private void Awake()
    {
        m_sceneManager = FindObjectOfType<LoadSceneManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        //　次のシーンへ遷移途中でない時
        if (col.tag == "Player" && !isTransition)
        {
            isTransition = true;
            m_sceneManager.GoToNextScene(scene);
        }
    }
    //フェードをした後にシーン読み込み
    IEnumerator FadeAndLoadScene(SceneTransitionData.SceneType scene)
    {
        //　シーンの読み込み
        if (scene == SceneTransitionData.SceneType.CastleTown)
        {
            yield return StartCoroutine(LoadScene("CastleTown"));
        }
        else if (scene == SceneTransitionData.SceneType.CastleTownToWorldMap)
        {
            yield return StartCoroutine(LoadScene("WorldMap"));
        }
        isTransition = false;
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


}
