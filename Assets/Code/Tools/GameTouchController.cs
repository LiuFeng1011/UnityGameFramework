using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameDir
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}
/// <summary>
/// 玩家操作控制器
/// 
/// </summary>
public class GameTouchController{
	float slideDis = 20;

	public Vector3 m_InputStartVec;
	public Vector3 m_InputFinalVec;
    private float m_Inputtime;

	//确保一次操作对应一次动作
	public bool isStartMouse = false;

	public void Init(){

	}
	// Update is called once per frame
	public void Update () {
		Control() ;
		KeyControl() ;
	}

	public void KeyControl(){
		if (Input.GetKeyDown (KeyCode.W))
		{
			ChangeDirection(GameDir.Up);
		}
		if (Input.GetKeyDown (KeyCode.S))
		{
			ChangeDirection(GameDir.Down);
		}
		if (Input.GetKeyDown (KeyCode.A))
		{
			ChangeDirection(GameDir.Left);
		}
		if (Input.GetKeyDown (KeyCode.D))
		{
			ChangeDirection(GameDir.Right);
		}
	}

	//捕捉玩家控制动作
	private void Control() 
	{
		if (Input.GetMouseButtonDown(0))
		{
			m_InputStartVec = Input.mousePosition;
			isStartMouse = true;

            (new EventTouch(EventID.EVENT_TOUCH_DOWN, m_InputStartVec)).Send();
		}
		else if (Input.GetMouseButtonUp(0))
        {
            (new EventTouch(EventID.EVENT_TOUCH_UP, Input.mousePosition)).Send();
		}
		if (isStartMouse && Input.GetMouseButton(0))
		{
			m_InputFinalVec = Input.mousePosition;

            (new EventTouch(EventID.EVENT_TOUCH_MOVE, Input.mousePosition)).Send();

			float vy = m_InputFinalVec.y - m_InputStartVec.y;
			float vx = m_InputFinalVec.x - m_InputStartVec.x;

            if (Mathf.Abs(vy) > Mathf.Abs(vx))
            {
                if (vy >= slideDis)
                {
                    ChangeDirection(GameDir.Up);
                }
                else if (vy < -slideDis)
                {
                    ChangeDirection(GameDir.Down);
                }
            }
            else
            {
                if (vx >= slideDis)
                {
                    ChangeDirection(GameDir.Right);
                }
                else if (vx < -slideDis)
                {
                    ChangeDirection(GameDir.Left);
                }
            }

            if (m_Inputtime > 0.2f && (Mathf.Abs(vy) >= slideDis * 0.2f || Mathf.Abs(vx) >= slideDis * 0.2f))
            {
                if (Mathf.Abs(vy) >= Mathf.Abs(vx))
                {
                    if (vy >= slideDis * 0.2f)
                    {
                        ChangeDirection(GameDir.Up);
                    }
                    else if (vy < -slideDis * 0.2f)
                    {
                        ChangeDirection(GameDir.Down);
                    }
                }
                else 
                {
                    if (vx >= slideDis * 0.2f)
                    {
                        ChangeDirection(GameDir.Right);
                    }
                    else if (vx < -slideDis * 0.2f)
                    {
                        ChangeDirection(GameDir.Left);
                    }
                }

            }
            else 
            {
                m_Inputtime += Time.deltaTime;
            }
		}


	}

	//捕捉到动作
	public void ChangeDirection(GameDir dir){
        float x = 0;
        float y = 0;
        switch(dir){
            case GameDir.Up:
                y = 1;
                break;
            case GameDir.Down:
                y = -1;
                break;
            case GameDir.Left:
                x = -1;
                break;
            case GameDir.Right:
                x = 1;
                break;

        }

        (new EventTouch(EventID.EVENT_TOUCH_SWEEP, new Vector3(x,y,0))).Send();

	}

}
