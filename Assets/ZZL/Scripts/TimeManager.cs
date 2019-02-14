using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton time manager that only exists only one copy in the game
// 假设游戏时间设定为写死，P5的时间系统
public class TimeManager : MonoBehaviour
{
    public static TimeManager s_instance = null;

    public int days;

    public static int s_days;
    
    void Awake()
    {
        if(!s_instance)
        {
            s_instance = this;
        }
        else if(s_instance != this)
        {
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        s_days = days;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        s_days = days;
    }
}
