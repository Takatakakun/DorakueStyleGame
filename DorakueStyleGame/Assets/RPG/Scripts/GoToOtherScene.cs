﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToOtherScene : MonoBehaviour
{
    private LoadSceneManager sceneManager;
    //どのシーンへ遷移するか
    [SerializeField]
    private SceneMovementData.SceneType scene = SceneMovementData.SceneType.FirstVillage;
    //シーン遷移中かどうか
    private bool m_isTransition;

    private void Awake()
    {
        sceneManager = FindObjectOfType<LoadSceneManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        //次のシーンへ遷移途中でない時
        if (col.tag == "Player" && !m_isTransition)
        {
            m_isTransition = true;
            sceneManager.GoToNextScene(scene);
        }
    }
    //フェードをした後にシーン読み込み
    // IEnumerator FadeAndLoadScene(SceneMovementData.SceneType scene)
    //{
    //    /*
    //    その他の処理
    //    */
    //    m_isTransition = false;
    //}
}
