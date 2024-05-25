using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class TargetInteractionDataAnalyzer : MonoBehaviour
{
    private  string folderPath = @"D:\GEM Lab\GONVI\Outdoor-VR-Navigation-Game-\Assets\Resources\POS LOG Renderer files from user study"; // Path to the folder with positional data files
    public string folderName = ""; // Folder name within the specified path

    private List<Vector3> origins;           // List of origin positions
    private List<float> timeSpentWithinRadius;
    private List<float> closestDistances;
    private List<int> frameCounts;

    private float radius;  // Radius in Unity units

    void Start()
    {
        // origins = new List<Vector3>
        // {
        //     new Vector3(-282.920013f,-5.73999977f,61.4900017f), // Origin 1
        //     new Vector3(-346.26001f,-5.73999977f,57.4799995f), // Origin 2
        //     new Vector3(-363.869995f,-5.73999977f,30.9099998f)  // Origin 3
        // };

        origins = new List<Vector3>
        {
            // Define your origin positions for folder 3.1 here
            new Vector3(-219.529999f,-5.73999977f,-2.03999996f), // Origin 1
            new Vector3(-260.559998f,-5.73999977f,3.76999998f) // Origin 2
        };

        // Calculate radius based on the distance between Origin 2 and the specified point
        Vector3 referenceOrigin = new Vector3(-346.26001f,-5.73999977f,57.4799995f);
        Vector3 targetPoint = new Vector3(-335.190002f,-5.73999977f,57.1100006f); // Given point

        radius = Vector3.Distance(referenceOrigin, targetPoint);

        // Get all files in the specified folder
        string fullFolderPath = Path.Combine(folderPath, folderName);
        string[] files = Directory.GetFiles(fullFolderPath, "*.txt");

        // Process each file
        foreach (var file in files)
        {
            // Initialize lists for each file
            timeSpentWithinRadius = new List<float>(new float[origins.Count]);
            closestDistances = new List<float>(new float[origins.Count]);
            frameCounts = new List<int>(new int[origins.Count]);

            // Set initial closest distances to a high value
            for (int i = 0; i < closestDistances.Count; i++)
            {
                closestDistances[i] = float.MaxValue;
            }

            List<Vector3> positions = ReadPositionsFromFile(file);

            // Process each position
            foreach (var position in positions)
            {
                ProcessPosition(position);
            }

            // Output results to the console
            OutputResults(file);
        }
    }

    void ProcessPosition(Vector3 position)
    {

        for (int i = 0; i < origins.Count; i++)
        {
            Vector3 origin = origins[i];

            // Calculate the distance from the origin
            float distance = Vector3.Distance(origin, position);

            // Update the closest distance if this position's distance is closer
            if (distance < closestDistances[i])
            {
                closestDistances[i] = distance;
            }

            // Check if the user is within the radius
            if (distance <= radius)
            {
                // Increment time spent within the radius
                timeSpentWithinRadius[i] += Time.deltaTime;
                frameCounts[i]++;
            }
        }
    }

    List<Vector3> ReadPositionsFromFile(string filePath)
    {
        List<Vector3> positions = new List<Vector3>();

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.StartsWith("POSLOG: Task") && !string.IsNullOrWhiteSpace(line))
                    {
                        string[] values = line.Split(',');
                        float x = 0f, y = 0f, z = 0f;

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
                        positions.Add(new Vector3(x, y, z));
                    }
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError("The file could not be read:");
            Debug.LogError(e.Message);
        }

        return positions;
    }

void OutputResults(string fileName)
{
    // Extract the filename without extension
    string fileNameOnly = Path.GetFileNameWithoutExtension(fileName);

    // Split the filename by underscores
    string[] fileNameParts = fileNameOnly.Split('_');

    // Check if there are at least three parts (e.g., "PositionData_filenumber_PARSE_6")
    if (fileNameParts.Length >= 3)
    {
        // Extract the file number from the second part
        string fileNumber = fileNameParts[1];

        // Format the file number as "P<filenumber>"
        string formattedFileName = "P" + fileNumber;

        // Create a new CSV file path
        string csvFilePath = Path.Combine(folderPath, "Results.csv");

        // Check if the CSV file already exists
        bool appendHeader = !File.Exists(csvFilePath);

        // Open the CSV file for writing (append mode)
        using (StreamWriter sw = new StreamWriter(csvFilePath, true))
        {
            // Write header if the file is newly created
            if (appendHeader)
            {
                sw.WriteLine("Filename, Origin, Closest Distance, Time Within Radius (seconds), Frames Within Radius");
            }

            // Write results for each origin
            for (int i = 0; i < origins.Count; i++)
            {
                // Write a line for each origin
                sw.WriteLine($"{formattedFileName}, Origin {i + 1}, {closestDistances[i]}, {timeSpentWithinRadius[i]}, {frameCounts[i]}");
            }
        }

        // Log a message indicating successful save
        Debug.Log("Results saved to CSV file: " + csvFilePath);
    }
    else
    {
        Debug.LogError("Filename does not match expected pattern.");
    }
}


    void OnDrawGizmos()
    {
        if (origins == null) return;

        Gizmos.color = Color.red;

        foreach (var origin in origins)
        {
            DrawCircle(origin, radius);
        }
    }

    void DrawCircle(Vector3 center, float radius)
    {
        int segments = 100;
        float angle = 0f;
        Vector3 lastPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        for (int i = 0; i <= segments; i++)
        {
            angle = i * (2f * Mathf.PI / segments);
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            Gizmos.DrawLine(lastPoint, nextPoint);
            lastPoint = nextPoint;
        }
    }
}