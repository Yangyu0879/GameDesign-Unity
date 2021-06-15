using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dialogImage;
    public Text dialogText;
    public GameObject[] options;

    public string[] text;
    public string[] optionA;
    public string[] optionB;
    private bool isInRange = false;
    private int count = 0;
    private bool optionActive = false;
    private int inOptionText=-1;
   

    void Start()
    {
        dialogImage.SetActive(false);
        foreach(GameObject option in options)
        {
            option.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            dialogImage.SetActive(true);
            if (inOptionText == -1)
            {
                if (count < text.Length)
                {
                    dialogText.text = text[count];
                    count = count + 1;
                }
                else
                {
                    foreach (GameObject option in options)
                    {
                        option.SetActive(true);
                        optionActive = true;
                    }
                }
            } else if (inOptionText == 0)
            {
                if (count < optionA.Length)
                {
                    dialogText.text = optionA[count];
                    count = count + 1;
                }
                else
                {
                    foreach (GameObject option in options)
                    {
                        option.SetActive(true);
                        optionActive = true;
                    }
                }
            }
            else if (inOptionText == 1)
            {
                if (count < optionB.Length)
                {
                    dialogText.text = optionB[count];
                    count = count + 1;
                }
                else
                {
                    foreach (GameObject option in options)
                    {
                        option.SetActive(true);
                        optionActive = true;
                    }
                }
            }
        }
        if (optionActive)
        {
            if (Input.GetKeyDown(KeyCode.A)){
                inOptionText = 0;
                optionActive = false;
                count = 0;
                dialogText.text = optionA[count];
                setOptionFalse();
            } else if (Input.GetKeyDown(KeyCode.B))
            {
                inOptionText = 1;
                optionActive = false;
                count = 0;
                dialogText.text = optionB[count];
                setOptionFalse();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && 
            other.GetType().ToString()== "UnityEngine.BoxCollider2D")
        {
            count = 0;
            isInRange = true;
            optionActive = false;
            inOptionText = -1;
        }
    }

    private void setOptionFalse()
    {
        foreach (GameObject option in options)
        {
            option.SetActive(false);
        }
    } 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") &&
            other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            isInRange = false;
            dialogImage.SetActive(false);
            foreach (GameObject option in options)
            {
                option.SetActive(false);
            }
        }
    }
}
