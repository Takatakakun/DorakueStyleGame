using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// セーブデータの表示
/// </summary>
public class DisplaySaveData : MonoBehaviour
{
    PlayerStatus m_playerStatus = new PlayerStatus();

    public Text m_saveData1;
    public Text m_saveData2;
    public Text m_saveData3;

    private void Start()
    {

        LoadPlayerData();
    }

    public void LoadPlayerData()
    {

        string datastr = "";

        StreamReader reader;


        reader = new StreamReader(Application.dataPath + "/Save/" + "吉田崇信" + ".json");
        datastr = reader.ReadToEnd();
        reader.Close();

        m_playerStatus = JsonUtility.FromJson<PlayerStatus>(datastr);
        Debug.Log(m_playerStatus.GetPlayerName() + "のデータをロードしました");
        m_saveData1.text = m_playerStatus.GetPlayerName();
    }

}