using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// MuseClick処理
public class MouseClick : MonoBehaviour
{
    public void HandolUpdate()
    {
        ClickMouse();
    }
    /// <summary>
    /// マウスクリックしたとき
    /// </summary>
    private void ClickMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, LayerMask.GetMask("EventPoint"));
            AudioManager.Instance.PlaySE(SESoundData.SE.Click);
            if (hit.collider != null)
            {
                IClickAction clickAction = hit.collider.gameObject.GetComponent<IClickAction>();
                clickAction?.ClickAction();// clickActionがnullの場合実行しない
            }
            else return;
        }
    }
}
