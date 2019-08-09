using System;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace XLuaFramework
{
    public class TestLua : MonoBehaviour
    {
        public Button _btn;
        [SerializeField] private TextAsset _textAsset;
        [SerializeField] private TextAsset _textAsset1;

        private void Start()
        {
            _btn.onClick.AddListener(TestEvent);
            PromotionGiftHelper.RefreshData = _textAsset1.text;
        }

        private void TestEvent()
        {
            var jsonData = JsonMapper.ToObject(_textAsset.text);
            LuaHelper.ExecuteEvent(jsonData["events"]);
        }
    }
}