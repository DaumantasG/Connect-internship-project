using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int count = 0;
    public int last = 0;
    private bool flag = false;

    [SerializeField] private GameObject Start;
    [SerializeField] private GameObject Finish;

    [SerializeField] private Button button;
    [SerializeField] private RopeManager ropeManager;

    void Update()
    {
        //Checks if last object is clicked and animation finished playing
        if(count == last && last != 0 && !ropeManager.isAnimating)
        {
            //Draws line between last and first object
            if (!flag)
            {
                ropeManager.LastLine();
                flag = true;
            }
            //Shows Finish screen after animation is finished
            else
            {
                if (!ropeManager.isAnimating)
                    Finish.SetActive(true);
            }
        }
    }
    
    public void levelSelection()
    {
        //Destroys all objects and lines
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //Resets all numbers of objects for next level
        count = 0;
        last = 0;
        flag = false;

        Finish.SetActive(false);
        Start.SetActive(true);
    }
}
