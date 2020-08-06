using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    //シーンデータ
    [SerializeField]
    private SceneTransitionData.SceneType m_sceneType;
    [SerializeField]
    private Vector3 m_position;//場所
    [SerializeField]
    private Quaternion m_rotation;//回転


    //プレイヤーの保存データ
    [SerializeField]
    private string         m_playerName;            //名前
    [SerializeField]
    private int            m_playerLevel;           //レベル
    [SerializeField]
    private int            m_playerMaxHp;           //最大体力
    [SerializeField]
    private int            m_playerHp;              //体力
    [SerializeField]
    private int            m_playerMaxMp;           //最大魔力
    [SerializeField]
    private int            m_playerMp;              //魔力
    [SerializeField]
    private int            m_playerAgility;         //素早さ
    [SerializeField]
    private int            m_playerPower;           //攻撃力
    [SerializeField]
    private int            m_playerMagicPower;      //魔法力
    [SerializeField]
    private int            m_playerPowerDefense;    //防御力
    [SerializeField]
    private int            m_playerMagicDefense;    //魔法防御力
    [SerializeField]
    private int            m_playerEarnedExperience;//獲得経験値
    [SerializeField]
    private Item           m_playerEquipWeapon;     //装備武器
    [SerializeField]
    private Item           m_playerEquipArmor;      //装備防具
    [SerializeField]
    private ItemDictionary m_playerItemDictionary;
    [SerializeField]
    private List<Item>     m_playerDictionaryKeys;
    [SerializeField]
    private List<int>      m_playerItemDictionaryValues;


    //シーンデータプロパティ
    public SceneTransitionData.SceneType SceneType
    {
        get { return m_sceneType; }
        set { m_sceneType = value; }
    }
    public Vector3 Position
    {
        get { return m_position; }
        set { m_position = value; }
    }
    public Quaternion Rotation
    {
        get { return m_rotation; }
        set { m_rotation = value; }
    }


    //プレイヤー保存データプロパティ
    public string PlayerName
    {
        get { return m_playerName; }
        set { m_playerName = value; }
    }
    public int PlayerLevel
    {
        get { return m_playerLevel; }
        set { m_playerLevel = value; }
    }
    public int PlayerMaxHP
    {
        get { return m_playerMaxHp; }
        set { m_playerMaxHp = value; }
    }
    public int PlayerHP
    {
        get { return m_playerHp; }
        set { m_playerHp = value; }
    }
    public int PlayerMaxMP
    {
        get { return m_playerMaxMp; }
        set { m_playerMaxMp = value; }
    }
    public int PlayerMP
    {
        get { return m_playerMp; }
        set { m_playerMp = value; }
    }
    public int PlayerAgility
    {
        get { return m_playerAgility; }
        set { m_playerAgility = value; }
    }
    public int PlayerPower
    {
        get { return m_playerPower; }
        set { m_playerPower = value; }
    }
    public int PlayerMagicPower
    {
        get { return m_playerMagicPower; }
        set { m_playerMagicPower = value; }
    }
    public int PlayerPowerDefense
    {
        get { return m_playerPowerDefense; }
        set { m_playerPowerDefense = value; }
    }
    public int PlayerMagicDefense
    {
        get { return m_playerMagicDefense; }
        set { m_playerMagicDefense = value; }
    }
    public int PlayerEarnedExperience
    {
        get { return m_playerEarnedExperience; }
        set { m_playerEarnedExperience = value; }
    }
    public Item PlayerEquipWeapon
    {
        get { return m_playerEquipWeapon; }
        set { m_playerEquipWeapon = value; }
    }
    public Item PlayerEquipArmor
    {
        get { return m_playerEquipArmor; }
        set { m_playerEquipArmor = value; }
    }

    public ItemDictionary PlayerItemDictionary
    {
        get { return m_playerItemDictionary; }
        set { m_playerItemDictionary = value; }
    }
    public List<Item> PlayerItemDictionaryKeys
    {
        get { return m_playerDictionaryKeys; }
        set { m_playerDictionaryKeys = value; }
    }
    public List<int> PlayerItemDictionaryValues
    {
        get { return m_playerItemDictionaryValues; }
        set { m_playerItemDictionaryValues = value; }
    }
}
