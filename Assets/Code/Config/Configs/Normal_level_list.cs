using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class normal_level_list  {

	public	int		Normal_level_ai	;	/*	"电脑AI数量
(int)"	*/
	public	float	Normal_level_time	;	/*	"每局时间
（秒）
(float)"	*/
	public	float	Normal_level_die	;	/*	"死亡后延时时间
（秒）
(float)"	*/
	public	int		ai_view	;	/*ai视野范围(m)(int)*/
	public	float	absorb_speed;	/*吸金币速度(float)*/
	public	int		disperse_bullet_angle;/*散射子弹间隔角度(int)*/
	public	int		disperse_bullet_count;/*散射子弹数量(int)*/
	public	float	main_camera_high;/*"摄像机高度
(float)"*/
	public	float	main_camera_pos;/*摄像机偏移（前后距离）(float)*/
	public	float	main_camera_rotate;/*摄像机角度0-90(float)*/
}

public class NormalLevelManager{
	public normal_level_list data {get;private set;}

	public void Load(){
		
		List<normal_level_list> datas = ConfigManager.Load<normal_level_list>();

		if(datas.Count <= 0){
			Debug.Log("NormalLevelManager data count <= 0!!!");
			return;
		}
		data = datas[0];


//		Debug.Log("ConstantManager datas : " + JsonMapper.ToJson(data));
	}

}
