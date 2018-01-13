using UnityEngine;
using System.Collections;

public class NetProtocols    
{

	/// <summary>
	/// 游戏链接服务器前需访问此地址获取服务器的ip地址
	/// type=0 测试开发环境使用此类型，需要会根据不同环境返回不同服务器
	/// type=1 打包时使用1类型，固定返回线上服务器，不会改变
	/// </summary>
	public const string GET_SERVER_ID_ADDRESS = "http://hideseek.dreamgear82.com/hideseek/GetServerIP.php?type=0";

	/// <summary>
	/// 游戏服务器的ip地址
	/// 需要在游戏开始时从网络获取
	/// </summary>
	public static string SERVER_ADDRESS = "192.168.1.91";
//	public static string SERVER_ADDRESS = "47.92.122.22";

	/// <summary>
	/// 服务器端口号，暂时固定，游戏区服使用此值调整
	/// </summary>
	public const int SERVER_PORT = 20101;

	//心跳
	public const int HEART = 0x00000000;

	//[0001 - 000F]系统协议
	public const int RESP_MODULE = 0x00000001;//模块消息

	//[0100 - 02FF]玩家操作
	public const int ENTRY_GAME = 0x00000100;//登录游戏

	//=============ROOM============
	public const int ROOM = 0x00000200;//进入房间

	public const int ROOM_IN = 1;//进入房间
	public const int ROOM_LEAVE = 2;//离开房间
	public const int ROOM_INPLAYER = 3;//房间增加一个玩家
	public const int ROOM_OUTPLAYER = 4;//房间离开一个玩家

	public const int ROOM_CREATE = 5;//创建房间
	public const int ROOM_SEL_MODE = 6;//选择物体

	public const int ROOM_RESET_POSITION = 7;//重置玩家坐标

	public const int ROOM_READY = 10;//开始准备
	public const int ROOM_HIDE = 11;//开始躲藏模式
	public const int ROOM_FIND = 12;//开始寻找模式
	public const int ROOM_OVER = 13;//开始结束
	public const int ROOM_RESULT = 14;//开始结果
	public const int ROOM_WAIT = 15;//开始等待

	public const int ROOM_PLAYER_DIE = 20;//玩家死亡
	public const int ROOM_PLAYER_LIFE = 21;//玩家血量变化

	public const int ROOM_SHOW_RAND_ITEM = 30;//出现随机道具
	public const int ROOM_GET_RAND_ITEM = 31;//拾取道具
	public const int ROOM_USE_ITEM = 32;//使用道具
	public const int ROOM_ITEM_BOMB = 33;//炸弹爆炸

	public const int PLAYER_ACTION = 0x00000300;//玩家行为

	public const int PLAYER_ACTION_MOVE = 1;//移动
	public const int PLAYER_ACTION_STOP = 2;//停止
	public const int PLAYER_ACTION_JUMP = 3;//跳跃
	public const int PLAYER_ACTION_ATK = 4;//攻击
	public const int PLAYER_ACTION_ROTATE = 5;//旋转
	public const int PLAYER_ACTION_DOWN = 6;//蹲下或站起
	public const int PLAYER_ACTION_LOCK = 7;//锁定位置

	//===============PLAYER===============
	public const int PLAYER = 0x00000400;//玩家游戏外操作

	public const int PLAYER_DATA = 1;//下发玩家数据
	public const int PLAYER_ITEMCOUNT = 2;//下发玩家道具数量
	public const int PLAYER_CHANGENAME = 3;//修改昵称
	public const int PLAYER_LVUP = 4;//玩家等级提升

	public const int PLAYER_BUY_ITEM = 5;//购买道具
	public const int PLAYER_USE_ITEM = 6;//使用道具
	public const int PLAYER_GET_ITEMLSIT = 7;//展示道具列表

	public const int PLAYER_SET_NAMECOLOR = 8;//设置昵称颜色
	public const int PLAYER_SET_ROLE = 9;//设置角色

	public const int PLAYER_DAILYTASK_DATA = 10;//更新每日任务数据
	public const int PLAYER_FINISHED_TASK = 11;//完成任务，领取任务奖励

	public const int PLAYER_CHANGEHEAD = 12;//设置头像
	public const int PLAYER_SHOPPING = 13;//购买商店物品
	//===============CHAT===============
	public const int CHAT = 0x00000500;//聊天
	public const int CHAT_NORMAL = 1;//聊天
	public const int CHAT_FACE = 2;//表情
	public const int CHAT_TAUNT = 3;//嘲讽
	//下发消息
	public const int MESSAGE = 0x00010000;//消息

	//test
	public const int TEST = 0x0FFFFFF0;

}
