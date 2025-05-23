using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
     public int chosenSkinId;
    public static SkinManager Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void setSkinId(int id)
    {
        chosenSkinId = id;

    }

    public int getSkinId()
    {
        return chosenSkinId;
    }
}
