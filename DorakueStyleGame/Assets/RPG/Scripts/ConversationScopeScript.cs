using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationScopeScript : MonoBehaviour
{
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player" && col.GetComponent<MainPlayerScript>().GetState() != MainPlayerScript.State.Talk)
        {
            //メインプレイヤーが近づいたら会話相手として自分のゲームオブジェクトを渡す
            col.GetComponent<MainPlayerTalkScript>().SetConversationPartner(transform.parent.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" && col.GetComponent<MainPlayerScript>().GetState() != MainPlayerScript.State.Talk)
        {
            //メインプレイヤーが遠ざかったら会話相手から外す
            col.GetComponent<MainPlayerTalkScript>().ResetConversationPartner(transform.parent.gameObject);
        }
    }
}
