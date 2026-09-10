using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SaveSlot : MonoBehaviour
{
    [Header("Required")]
    [SerializeField] private TextMeshProUGUI progressDisplay;
    [SerializeField] private TextMeshProUGUI nameDisplay;
    [SerializeField] private TextMeshProUGUI difficultyDisplay;
    [SerializeField] private TextMeshProUGUI creationDateDisplay;
    [SerializeField] private TextMeshProUGUI lastPlayedDateDisplay;
    [SerializeField] private Button renameButton;
    [SerializeField] private Button deleteButton;

    [SerializeField] private string emptyText;

    // [Header("Optional")]
    [Header("Readonly")]
    private DateTime lastPlayedDate;
    private DateTime creationDate;

    private void Start()
    {
        if (!IsFilled())
        {
            SetDisplayEmpty();
        }
        else
        {
            SetDisplayFilled();
        }

    }

    private void SetDisplayEmpty()
    {
        progressDisplay.gameObject.SetActive(false);
        difficultyDisplay.gameObject.SetActive(false);
        creationDateDisplay.gameObject.SetActive(false);
        lastPlayedDateDisplay.gameObject.SetActive(false);
        renameButton.gameObject.SetActive(false);
        deleteButton.gameObject.SetActive(false);

        nameDisplay.text = emptyText;
    }

    private void SetDisplayFilled()
    {

    }

    private bool IsFilled()
    {
        // if there's something here
        // return true;

        return false;
    }


}
