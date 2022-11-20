using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int x;
    public int y;
    public PikachuItem pikachuItem;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    public void UpdatePosition(int _x, int _y){
        x= _x;
        y= _y;
    }

    public void UpdateIcon(){
        spriteRenderer.sprite = pikachuItem.icon;
    }
    
    public string ToStringItem(){
        return $"{x}-{y}";
    }

    private void OnMouseDown() {
        //Debug.Log("Pressed - "+name);
        if(GameManager.Instance.m_isAnswerChecking) return;
        GameManager.Instance.itemAnswers.Add(this);
        if(GameManager.Instance.itemAnswers.Count == 2){
            //Debug.Log($"kiem tra {GameManager.Instance.itemAnswers[0].ToStringItem()} voi {GameManager.Instance.itemAnswers[1].ToStringItem()}");
            GameManager.Instance.m_isAnswerChecking = true;
            GameManager.Instance.Kiemtra();
            GameManager.Instance.m_isAnswerChecking = false;
            GameManager.Instance.itemAnswers.Clear();
        }
    }

    public void DestroyItem(){
        Destroy(gameObject);
    }
}
