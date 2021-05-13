using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image screenFlashImg;
    public float screenFlashTime;

    public Color flashColor;
    private Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor= screenFlashImg.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlashScreen()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        screenFlashImg.color = flashColor;
        yield return new WaitForSeconds(screenFlashTime);
        screenFlashImg.color = defaultColor;
    }
}
