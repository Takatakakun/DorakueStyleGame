using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CharacterStatus : ScriptableObject
{
    [SerializeField]
    private string m_characterName = "";    //キャラクターの名前
    [SerializeField]
    private int    m_level = 1;             //キャラクターのレベル
    [SerializeField]
    private int    m_maxHp = 100;           //最大HP
    [SerializeField]
    private int    m_hp = 100;              //HP
    [SerializeField]
    private int    m_maxMp = 50;            //最大MP
    [SerializeField]
    private int    m_mp = 50;               //MP
    [SerializeField]
    private int    m_agility = 5;           //素早さ
    [SerializeField]
    private int    m_power = 10;            //攻撃力
    [SerializeField]
    private int    m_magicPower = 10;       //魔法力
    [SerializeField]
    private int    m_powerDefense = 10;     //防御力
    [SerializeField]
    private int    m_magicDefense = 10;     //魔法防御力


    public void SetCharacterName(string characterName)
    {
        this.m_characterName = characterName;
    }
    public string GetCharacterName()
    {
        return m_characterName;
    }

    public void SetLevel(int level)
    {
        this.m_level = level;
    }
    public int GetLevel()
    {
        return m_level;
    }

    public void SetMaxHp(int hp)
    {
        this.m_maxHp = hp;
    }
    public int GetMaxHp()
    {
        return m_maxHp;
    }

    public void SetHp(int hp)
    {
        this.m_hp = Mathf.Max(0, Mathf.Min(GetMaxHp(), hp));
    }
    public int GetHp()
    {
        return m_hp;
    }

    public void SetMaxMp(int mp)
    {
        this.m_maxMp = mp;
    }
    public int GetMaxMp()
    {
        return m_maxMp;
    }

    public void SetMp(int mp)
    {
        this.m_mp = Mathf.Max(0, Mathf.Min(GetMaxMp(), mp));
    }
    public int GetMp()
    {
        return m_mp;
    }

    public void SetAgility(int agility)
    {
        this.m_agility = agility;
    }
    public int GetAgility()
    {
        return m_agility;
    }

    public void SetPower(int power)
    {
        this.m_power = power;
    }
    public int GetPower()
    {
        return m_power;
    }

    public void SetMagicPower(int magicPower)
    {
        this.m_magicPower = magicPower;
    }
    public int GetMagicPower()
    {
        return m_magicPower;
    }

    public void SetPowerDefense(int powerDefense)
    {
        this.m_powerDefense = powerDefense;
    }
    public int GetPowerDefense()
    {
        return m_powerDefense;
    }

    public void SetMagicDefense(int magicDefense)
    {
        this.m_magicDefense = magicDefense;
    }
    public int GetMagicDefense()
    {
        return m_magicDefense;
    }

}
