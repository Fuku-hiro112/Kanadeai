using UnityEngine;

// UI操作系クラスの窓口枠クラス　依存関係を単純化する
public class UIManager : MonoBehaviour
{
    // ボタンシステムは「はい・いいえ」に割り振るクラス
    [SerializeField]private ButtonSystem _buttonSystem;// 他にもBotton操作系を作った場合区別したいためSerialezeで取ってくるようにしている
    // 文字を表示させるクラス
    private DialogSystem _dialogSystem;
    // フェードさせるクラス
    private Fade _fade;　　　　　　　　

    public DialogSystem DialogSystem { get => _dialogSystem;}
    public ButtonSystem ButtonSystem { get => _buttonSystem;}
    public Fade Fade { get => _fade;}

    void Start()
    {
        _fade = new Fade();
        _dialogSystem = GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>();
    }
}
