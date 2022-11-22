using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Uimanager : MonoBehaviour
{
    public GameObject ingame;
    public GameObject menugame;
    public Button lv1Btn;
    public Button lv2Btn;
    public Button lv3Btn;
    public TMP_Text LevelText;
    public TMP_Text ThongBaoText;
    private static Uimanager instance;
    public static Uimanager Instance
    {
        get{
            if(instance == null){
                instance = FindObjectOfType<Uimanager>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    void Start()
    {
        ThongBaoText.gameObject.SetActive(false);
        ingame.SetActive(true);
        menugame.SetActive(true);
        lv1Btn.onClick.AddListener(()=>{
            Board.Instance.ResetGame();
            menugame.SetActive(false);
            GameManager.Instance.levelCurrent = Level.LEVEL1;
            LevelText.text = "Level1";
        });
        lv2Btn.onClick.AddListener(()=>{
            Board.Instance.ResetGame();
            menugame.SetActive(false);
            GameManager.Instance.levelCurrent = Level.LEVEL2;
            LevelText.text = "Level2";
        });
        lv3Btn.onClick.AddListener(()=>{
            Board.Instance.ResetGame();
            menugame.SetActive(false);
            GameManager.Instance.levelCurrent = Level.LEVEL3;
            LevelText.text = "Level3";
        });
    }
}
