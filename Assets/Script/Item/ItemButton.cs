using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    [SerializeField] GameObject _inventory;
    private bool ItemButtonCheck = false;

    [SerializeField]private AudioClip _se;
    AudioSource _audioSource;

    void Start()
    {
        _inventory.SetActive(false);
        _audioSource = GetComponent<AudioSource>();

    }
    public void OnClick()
    {
        if (ItemButtonCheck)
        {
            _inventory.SetActive(false);
            ItemButtonCheck = false;
        }
        else
        {
            _audioSource.PlayOneShot(_se);
            _inventory.SetActive(true);
            ItemButtonCheck = true;
        }
    }
}
