using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SaveManager : MonoBehaviour
{
    PlayerStatus m_playerStatus = new PlayerStatus();

    public void SavePlayerData()
    {
        StreamWriter m_writer;
        var playerName = "タカシ";

        m_playerStatus.SetPlayerName(playerName);
        m_playerStatus.SetPlayerLevel(1);
        m_playerStatus.SetMaxHp(100);
        m_playerStatus.SetHp(100);
        m_playerStatus.SetMaxMp(50);
        m_playerStatus.SetMp(50);
        

        string jsonstr = JsonUtility.ToJson(m_playerStatus);

        m_writer = new StreamWriter(Application.dataPath +"/Save/"+ playerName + ".json", false);
        m_writer.Write(jsonstr);
        m_writer.Flush();
        m_writer.Close();
    }
}
