using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBase : MonoBehaviour
{
    private IPlayerController _iPlayerController;
    private UIManager _uiManager;

    // UnityのStartはoverrideをつけなくても、自動的にオーバーライドされる　自動でoverrideされるというよりは自動でnewされているっぽい
    protected void Start()// base.Start();で親のStartを呼ぶためprotected
    {
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _iPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public virtual void ClickAction() { }
}
