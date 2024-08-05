using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instace;

    private void Awake()
    {
        if(Instace != this && Instace == null)
        { 
            Instace = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CalculateReward()
    {

    }
}
