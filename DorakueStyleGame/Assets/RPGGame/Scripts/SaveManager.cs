using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private AllyStatus m_playerStatus = null;
    [SerializeField]
    private Transform m_playerTransform;
    [SerializeField]
    private Text m_saveData1;
    [SerializeField]
    private Text m_saveData2;
    [SerializeField]
    private Text m_saveData3;
    [SerializeField]
    private string m_dataName = "UserRPGData";

    /// <summary>
    /// セーブデータの表示
    /// </summary>
    public void DisplaySaveData()
    {
        Data data;
        //　いる場所
        string place = "";

        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.HasKey(m_dataName + i))
            {
                data = JsonUtility.FromJson<Data>(PlayerPrefs.GetString(m_dataName + i));

                if (data.SceneType == SceneTransitionData.SceneType.FirstVillage)
                {
                    place = "最初の村";
                }
                else if (data.SceneType == SceneTransitionData.SceneType.WorldMap)
                {
                    place = "ワールドマップ";
                }
                else
                {
                    place = "";
                }

                if (i == 1)
                {
                    m_saveData1.text = "データ" + i + "： LV" + data.PlayerLevel + " " + data.PlayerName + " " + place;
                }
                else if (i == 2)
                {
                    m_saveData2.text = "データ" + i + "： LV" + data.PlayerLevel + " " + data.PlayerName + " " + place;
                }
                else if (i == 3)
                {
                    m_saveData3.text = "データ" + i + "： LV" + data.PlayerLevel + " " + data.PlayerName + " " + place;
                }
            }
            else
            {
                if (i == 1)
                {
                    m_saveData1.text = "データ" + i + "： " + "データがありません。";
                }
                else if (i == 2)
                {
                    m_saveData2.text = "データ" + i + "： " + "データがありません。";
                }
                else if (i == 3)
                {
                    m_saveData3.text = "データ" + i + "： " + "データがありません。";
                }
            }
        }
    }

    //Saveメソッド
    public void Save(int num)
    {
        Data data = new Data();
        //シーンデータをセット
        if (SceneManager.GetActiveScene().name == "Village")
        {
            data.SceneType = SceneTransitionData.SceneType.FirstVillage;
        }
        else if (SceneManager.GetActiveScene().name == "WorldMap")
        {
            data.SceneType = SceneTransitionData.SceneType.WorldMap;
            data.Position = m_playerTransform.position;
            data.Rotation = m_playerTransform.rotation;
        }

        //プレイヤーデータをセット
        data.PlayerName             = m_playerStatus.GetCharacterName();
        data.PlayerLevel            = m_playerStatus.GetLevel();
        data.PlayerMaxHP            = m_playerStatus.GetMaxHp();
        data.PlayerHP               = m_playerStatus.GetHp();
        data.PlayerMaxMP            = m_playerStatus.GetMaxMp();
        data.PlayerMP               = m_playerStatus.GetMp();
        data.PlayerAgility          = m_playerStatus.GetAgility();
        data.PlayerPower            = m_playerStatus.GetPower();
        data.PlayerMagicPower       = m_playerStatus.GetMagicPower();
        data.PlayerPowerDefense     = m_playerStatus.GetPowerDefense();
        data.PlayerMagicDefense     = m_playerStatus.GetMagicDefense();
        data.PlayerEarnedExperience = m_playerStatus.GetEarnedExperience();
        data.PlayerEquipWeapon      = m_playerStatus.GetEquipWeapon();
        data.PlayerEquipArmor       = m_playerStatus.GetEquipArmor();

        var playerItemDictionaryKeys = new List<Item>();
        var playerItemDictionaryValues = new List<int>();
        foreach (var item in m_playerStatus.GetItemDictionary())
        {
            playerItemDictionaryKeys.Add(item.Key);
            playerItemDictionaryValues.Add(item.Value);
        }
        data.PlayerItemDictionaryKeys = playerItemDictionaryKeys;
        data.PlayerItemDictionaryValues = playerItemDictionaryValues;

        PlayerPrefs.SetString(m_dataName + num, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }
}
