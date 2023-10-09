using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickGimmick : MonoBehaviour ,IClickAction
{
    [SerializeField] Canvas _canvas;

    public void ClickAction()
    {
        _canvas.enabled = true;

    }
}
