using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;

    public GameController gameController;
    public RopeManager ropeManager;
    [SerializeField] private Transform parent;

    [SerializeField] private TextAsset jsonFile;
    private LevelContainer levelContainer;

    void Start()
    {
        LoadLevelsFromJson();
    }

    //Saving coordinates
    private void LoadLevelsFromJson()
    {
        levelContainer = JsonUtility.FromJson<LevelContainer>(jsonFile.text);
    }

    public void InstantiatePrefabs(int levelIndex)
    {
        //Checks if existing level data
        if (levelIndex >= 0 && levelIndex < levelContainer.levels.Length)
        {
            LevelData levelData = levelContainer.levels[levelIndex];
            //x and y coordinates for objects
            for (int j = 0; j < levelData.level_data.Length; j += 2)
            {
                float x = float.Parse(levelData.level_data[j]);
                float y = float.Parse(levelData.level_data[j + 1]);

                //Fixing coordinates to Unity
                x = x - 500;
                y = 500 - y;

                //Creates buttons for level
                GameObject prefabInstance = Instantiate(buttonPrefab, new Vector2(x, y), Quaternion.identity, parent);

                //Adds scripts to each created button
                Button buttonScript = prefabInstance.GetComponent<Button>();
                if (buttonScript != null)
                {
                    buttonScript.controller = gameController;
                    buttonScript.ropeManager = ropeManager;
                }
            }
        }
        //Not existing level
        else
        {
            Debug.LogError("Invalid level index!");
        }
    }
}

//Stores coordinates for each level
[System.Serializable]
public class LevelData
{
    public string[] level_data;
}

//Stores level amount
[System.Serializable]
public class LevelContainer
{
    public LevelData[] levels;
}
