using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitCountText : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public void UpdateText(int value) {
        text.text = "Wait: " + value + "s";
    }
}
