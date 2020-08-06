using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// セーブデータの表示
/// </summary>
public class DisplaySaveData : MonoBehaviour
{
    PlayerStatus m_playerStatus = new PlayerStatus();

    public List<Text> m_saveData;

    public static string m_selectDataName;

    private void Start()
    {
        LoadPlayerData();

        //Debug.Log(m_saveData.Count);

        //Debug.Log(m_saveData[0]);
        //Debug.Log(m_saveData[1]);
        //Debug.Log(m_saveData[2]);
    }

    //画面に表示用
    public void LoadPlayerData()
    {
        //.jsonファイルを検索
        string path = Application.dataPath + "/" + "Save";
        string[] file = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);

        string datastr = "";

        //.jsonファイル分ループ
        for (int i = 0; i < file.Length; i++)
        {
            //データの読み込み
            StreamReader reader = new StreamReader(file[i]);
            datastr = reader.ReadToEnd();
            reader.Close();

            //読み込んだデータをPlayerStatusに変換
            m_playerStatus = JsonUtility.FromJson<PlayerStatus>(datastr);
            //テキストに入れる
            m_saveData[i].text = "LV : " + m_playerStatus.GetPlayerLevel().ToString() + "\n" +
                                 "プレイヤー名 : " + m_playerStatus.GetPlayerName() + "\n";

        }
        
    }

    //ボタン押されたとき
    public void OnClick(string filename)
    {
        //選んだファイル名を保存（別のシーンでも使うため）
        m_selectDataName = filename;

        SceneManager.LoadScene("BattleScene");
    }

}