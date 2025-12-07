using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class music : MonoBehaviour
{
    PlayerInput inputAction; 
    PlayerInput.PlayerActions PlayerInput;
    private GameObject player; // 玩家对象

    bool isplay = false;
    bool isShow = false;
    public GameObject textshow;//声明一个公共游戏对象引用
    private void Awake()
    {
        inputAction = new PlayerInput();
        PlayerInput = inputAction.Player;
        //move是Input类里的变量不是Action Map
        textshow.SetActive(false);//SetActive控制游戏对象是否显示
        player = GameObject.FindWithTag("Player");
    }
    private void OnEnable()
    {
        inputAction.Enable(); //启用输入操作
    }

    
    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance<1)
        {
            textshow.SetActive(true);
            isShow = true;
        }
        else
        {
            textshow.SetActive(false);
            isShow = false;
        }
        AudioSource audio = GetComponent<AudioSource>();
        if (isShow&& PlayerInput.Music.triggered)
        {
            if (!isplay)
            {
                audio.Play();
                isplay = true;
            }
            else
            {
                audio.Stop();
                isplay = false;
            }
        }                                                    
    }
    private void OnDisable()
    {
        inputAction.Disable();//禁用输入操作
    }
}
