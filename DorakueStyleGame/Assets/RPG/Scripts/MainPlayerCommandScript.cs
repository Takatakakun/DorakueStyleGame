using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainPlayerCommandScript : MonoBehaviour
{
    private LoadSceneManager sceneManager;
    //コマンド用UI
    [SerializeField]
    private GameObject commandUI = null;
    private MainPlayerScript mainPlayerScript;

    private void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<LoadSceneManager>();
        mainPlayerScript = GetComponent<MainPlayerScript>();
    }

    private void Update()
    {
        if (sceneManager.IsTransition() || mainPlayerScript.GetState() == MainPlayerScript.State.Talk)
        {
            return;
        }
        //コマンドUIの表示・非表示の切り替え
        if (Input.GetButtonDown("Menu"))
        {
            //　コマンド
            if (!commandUI.activeSelf)
            {
                //メインプレイヤーをコマンド状態にする
                mainPlayerScript.SetState(MainPlayerScript.State.Command);
            }
            else
            {
                ExitCommand();
            }
            //コマンドUIのオン・オフ
            commandUI.SetActive(!commandUI.activeSelf);
        }
    }
    //CommandScriptから呼び出すコマンド画面の終了
    public void ExitCommand()
    {
        EventSystem.current.SetSelectedGameObject(null);
        mainPlayerScript.SetState(MainPlayerScript.State.Normal);
    }
}
