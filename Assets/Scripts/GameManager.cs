using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Item> itemAnswers;
    public bool m_isAnswerChecking = false;
    public int[,] manglogic;
    private static GameManager instance;
    public static GameManager Instance
    {
        get{
            if(instance == null){
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            Debug.Log("dang in nay");
            printLogicArray();
        }
    }

    public void Kiemtra(){
        if(itemAnswers[0].pikachuItem.Id == itemAnswers[1].pikachuItem.Id){
            changeLogicArray(0);
            if(itemAnswers[0].x == itemAnswers[1].x){
                if(checkLineX(itemAnswers[0].y, itemAnswers[1].y, itemAnswers[0].x)){
                    successCheckLogic();
                    return;
                }
            }
            if(itemAnswers[0].y == itemAnswers[1].y){
                if(checkLineY(itemAnswers[0].x, itemAnswers[1].x, itemAnswers[0].y)){
                    successCheckLogic();
                    return;
                }
            }
            if(checkRectX(itemAnswers[0], itemAnswers[1]) != -1){
                successCheckLogic();
                return;
            }
            if(checkRectY(itemAnswers[0], itemAnswers[1]) != -1){
                successCheckLogic();
                return;
            }
            if(checkMoreLineX(itemAnswers[0], itemAnswers[1], 1) != -1 ){
                successCheckLogic();
                return;
            }

            if(checkMoreLineX(itemAnswers[0], itemAnswers[1], -1) != -1 ){
                successCheckLogic();
                return;
            }

            if(checkMoreLineY(itemAnswers[0], itemAnswers[1], 1)!= -1){
                successCheckLogic();
                DestroyAnswers();
                return;
            }

            if(checkMoreLineY(itemAnswers[0], itemAnswers[1], -1)!= -1){
                successCheckLogic();
                return;
            }
            changeLogicArray(2);
        }else{
            Debug.Log("sai roi");
        }
    }
    //Xoa vs reset logic mang neu thanh cong
    private void successCheckLogic(){
        changeLogicArray(5);
        DestroyAnswers();
        //StartCoroutine(DecreaseRowCo());
        StartCoroutine(IncreaseRowCo());
    }

    // doi gia tri mang logic
    private void changeLogicArray(int a){
        manglogic[itemAnswers[0].x,itemAnswers[0].y] = a;
        manglogic[itemAnswers[1].x,itemAnswers[1].y] = a;
    }
    private void DestroyAnswers(){
        itemAnswers[0].DestroyItem();
        itemAnswers[1].DestroyItem();
    }

    //check with line x, from column y1 to y2
    private bool checkLineX(int y1, int y2, int x) {
        //Debug.Log("dang checkLineX");
        // find point have column max and min
        int min = Mathf.Min(y1, y2);
        int max = Mathf.Max(y1, y2);
        // run column
        for (int y = min; y <= max; y++) {
            if (manglogic[x,y] == 2) { // if see barrier then die
                //Debug.Log("die: " + x + "" + y);
                return false;
            }
        }
        // not die -> success
        return true;
    }
    private bool checkLineY(int x1, int x2, int y) {
        //Debug.Log("dang checkLineY");
        int min = Mathf.Min(x1, x2);
        int max = Mathf.Max(x1, x2);
        for (int x = min; x <= max; x++) {
            if (manglogic[x,y] == 2) {
                //Debug.Log("die: " + x + "" + y);
                return false;
            }
        }
    return true;
    }

    // check in rectangle
    private int checkRectX(Item p1, Item p2) {
        // find point have y min and max
        //Debug.Log("dang checkRectX");
        Item pMinY = p1, pMaxY = p2;
        if (p1.y > p2.y) {
            pMinY = p2;
            pMaxY = p1;
        }
        for (int y = pMinY.y + 1; y < pMaxY.y; y++) {
            // check three line
            if (   
                    checkLineX(pMinY.y, y, pMinY.x)
                    && checkLineY(pMinY.x, pMaxY.x, y)
                    && checkLineX(y, pMaxY.y, pMaxY.x)
            ) 
            {
            // if three line is true return column y
            return 1;
            }
        }
        // have a line in three line not true then return -1
        return -1;
    }

    private int checkRectY(Item p1, Item p2) {
        //Debug.Log("dang checkReactY");
        // find point have y min
        Item pMinX = p1, pMaxX = p2;
        if (p1.x > p2.x) {
            pMinX = p2;
            pMaxX = p1;
        }
        // find line and y begin
        for (int x = pMinX.x + 1; x < pMaxX.x; x++) {
            if (
                checkLineY(pMinX.x, x, pMinX.y)
                && checkLineX(pMinX.y, pMaxX.y, x)
                && checkLineY(x, pMaxX.x, pMaxX.y)
            ) {
            return 1;
            }
        }
        return -1;
    }

    private int checkMoreLineX(Item p1, Item p2, int type){
        //Debug.Log("dang checkMoreLineX");
        // find point have y min
        Item pMinY = p1, pMaxY = p2;
        if (p1.y > p2.y) {
            pMinY = p2;
            pMaxY = p1;
        }
        // find line and y begin
        int y = pMaxY.y;
        int row = pMinY.x;
        if (type == -1) {
            y = pMinY.y;
            row = pMaxY.x;
        }
        // check more
        if (checkLineX(pMinY.y, pMaxY.y, row)) {
            while (manglogic[pMinY.x,y] != 2 && manglogic[pMaxY.x,y] != 2) {
                if (checkLineY(pMinY.x, pMaxY.x, y)) {
                    return y;
                }
                y += type;
                if(y > manglogic.GetLength(1) && (y<0)){
                    return -1;
                }
            }
        }
        return -1;
    }

    private int checkMoreLineY(Item p1, Item p2, int type) {
        //Debug.Log("dang checkMoreLineY");
        Item pMinX = p1, pMaxX = p2;
        if (p1.x > p2.x) {
            pMinX = p2;
            pMaxX = p1;
        }
        int x = pMaxX.x;
        int col = pMinX.y;
        if (type == -1) {
            x = pMinX.x;
            col = pMaxX.y;
        }
        if (checkLineY(pMinX.x, pMaxX.x, col)) {
            while (manglogic[x,pMinX.y] != 2 && manglogic[x,pMaxX.y] != 2) {
                 //while (manglogic[pMinY.x,y] != 2 && manglogic[pMaxY.x,y] != 2) {
                if (checkLineX(pMinX.y, pMaxX.y, x)) {
                    return x;
                }
                x += type;
                if(x > manglogic.GetLength(0) && (x<0)){
                    return -1;
                }
            }
        }
        return -1;
    }

    private void printLogicArray(){
        string h = "";
        for(int x = 0; x < manglogic.GetLength(0); x++){
            for(int y = 0; y< manglogic.GetLength(1); y++){
                h+=manglogic[x,y];
            }
            h+="\n";
        }
        Debug.Log(h);
    }

    //lv2
    private IEnumerator DecreaseRowCo(){
        yield return new WaitForSeconds(.2f);
        int nullCounter = 0;
        for(int x=0; x < manglogic.GetLength(0); x++){
            for(int y=0 ; y < manglogic.GetLength(1); y++){
                if(manglogic[x,y] == 5){
                    nullCounter++;
                }else if( nullCounter > 0){
                    var itemPikachu = GameObject.Find("item"+ x + "_"+ y);
                    Item item = itemPikachu.GetComponent<Item>();
                    float itemY = itemPikachu.transform.position.y - nullCounter; 
                    itemPikachu.transform.position = new Vector2(x,itemY);
                    item.UpdatePosition(x, (int)itemY);
                    itemPikachu.name = "item"+ x + "_"+ itemY;
                    manglogic[x,y-nullCounter] = manglogic[x,y];
                    manglogic[x,y] = 5;
                }
            }
            nullCounter = 0;
        }
    }

    //lv3
    private IEnumerator IncreaseRowCo(){
        yield return new WaitForSeconds(.2f);
        int nullCounter = 0;
        for(int x=manglogic.GetLength(0)-1; x >=0 ; x--){
            for(int y=manglogic.GetLength(1)-1 ; y >=0 ; y--){
                if(manglogic[x,y] == 5){
                    nullCounter--;
                }else if( nullCounter < 0){
                    var itemPikachu = GameObject.Find("item"+ x + "_"+ y);
                    Item item = itemPikachu.GetComponent<Item>();
                    float itemY = itemPikachu.transform.position.y - nullCounter; 
                    itemPikachu.transform.position = new Vector2(x,itemY);
                    item.UpdatePosition(x, (int)itemY);
                    itemPikachu.name = "item"+ x + "_"+ itemY;
                    manglogic[x,y-nullCounter] = manglogic[x,y];
                    manglogic[x,y] = 5;
                }
            }
            nullCounter = 0;
        }
    }
}
