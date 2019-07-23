using UnityEngine;

namespace MyTest
{
    public class VoicePlayTest : MonoBehaviour
    {
        readonly string assetName = "booknames";

        void Start()
        {
            VoiceMenu asset = Resources.Load<VoiceMenu>(assetName);
            foreach (Voice gd in asset.menus)
            {
                Debug.Log(gd.Name);
                Debug.Log(gd.Chinese);
                Debug.Log(gd.English);
            }
        }
    }
}