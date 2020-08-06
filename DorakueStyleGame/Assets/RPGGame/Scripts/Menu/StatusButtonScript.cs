using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusButtonScript : MonoBehaviour,ISelectHandler,IDeselectHandler
{
    //ボタンを選択した時に表示する画像
    private Image selectedImage;

    void Awake()
    {
        selectedImage = transform.Find("Image").GetComponent<Image>();
    }

    private void OnEnable()
    {
        //アクティブになった時自身がEventSystemで選択されていたら
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            selectedImage.enabled = true;
        }
        else
        {
            selectedImage.enabled = false;
        }
    }

    //ボタンが選択された時に実行
    public void OnSelect(BaseEventData eventData)
    {
        selectedImage.enabled = true;
    }
    //ボタンが選択解除された時に実行
    public void OnDeselect(BaseEventData eventData)
    {
        selectedImage.enabled = false;
    }
}
