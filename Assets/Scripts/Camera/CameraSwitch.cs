using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CameraSwitch : MonoBehaviour
{
    public PlayableDirector director;
    bool isPlayerIn;
    public GameObject player;
    public CinemachineVirtualCamera virtualCamera; // 你的虚拟相机
    public float speed = 1f; // 用于控制速度的因子

    private CinemachineTrackedDolly trackedDolly;
    private CinemachinePath path;
    void Start()
    {
        trackedDolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(isPlayerIn);
        if (director != null && director.state == PlayState.Playing && trackedDolly != null)
        {
            trackedDolly.m_PathPosition += 1f * Time.deltaTime;
        }
        if (isPlayerIn)
        {
            return;
        }
        FindPlayer();
        xunuan();
        
    }
    void FindPlayer()
    {
        RaycastHit hit01;
        RaycastHit hit02;
        float rayLength = 30f;
        Vector3 rayStart01 = transform.position - transform.up*1.5f + transform.right * 2;
        //transform.up
        Vector3 rayStart02 = transform.position - transform.up * 2.5f + transform.right * 2;
        bool hit01Detected = Physics.Raycast(rayStart01, -transform.right, out hit01, rayLength);
        bool hit02Detected = Physics.Raycast(rayStart02, -transform.right, out hit02, rayLength);

        Debug.DrawRay(rayStart01, -transform.right * rayLength, Color.red);
        Debug.DrawRay(rayStart02, -transform.right * rayLength, Color.red);
        if (hit01Detected || hit02Detected)
        {
            if ((hit01.collider != null && hit01.collider.name == "Kate") ||
        (hit02.collider != null && hit02.collider.name == "Kate"))
            {
               
                    isPlayerIn = true;
            }
        }
    }
    void xunuan()
    {
        if (isPlayerIn)
        {
            
            //Debug.Log(isPlayerIn);
            director.Play();
            // 禁用玩家的移动
            DisablePlayerMovement();
            // 在 Timeline 播放结束后恢复状态
            Invoke("EnablePlayerMovement", (float)director.duration);
        }
    }
    // 禁用玩家移动
    void DisablePlayerMovement()
    {
        // 禁用 CharacterController
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        // 停止 Animator 动画（冻结在当前帧）
        Animator animator = player.GetComponent<Animator>();
        if (animator != null)
        {
            animator.speed = 0;
        }
    }

    // 恢复玩家移动
    void EnablePlayerMovement()
    {
        // 启用 CharacterController
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = true;
        }

        // 恢复 Animator 动画速度
        Animator animator = player.GetComponent<Animator>();
        if (animator != null)
        {
            animator.speed = 1;
        }
    }
}