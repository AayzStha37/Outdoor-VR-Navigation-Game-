using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RetreiveAndShowPartcipantDetail : MonoBehaviour
{
    private SceneDataTransfer dataTransfer;
    
    public TMP_InputField participantIdInputField;
    void Start()
    {
        dataTransfer = FindObjectOfType<SceneDataTransfer>();
        if (dataTransfer != null)
            participantIdInputField.text = dataTransfer.GetTransferredText(); // Display the transferred text
    }
}
