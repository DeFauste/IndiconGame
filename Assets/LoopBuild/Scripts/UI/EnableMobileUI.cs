using UnityEngine;

namespace Assets.Scripts.UI
{
    public class EnableMobileUI : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (SystemInfo.deviceType != DeviceType.Handheld)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
