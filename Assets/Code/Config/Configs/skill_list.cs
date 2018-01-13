using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class skill_list {

	public	int	skill_id	;	/*	"技能id
(int)"	*/
	public	string	skill_name	;	/*	"技能名称
(string)"	*/
	public	float	skill_time	;	/*	"持续时间
（秒）
(float)"	*/
	public	float	skill_cd	;	/*	"CD时间
（秒）
(float)"	*/
}

public class SkillListManager{
	public List<skill_list> datas {get;private set;}
	Dictionary<int, skill_list> dic = new Dictionary<int, skill_list>();

	public void Load(){

		if(datas != null) datas.Clear();
		dic.Clear();

		datas = ConfigManager.Load<skill_list>();
		for(int i = 0 ; i < datas.Count ; i ++){
			dic.Add(datas[i].skill_id,datas[i]);
		}
//		Debug.Log("ConstantManager datas : " + JsonMapper.ToJson(datas));
	}

	public skill_list GetSkill(int id){
		if(!dic.ContainsKey(id)){
			return null;
		}
		return dic[id];
	}

}
