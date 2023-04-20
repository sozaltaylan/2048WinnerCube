using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaylanGame.controllers;

public class AimController : MonoBehaviour
{
    [SerializeField] private InputController inputController;

    private void Update()
    {
        CloseAim();
    }

    public void CloseAim()
    {
        if (inputController._isMouseUp)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
