using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField] private Sprite Selected;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private int number;
    private static int latestNumber = 0;

    [SerializeField] private Text numberText;

    [SerializeField] public GameController controller;
    [SerializeField] public RopeManager ropeManager;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //Counts objects numbers and prints it on screen
        if (number == 0)
        {
            latestNumber++;
            number = latestNumber;
            controller.last++;
        }

        numberText.text = number.ToString();
    }
    void Update()
    {
        //Input for phone
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Creates ray on touch
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            //Checks if ray hit an object
            if(Physics.Raycast(ray , out hit))
            {
                //Checks if hit object is a button
                if (hit.transform.tag == "Button")
                {
                    if (ropeManager.isAnimating)
                        return;

                    //Checks if legal button to press
                    if (number == controller.count + 1)
                    {
                        spriteRenderer.sprite = Selected;
                        numberText.CrossFadeAlpha(0.0f, 0.2f, false);
                        controller.count++;

                        //Saves first object to RopeManager
                        if (ropeManager.previousObj == null)
                        {
                            ropeManager.startObj = this.transform;
                            ropeManager.previousObj = this.transform;
                            ropeManager.savedObj = this.transform;
                        }
                        //Saves other objects to RopeManager and creates lines between them
                        else
                        {
                            ropeManager.nextObj = this.transform;
                            ropeManager.CreateLine();
                        }
                    }
                    //Resets numbers after finishing game
                    if (number == controller.last)
                    {
                        latestNumber = 0;
                    }
                }
            }
        }
    }

    //Testing on PC
    private void OnMouseDown()
    {
        if (ropeManager.isAnimating)
            return;

        if (number == controller.count + 1)
        {
            spriteRenderer.sprite = Selected;
            numberText.CrossFadeAlpha(0.0f, 0.2f, false);
            controller.count++;

            if (ropeManager.previousObj == null)
            {
                ropeManager.startObj = this.transform;
                ropeManager.previousObj = this.transform;
                ropeManager.savedObj = this.transform;
            }
            else
            {
                ropeManager.nextObj = this.transform;
                ropeManager.CreateLine();
            }
        }
        if (number == controller.last)
        {
            latestNumber = 0;
        }
    }
}
