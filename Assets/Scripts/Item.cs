using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int x;
    public int y;
    public PikachuItem pikachuItem = null;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spriteRendererNen;
    public SpriteRenderer spriteRendererMain;
    private void Awake() {
        spriteRendererMain = gameObject.GetComponent<SpriteRenderer>();
    }
    
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
        // check double item
        if(GameManager.Instance.itemAnswers.Contains(this)) return;
        //check xem no phai tuong khong
        if(this.pikachuItem.icon == null) return;
        spriteRendererMain.color = Color.red;
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
    public void DisableRender(){
        spriteRendererNen.sprite = null;
        spriteRendererMain.sprite = null;
    }
}
