using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFrameRate : MonoBehaviour
{
    [SerializeField] private int _frameRate = 60;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = _frameRate;
        QualitySettings.vSyncCount = 0;
    }
}
