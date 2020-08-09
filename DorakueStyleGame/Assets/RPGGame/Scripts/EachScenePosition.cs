using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移ごとのプレイヤーの場所を設定します。プレイヤーにアタッチ。
/// </summary>
public class EachScenePosition : MonoBehaviour
{
    [SerializeField]
    private SceneTransitionData m_sceneTransitionData = null;

    private void Start()
    {
        //シーン遷移の種類に応じて初期位置の設定
        if (m_sceneTransitionData.GetSceneType() == SceneTransitionData.SceneType.StartGame)
        {
            var initialPosition = GameObject.Find("InitialPosition").transform;
            transform.position = initialPosition.position;
            transform.rotation = initialPosition.rotation;
        }
        else if (m_sceneTransitionData.GetSceneType() == SceneTransitionData.SceneType.CastleTown)
        {
            var initialPosition = GameObject.Find("InitialPosition").transform;
            transform.position = initialPosition.position;
            transform.rotation = initialPosition.rotation;
        }
        else if (m_sceneTransitionData.GetSceneType() == SceneTransitionData.SceneType.CastleTownToWorldMap)
        {
            var initialPosition = GameObject.Find("InitialPositionCastleTownToWorldMap").transform;
            transform.position = initialPosition.position;
            transform.rotation = initialPosition.rotation;
        }

        Debug.Log(m_sceneTransitionData.GetSceneType());
    }
}
