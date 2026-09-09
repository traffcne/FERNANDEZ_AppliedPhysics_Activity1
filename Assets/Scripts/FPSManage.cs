using UnityEngine;

public class FPSManage : MonoBehaviour
{
    void Awake() {
    QualitySettings.vSyncCount = 0;      // without this, the next line does nothing
    //Application.targetFrameRate = 30;
    //Application.targetFrameRate = 60;
    Application.targetFrameRate = 144;
}
}
