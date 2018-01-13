using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class weapons_list  {

	public	int	weapons_id	;	/*	"武器id
(int)"	*/
	public	string	weapons_name	;	/*	"武器名称
(string)"	*/
	public	string	weapons_model	;	/*	"武器模型
(string)"	*/
	public	int	weapons_atk	;	/*	"攻击力参数
(int)"	*/
	public	float	weapons_speed	;	/*	"射速参数
（单位格）
(float)"	*/
	public	float	weapons_range	;	/*	"射程参数
（单位格）
(float)"	*/
	public	float	weapons_cost	;	/*	"最大弹量参数
(float)"	*/
	public	float	weapons_cd	;	/*	"攻击间隔时间
（秒）
           (float)"	*/

	public	int	bullet_bomb_effect;/*子弹爆炸特效（int）*/
	public	int	bullet_tail_effect;/*子弹拖尾特效(int)*/
}
public class WeaponListManager{
	List<weapons_list> datas ;
	Dictionary<int, weapons_list> dic = new Dictionary<int, weapons_list>();

	public void Load(){

		if(datas != null) datas.Clear();
		dic.Clear();

		datas = ConfigManager.Load<weapons_list>();
		for(int i = 0 ; i < datas.Count ; i ++){
			dic.Add(datas[i].weapons_id,datas[i]);
		}

//		Debug.Log("ConstantManager datas : " + JsonMapper.ToJson(datas));
	}

	public weapons_list GetData(int wid){
		if(!dic.ContainsKey(wid)){
			return null;
		}
		return dic[wid];
	}
}