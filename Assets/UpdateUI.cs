using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI targetText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.UpdateUIText.AddListener(UpdateUIText);
    }

    private void UpdateUIText(int targetValue)
    {
        targetText.text = targetValue.ToString();
    }
}
