using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistance : MonoBehaviour
{
    private float defaultDistance = 5.0f;
    private float miniDistance = 1.0f;
    private float maxDistance = 10.0f;
    private float zoom = 3.0f;//滑轮控制扩大与缩放的速度
    private float currentDistance;
    private CinemachineFramingTransposer framingTransposer;//CinemachineFramingTransposer是CinemachineVirtualCamera->Body的Framing Transposer选项
    private CinemachineInputProvider inputProvider;
    private void Awake()
    {
        framingTransposer=GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        //获取虚拟相机
        //获取虚拟相机的帧转置器 
        inputProvider=GetComponent<CinemachineInputProvider>(); 
        currentDistance=defaultDistance;
    }
   
    private void Update()
    {
        cDistance();//获取鼠标滑轮滚动值
    }

    private void cDistance()
    {
        float zoomValue = inputProvider.GetAxisValue(2);
        //轴的索引X:0 Y:1 Z:2
        currentDistance=Mathf.Clamp(currentDistance+zoomValue*zoom,miniDistance,maxDistance);//计算距离并限制距离
        framingTransposer.m_CameraDistance = currentDistance;//输出距离                                                                               
    }
}
