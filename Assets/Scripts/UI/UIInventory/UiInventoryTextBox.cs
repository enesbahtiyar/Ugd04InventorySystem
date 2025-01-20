using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiInventoryTextBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshTop1;
    [SerializeField] private TextMeshProUGUI textMeshTop2;
    [SerializeField] private TextMeshProUGUI textMeshTop3;
    [SerializeField] private TextMeshProUGUI textMeshBottom1;
    [SerializeField] private TextMeshProUGUI textMeshBottom2;
    [SerializeField] private TextMeshProUGUI textMeshBottom3;

    public void SetTextBoxText(string texTop1, string texTop2, string texTop3, string texBottom1, string texBottom2, string texBottom3)
    {
        textMeshTop1.text = texTop1;
        textMeshTop2.text = texTop2;
        textMeshTop3.text = texTop3;
        textMeshBottom1.text = texBottom1;
        textMeshBottom2.text = texBottom2;
        textMeshBottom3.text = texBottom3;
    }
}
