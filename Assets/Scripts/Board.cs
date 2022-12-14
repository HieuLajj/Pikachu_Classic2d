using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Board : MonoBehaviour
{
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    public GameObject bgTilePrefab;
    //private int[,] manglogic;
    public PikachuItem[] pikachuItems;
    public List<PikachuItem> pikachiItemsCopy;
    public List<PikachuItem> pikachuConlai;
    public int sookhongtinhbenngoai = 0;
    private static Board instance;
    public static Board Instance
    {
        get{
            if(instance == null){
                instance = FindObjectOfType<Board>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }

    public void SetupGame(){
        sookhongtinhbenngoai = (width+height)*2-4;
        GameManager.Instance.manglogic = new int[width,height];
        GenerateMatchItems();
        Setup();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.B)){
            Debug.Log("dang hoan doi lai vi tri");
            Hoandoicacvitrikhihetduong();
        }
    }
    private void GenerateMatchItems(){
        int halftotal = (width * height - sookhongtinhbenngoai)/2;
        for(int i = 0 ; i < halftotal; i++){
            int h = UnityEngine.Random.Range(0,pikachuItems.Length);
            var pikachuItem = pikachuItems[h];
            if(pikachuItem != null){
                pikachuItem.Id = h;
            }
            pikachiItemsCopy.Add(pikachuItem);
            pikachiItemsCopy.Add(pikachuItem);
        }
        Shuffle(pikachiItemsCopy);
    }
    
    //setup map
    private void Setup(){
        for(int x = 0 ; x < width; x++){
            for(int y = 0; y < height; y++){
                Vector2 pos = new Vector2(x,y);
                GameObject bgTile = Instantiate(bgTilePrefab, pos, Quaternion.identity);
                bgTile.transform.parent = transform;
                Item item = bgTile.GetComponent<Item>();
                if(x==0 || y == 0 || x == width-1 || y == height-1){
                    if((x==0 && y==0) || (x==width-1 && y==0) || (x==0 && y== height-1) || (x==width-1 && y==height-1))
                    {
                        GameManager.Instance.manglogic[x,y] = 2;
                    }else{
                        GameManager.Instance.manglogic[x,y] = 0;
                    }
                    item.DisableRender();
                }else{
                    GameManager.Instance.manglogic[x,y] = 2;
                    item.pikachuItem = pikachiItemsCopy[(y-1)*(width-2)+(x-1)];
                    item.UpdatePosition(x,y);
                    item.UpdateIcon();
                }
                bgTile.name = "item"+ x + "_"+ y;
            }
        }
        pikachiItemsCopy.Clear();
    }

    //xao tron mot List
    private void Shuffle(List<PikachuItem> list) {
        int n = list.Count;
        System.Random rnd = new System.Random();
        while (n > 1) {
            int k = (rnd.Next(0, n) % n);
            n--;
            PikachuItem value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    
    // Danh cho UI
    public void Hoandoicacvitrikhihetduong(){
        Item[] itemall = gameObject.GetComponentsInChildren<Item>();
        foreach(Item item in itemall){
            if(item?.pikachuItem.icon != null){
                pikachuConlai.Add(item.pikachuItem);
            }
        }
        int indexItem = 0;
        Shuffle(pikachuConlai);
        foreach(Item item in itemall){
            if(item?.pikachuItem.icon != null){
                item.pikachuItem = pikachuConlai[indexItem];
                indexItem++;
                item.UpdateIcon();
            }
        }
        pikachuConlai.Clear();
    }

    //reset game
    public void ResetGame(){
        var index = transform.childCount;
        for(var i = index - 1 ; i>=0; i--){
            Destroy(transform.GetChild(i).gameObject);
        }
        SetupGame();
        Timer.Instance.thietlaplaithoigian();
        GameManager.Instance.itemAnswers.Clear();
    }
}
