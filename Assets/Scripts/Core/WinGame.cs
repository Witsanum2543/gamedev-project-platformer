using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other) {
        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.Win();
    }
}
