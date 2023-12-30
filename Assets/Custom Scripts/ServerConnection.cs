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
       StartCoroutine(PostToServer(preparedJSONData, Constants.ENDPOINT_COLLISON_REGISTER)); 
    }
    public void SendVelocityDataToServer(Vector3 velocity)
    {
       string preparedJSONData = PrepareJSONDataToSend(velocity);
       StartCoroutine(PostToServer(preparedJSONData, Constants.ENDPOINT_DFT321_PREDCICT)); 
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

    private string PrepareJSONDataToSend(Vector3 vectorData)
    {
        var dataObject = new
        {
            x = vectorData.x,
            y = vectorData.y,
            z = vectorData.z
        };

        // Serialize the object to a JSON string
        string jsonDataToSend = JsonConvert.SerializeObject(dataObject);

        return jsonDataToSend;
    }

    IEnumerator PostToServer(String jsonDataToSend, string endPoint)
    {
        string url = Constants.FLASK_SERVER_URL+endPoint;

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
