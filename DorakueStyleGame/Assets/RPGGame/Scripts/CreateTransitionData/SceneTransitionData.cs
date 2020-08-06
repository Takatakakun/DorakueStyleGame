using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーン遷移時のデータを保存
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "SceneTransitionData",menuName = "CreateSceneTransitionData")]
public class SceneTransitionData : ScriptableObject
{
    public enum SceneType
    {
        StartGame,//ゲーム開始
        CastleTown,//城下町
        CastleTownToWorldMap,//城下町からワールドマップへ
        WorldMap//ワールドマップ
    }

    [SerializeField]
    private SceneType m_sceneType;

    //シーンタイプの初期化
    public void OnEnable()
    {
        m_sceneType = SceneType.StartGame;
    }
    //シーンタイプの設定
    public void SetSceneType(SceneType scene)
    {
        m_sceneType = scene;
    }
    //シーンタイプを返す
    public SceneType GetSceneType()
    {
        return m_sceneType;
    }
}
