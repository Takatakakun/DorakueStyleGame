using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
//味方ステータス
[CreateAssetMenu(fileName = "AllyStatus", menuName = "CreateAllyStatus")]
//CharacterStatusを継承
public class AllyStatus : CharacterStatus
{
    //獲得経験値
    [SerializeField]
    private int m_earnedExperience = 0;
    //獲得したお金
    [SerializeField]
    private int m_earnedMoney = 0;
    //装備している武器
    [SerializeField]
    private Item m_equipWeapon = null;
    //装備している鎧
    [SerializeField]
    private Item m_equipArmor = null;
    //アイテムと個数のDictionary
    [SerializeField]
    private ItemDictionary m_itemDictionary = null;

    public void SetEarnedExperience(int earnedExperience)
    {
        this.m_earnedExperience = earnedExperience;
    }

    public int GetEarnedExperience()
    {
        return m_earnedExperience;
    }

    public void SetEarnedMoney(int earnedMoney)
    {
        this.m_earnedMoney = earnedMoney;
    }

    public int GetEarnedMoney()
    {
        return m_earnedMoney;
    }

    public void SetEquipWeapon(Item weaponItem)
    {
        this.m_equipWeapon = weaponItem;
    }

    public Item GetEquipWeapon()
    {
        return m_equipWeapon;
    }

    public void SetEquipArmor(Item armorItem)
    {
        this.m_equipArmor = armorItem;
    }

    public Item GetEquipArmor()
    {
        return m_equipArmor;
    }

    public void CreateItemDictionary(ItemDictionary itemDictionary)
    {
        this.m_itemDictionary = itemDictionary;
    }

    public void SetItemDictionary(Item item, int num = 0)
    {
        m_itemDictionary.Add(item, num);
    }

    //　アイテムが登録された順番のItemDictionaryを返す
    public ItemDictionary GetItemDictionary()
    {
        return m_itemDictionary;
    }
    //　平仮名の名前でソートしたItemDictionaryを返す
    public IOrderedEnumerable<KeyValuePair<Item, int>> GetSortItemDictionary()
    {
        return m_itemDictionary.OrderBy(item => item.Key.GetHiraganaName());
    }
    public int SetItemNum(Item tempItem, int num)
    {
        return m_itemDictionary[tempItem] = num;
    }
    //　アイテムの数を返す
    public int GetItemNum(Item item)
    {
        return m_itemDictionary[item];
    }
}
