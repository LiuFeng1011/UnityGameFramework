using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class tank_list {
	public	int	tank_id	;	/*	"坦克id
(int)"	*/
	public	string	tank_name	;	/*	"坦克名称
(string)"	*/
	public	string	tank_model	;	/*	"坦克模型
(string)"	*/
	public	int	tank_hp	;	/*	"生命参数
(int)"	*/
	public	float	tank_speed	;	/*	"移动速度参数
（每秒1单位格）
(float)"	*/
	public	int	tank_rotate_speed	;	/*	"转向速度参数
（每秒1转0.5圈）
(float)"	*/
	public	float	tank_absorb	;	/*	"吸收距离参数
（1单位格）
(float)"	*/
	public	int	tank_invincible	;	/*	"无敌时间参数
（秒）
(float)"	*/
	public	float	tank_uncontrolled	;	/*	"失控时间参数
（秒）
(float)"	*/

}
public class TankListManager{
	List<tank_list> datas ;
	Dictionary<int, tank_list> dic = new Dictionary<int, tank_list>();

	public void Load(){
		if(datas != null) datas.Clear();
		dic.Clear();

		datas = ConfigManager.Load<tank_list>();
		for(int i = 0 ; i < datas.Count ; i ++){
			dic.Add(datas[i].tank_id,datas[i]);
		}

//		Debug.Log(datas[0].tank_name);
//		Debug.Log("ConstantManager datas : " + JsonMapper.ToJson(datas));
	}

	public tank_list GetDataById(int id){
		if(!dic.ContainsKey(id)){
			return null;
		}
		return dic[id];
	}
}