using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class ServerConnection : MonoBehaviour
{
    public void SendDataToServer(string collisionStatus, string textureName)
    {
       string preparedJSONData = PrepareJSONDataToSend(collisionStatus,textureName);
       StartCoroutine(PostToServer(preparedJSONData)); 
    }

    private string PrepareJSONDataToSend(string collisionStatus, string textureName)
    {
        string jsonDataToSend;

        if (string.IsNullOrWhiteSpace(textureName))
        {
            jsonDataToSend = JsonConvert.SerializeObject(new
            {
                CollisionStatus = collisionStatus,
                TextureName = ""
            });
        }
        else
        {
            jsonDataToSend = JsonConvert.SerializeObject(new
            {
                CollisionStatus = collisionStatus,
                TextureName = textureName
            });
        }

        return jsonDataToSend;
    }

    IEnumerator PostToServer(String jsonDataToSend)
    {
        string url = "http://127.0.0.1:5000/predict"; // Replace with your server's IP and port

        UnityWebRequest request = UnityWebRequest.Post(url, jsonDataToSend);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonDataToSend);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest(); 

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Server Error: " + request.error);
        }
        else
        {
            Debug.Log("Server Response: " + request.downloadHandler.text);
            // Process the prediction received from the server
        }
    }

}
