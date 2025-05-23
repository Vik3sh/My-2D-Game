using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkinSelection : MonoBehaviour
{
    [SerializeField] private int currentIndex;
    [SerializeField] int maxIndex;
    [SerializeField] private Animator skinDisplay;



    public void selectSkin()
    {
        SkinManager.Instance.setSkinId(currentIndex);   
    }
    public void NextSkin()
    {
        currentIndex++;

        if (currentIndex > maxIndex)
        {
            currentIndex = 0;
        }
        updateSkinDisplay();
    }

    public void PreviousSkin()
    {
        currentIndex--;
        if(currentIndex < 0)
        {
            currentIndex = maxIndex;
        }
        updateSkinDisplay();
    }


    private void updateSkinDisplay()
    {
        for(int i=0;i<skinDisplay.layerCount;i++)
        {
            skinDisplay.SetLayerWeight(i, 0);
        }
        skinDisplay.SetLayerWeight(currentIndex, 1);
    }
 }
