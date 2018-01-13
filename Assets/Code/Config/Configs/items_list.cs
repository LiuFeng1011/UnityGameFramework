using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class items_list {

	public	int	items_id	;	/*	"道具id
(int)"	*/
	public	string	items_name	;	/*	"道具名称
(string)"	*/
	public	string	items_model	;	/*	"道具模型
(string)"	*/
	public	int		items_type	;	/*"功能类型(int)
1=积分
2=医疗箱
3=磁铁
4=地雷
5=武器"*/
	public	int	items_parameter	;	/*	"类型参数(string)
小星星=对应积分值
中星星=对应积分值
大星星=对应积分值
医疗箱=对应生命补充值
磁铁=对应额外增加的吸收距离（单位格）
地雷=对应爆炸的伤害值"	*/
	public	int	items_max	;	/*	"同时存在最大值
(int)"	*/
	public	float	items_time	;	/*	"刷新间隔时间参数
（秒）
(float)"	*/
}
public class ItemListManager{
	public List<items_list> datas {get;private set;}
	Dictionary<int, items_list> dic = new Dictionary<int, items_list>();

	public void Load(){

		if(datas != null) datas.Clear();
		dic.Clear();

		datas = ConfigManager.Load<items_list>();
		for(int i = 0 ; i < datas.Count ; i ++){
			dic.Add(datas[i].items_id,datas[i]);
		}
//		Debug.Log("ConstantManager datas : " + JsonMapper.ToJson(datas));
	}

	public items_list GetData(int id){
		if(!dic.ContainsKey(id)){
			return null;
		}
		return dic[id];
	}
}
