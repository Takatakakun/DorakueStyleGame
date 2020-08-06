using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "SceneTransitionData",menuName = "CreateSceneTransitionData")]
public class SceneTransitionData : ScriptableObject
{
    public enum SceneType
    {
        StartGame,
        FirstVillage,
        FirstVillageToWorldMap,
        WorldMapToBattle,
        BattleToWorldMap,
        WorldMap
    }

    [SerializeField]
    private SceneType sceneType;

    public void OnEnable()
    {
        sceneType = SceneType.StartGame;
    }

    public void SetSceneType(SceneType scene)
    {
        sceneType = scene;
    }

    public SceneType GetSceneType()
    {
        return sceneType;
    }
}
