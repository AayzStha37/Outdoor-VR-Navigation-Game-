using UnityEngine;
using System.IO;

public class LoggedPositionDataPathRenderer : MonoBehaviour
{ public string positionDataFileName = "PositionData_123.txt"; // Adjust this with your file name
    public LineRenderer lineRenderer; // Reference to the LineRenderer component

    void Start()
    {
        string positionDataFilePath = @"C:\Users\Admin\Desktop\Unity logs\" + positionDataFileName;

        if (File.Exists(positionDataFilePath))
        {
            string[] lines = File.ReadAllLines(positionDataFilePath);

            // Create an empty GameObject to hold the LineRenderer
            GameObject lineObject = new GameObject("LineObject");
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

            // Set LineRenderer properties
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;

            // Create a Vector3 list to store positions
            System.Collections.Generic.List<Vector3> positionsList = new System.Collections.Generic.List<Vector3>();

            // Parse the lines to extract x and z values (assuming Z-axis as the y-value in Unity)
            foreach (string line in lines)
            {
                if (line.StartsWith("POSLOG"))
                {
                    string[] values = line.Split(',');
                    float x = 0f, y =0f, z = 0f;

                    foreach (string val in values)
                    {
                        if (val.Contains("POSLOG: X:"))
                        {
                            float.TryParse(val.Split(':')[2], out x);
                        } 
                        else if (val.Contains("Y:"))
                        {
                            float.TryParse(val.Split(':')[1], out y);
                        }
                        else if (val.Contains("Z:"))
                        {
                            float.TryParse(val.Split(':')[1], out z);
                        }
                    }

                    positionsList.Add(new Vector3(x, y, z));
                }
            }

            // Assign the positions to the LineRenderer
            lineRenderer.positionCount = positionsList.Count;
            lineRenderer.SetPositions(positionsList.ToArray());
        }
        else
        {
            Debug.LogError("Position data file not found at path: " + positionDataFilePath);
        }
    }
}