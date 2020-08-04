using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// セーブ機能
/// </summary>

public class SaveManager : MonoBehaviour
{
    PlayerStatus m_playerStatus = new PlayerStatus();

    public void SavePlayerData()
    {
        StreamWriter m_writer;
        var playerName = "吉田崇信";
        m_playerStatus.SetPlayerName(playerName);

        string jsonstr = JsonUtility.ToJson(m_playerStatus);

        m_writer = new StreamWriter(Application.dataPath +"/Save/"+ playerName + ".json", false);
        m_writer.Write(jsonstr);
        m_writer.Flush();
        m_writer.Close();
    }
}
