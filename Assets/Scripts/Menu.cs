using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject Start;
    [SerializeField] private GameObject Finish;

    [SerializeField] private LevelGeneration generation;

    public void SelectLevel1()
    {
        Start.SetActive(false);
        generation.InstantiatePrefabs(0);
    }
    public void SelectLevel2()
    {
        Start.SetActive(false);
        generation.InstantiatePrefabs(1);
    }
    public void SelectLevel3()
    {
        Start.SetActive(false);
        generation.InstantiatePrefabs(2);
    }
    public void SelectLevel4()
    {
        Start.SetActive(false);
        generation.InstantiatePrefabs(3);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
