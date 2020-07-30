using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController m_characterCtl;
    private Animator m_animator;
    private Vector3 m_velocity;

    public Transform m_verRot;       //縦の視点移動の変数(カメラに合わせる)
    public Transform m_horRot;       //横の視点移動の変数(プレイヤーに合わせる)
    public float m_jumpPower;    //ジャンプ力
    public float m_moveSpeed;    //移動速度

    private void Start()
    {
        m_characterCtl = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        m_horRot.transform.Rotate(new Vector3(0, X_Rotation * 2, 0));
        m_verRot.transform.Rotate(-Y_Rotation * 2, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            m_characterCtl.Move(this.gameObject.transform.forward * m_moveSpeed * Time.deltaTime);
            m_animator.SetBool("run", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_characterCtl.Move(this.gameObject.transform.forward * -1.0f * m_moveSpeed * Time.deltaTime);//①後方にMoveSpeed＊Time.deltaTimeだけ動かす
            m_animator.SetBool("run", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_characterCtl.Move(this.gameObject.transform.right * -1.0f * m_moveSpeed * Time.deltaTime);//①左にMoveSpeed＊Time.deltaTimeだけ動かす
            m_animator.SetBool("run", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_characterCtl.Move(this.gameObject.transform.right * m_moveSpeed * Time.deltaTime);//①右にMoveSpeed＊Time.deltaTimeだけ動かす
            m_animator.SetBool("run", true);
        }

        m_characterCtl.Move(m_velocity);//①キャラクターコントローラーをVelocityだけ動かし続ける
        m_velocity.y += Physics.gravity.y * Time.deltaTime;//①Velocityのy軸を重力*Time.deltaTime分だけ動かす

        if (m_characterCtl.isGrounded)//①キャラクターコントローラーが地面に接触している時に
        {
            if (Input.GetKeyDown(KeyCode.Space))//①スペースキーがおされたら
            {
                m_velocity.y = m_jumpPower;//①Velocity.yをJumpPowerにする
            }
        }
    }

}
