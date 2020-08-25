using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerScript : MonoBehaviour
{
    public enum State
    {
        Normal,
        Talk,
        Command,
        Wait
    }

    private CharacterController characterController;
    private Animator animator;
    //ユニティちゃんの状態
    private State state;
    //ユニティちゃん会話処理スクリプト
    private MainPlayerTalkScript mainPlayerTalkScript;

    //プレイヤーの速度
    private Vector3 m_velocity;
    //移動スピード
    [SerializeField]
    private float m_moveSpeed = 4.0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        state = State.Wait;
        mainPlayerTalkScript = GetComponent<MainPlayerTalkScript>();
    }

    private void Update()
    {
        if (state == State.Normal)
        {
            if (characterController.isGrounded)
            {
                m_velocity = Vector3.zero;

                var input = new Vector3(Input.GetAxis("LStickHorizontal"), 0.0f, Input.GetAxis("LStickVertical"));

                if (input.magnitude > 0.1f)
                {
                    transform.LookAt(transform.position + input.normalized);
                    animator.SetBool("Run", true);
                    m_velocity += transform.forward * m_moveSpeed;
                }
                else
                {
                    animator.SetBool("Run", false);
                }

                if (mainPlayerTalkScript.GetConversationPartner() != null && Input.GetButtonDown("A_Button"))
                {
                    SetState(State.Talk);
                }
            }
        }
        else if (state == State.Talk)
        {

        }
        else if (state == State.Command)
        {

        }
        else if (state == State.Wait)
        {
            if (mainPlayerTalkScript.GetConversationPartner() != null&& Input.GetButtonDown("A_Button"))
            {
                SetState(State.Talk);
            }
        }

        //シーン遷移後に移動させるとデフォルトの位置にキャラクターがセットされてしまうので回避
        if (state != State.Wait)
        {
            m_velocity.y += Physics.gravity.y * Time.deltaTime;
            characterController.Move(m_velocity * Time.deltaTime);
        }
        else
        {
            if (!Mathf.Approximately(Input.GetAxis("LStickHorizontal"), 0.0f) || !Mathf.Approximately(Input.GetAxis("LStickVertical"), 0.0f))
            {
                SetState(State.Normal);
            }
        }
    }

    //状態変更と初期設定
    public void SetState(State state)
    {
        this.state = state;

        if (state == State.Talk)
        {
            m_velocity = Vector3.zero;
            animator.SetBool("Run", false);
            mainPlayerTalkScript.StartTalking();
        }
        else if (state == State.Command)
        {
            m_velocity = Vector3.zero;
            animator.SetBool("Run", false);
        }
        else if (state == State.Wait)
        {
            m_velocity = Vector3.zero;
            animator.SetBool("Run", false);
        }
    }

    public State GetState()
    {
        return state;
    }
}
