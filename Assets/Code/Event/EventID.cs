using UnityEngine;
using System.Collections;

public enum EventID{
    EVENT_ENTRYGAME = 10001,//登录游戏

    //*************************系统消息******************************
    //==========CONFIG==========
    EVENT_CONFIG_LOADFINISHED = 0x15001,//配置表加载完成
    //==========CHANG SCENE==========
    EVENT_SCENE_CHANGE = 0x16001,//切换场景
    EVENT_SCENE_LOADING_FINISHED = 0x16002,//loading场景加载完成

    //==========TOUCH==========
    EVENT_TOUCH_DOWN = 0x17001,//按下
    EVENT_TOUCH_UP = 0x17002,//抬起
    EVENT_TOUCH_MOVE = 0x17003,//移动
    EVENT_TOUCH_SWEEP = 0x17004,//划动
    //*******************************************************
}

