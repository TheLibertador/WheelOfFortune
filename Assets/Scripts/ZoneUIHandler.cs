using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bronzeZone;
    [SerializeField] private GameObject _silverZone;
    [SerializeField] private GameObject _goldZone;

    private void OnEnable()
    {
        
        GameManager.OnZoneChanged += HandleZoneChanged;
    }

    private void OnDisable()
    {
       
        GameManager.OnZoneChanged -= HandleZoneChanged;
    }

    private void ActivateBronzeZone()
    {
        if (!_bronzeZone.activeInHierarchy)
        {
            _bronzeZone.SetActive(true);
            _silverZone.SetActive(false);
            _goldZone.SetActive(false);
        }
    }

    private void ActivateSilverZone()
    {
        if (!_silverZone.activeInHierarchy)
        {
            _silverZone.SetActive(true);
            _bronzeZone.SetActive(false);
            _goldZone.SetActive(false);
        }
    }

    private void ActivateGoldZone()
    {
        if (!_goldZone.activeInHierarchy)
        {
            _goldZone.SetActive(true);
            _silverZone.SetActive(false);
            _bronzeZone.SetActive(false);
        }
    }

    private void HandleZoneChanged(GameManager.Zone newZone)
    {
        switch (newZone)
        {
            case GameManager.Zone.Bronze:
                ActivateBronzeZone();
                break;
            case GameManager.Zone.Silver:
                ActivateSilverZone();
                break;
            case GameManager.Zone.Gold:
                ActivateGoldZone();
                break;
        }
    }
}
