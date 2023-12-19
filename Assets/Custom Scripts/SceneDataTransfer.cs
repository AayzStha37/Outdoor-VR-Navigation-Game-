using UnityEngine;
using TMPro;

public class SceneDataTransfer : MonoBehaviour
{
    public TMP_InputField  inputField; // Reference to the InputField UI element

    // Static variable to store text input across scenes
    public static string transferredText = "";

    public string StoreInputText()
    {
        transferredText = inputField.text;
        return inputField.text;
    }

    public string GetTransferredText()
    {
        return transferredText;
    }
}
