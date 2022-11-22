using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider timerSlider;
    public float gameTime;
    private bool stopTimer;
    private float time;
    public float timeMax;

    private static Timer instance;
    public static Timer Instance
    {
        get{
            if(instance == null){
                instance = FindObjectOfType<Timer>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    void Start()
    {
       stopTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if( gameTime <= 0.5f && stopTimer == false){
            stopTimer = true;
            Uimanager.Instance.ThongBaoText.text = "THAT BAI";
            GameManager.Instance.Trolaimanhinhmenu();
        }

        if(stopTimer == false){
            gameTime -= Time.deltaTime;
            timerSlider.value = gameTime;
        }
    }

    public void thietlaplaithoigian(){
        gameTime = timeMax;
        timerSlider.maxValue = timeMax;
        timerSlider.value = timeMax;
        stopTimer = false;
    }
}
