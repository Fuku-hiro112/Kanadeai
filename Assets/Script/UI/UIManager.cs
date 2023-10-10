using UnityEngine;

// UInNXÌûgNX@Ë¶ÖWðP»·é
public class UIManager : MonoBehaviour
{
    [SerializeField]private ButtonSystem _buttonSystem;
    private DialogSystem _dialogSystem;
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
