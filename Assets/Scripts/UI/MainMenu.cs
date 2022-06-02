using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct LevelData
{
    public string description;
    public string name;
}

public class MainMenu : MonoBehaviour
{
    public GameObject sceneLoader; // Prefab with this script and the loadingscreen
    [SerializeField] LevelData[] levels;
    LevelData level;
    [SerializeField] TMP_Text description;

    // Start is called before the first frame update
    void Start()
    {
        level = levels[0];
        UpdateLevel();
    }

    public void ChangeLevel(string name)
    {
        foreach (LevelData item in levels)
        {
            if (item.name == name)
            {
                level = item;
                UpdateLevel();
                return;
            }
        }
    }

    void UpdateLevel()
    {
        description.text = level.description;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LaunchLevel()
    {
        GameObject newObject = Instantiate(sceneLoader, Vector3.zero, Quaternion.identity);

        newObject.GetComponent<SceneLoader>().Init(level.name);
    }
}
