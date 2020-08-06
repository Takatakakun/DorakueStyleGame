using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    private LoadSceneManager sceneManager;
    //メニュー用UI
    [SerializeField][Header("Menuを入れる")]
    private GameObject m_menuUI = null;
    private PlayerController m_playerController;

    private void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<LoadSceneManager>();
        m_playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (sceneManager.IsTransition()|| m_playerController.GetState() == PlayerController.PlayerState.Talk)
        {
            return;
        }
        //メニューUIの表示・非表示の切り替え
        if (Input.GetButtonDown("Y_Button"))
        {
            //コマンド
            if (!m_menuUI.activeSelf)
            {
                //プレイヤーをコマンド状態にする
                m_playerController.SetState(PlayerController.PlayerState.Menu);
            }
            else
            {
                ExitMenu();
            }
            //コマンドUIのオン・オフ
            m_menuUI.SetActive(!m_menuUI.activeSelf);
        }
    }
    //CommandScriptから呼び出すメニュー画面の終了
    public void ExitMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        m_playerController.SetState(PlayerController.PlayerState.Normal);
    }
}
