using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerScript : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    //プレイヤーの速度
    private Vector3 m_velocity;
    //移動スピード
    [SerializeField]
    private float m_moveSpeed = 4.0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (characterController.isGrounded)
        {
            m_velocity = Vector3.zero;

            var input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

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
        }
        m_velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(m_velocity * Time.deltaTime);
    }
}
