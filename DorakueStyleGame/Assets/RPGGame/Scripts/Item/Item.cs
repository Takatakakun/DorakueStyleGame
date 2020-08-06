using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public enum Type
    {
        HPRecovery,//体力回復
        MPRecovery,//魔力回復
        Weapon,    //武器
        Armor,     //防具
        Valuables  //貴重品
    }

    //アイテムの種類
    [SerializeField]
    public Type    m_itemType = Type.HPRecovery;
    //アイテムの漢字名
    [SerializeField]
    private string m_kanjiName = "";
    //アイテムの平仮名名
    [SerializeField]
    private string m_hiraganaName = "";
    //アイテム情報
    [SerializeField]
    private string m_information = "";
    //アイテムのパラメータ
    [SerializeField]
    private int    m_amount = 0;

    //アイテムの種類
    public Type GetItemType()
    {
        return m_itemType;
    }
    //アイテムの名前
    public string GetKanjiName()
    {
        return m_kanjiName;
    }
    //アイテムの平仮名の名前
    public string GetHiraganaName()
    {
        return m_hiraganaName;
    }
    //アイテム情報
    public string GetInformation()
    {
        return m_information;
    }
    //アイテムの強さを返す
    public int GetAmount()
    {
        return m_amount;
    }
}
