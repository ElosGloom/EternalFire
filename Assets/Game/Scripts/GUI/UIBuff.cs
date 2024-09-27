using System;
using Game.Scripts.Chest;
using TMPro;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class UIBuff : MonoBehaviour
    {
        public static event Action BuffEndEvent;
        [SerializeField] private float buffTime;
        [SerializeField] private TMP_Text timerText;
        private float _timerCounter;


        private void OnEnable()
        {
            _timerCounter = buffTime;
        }

        private void HideBuff()
        {
            BuffEndEvent?.Invoke();
           gameObject.SetActive(false);
        }
        private void Update()
        {
            float t = _timerCounter -= Time.deltaTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("00");

            timerText.text = minutes + ":" + seconds;

            if (_timerCounter <= 0.0f)
            {
                HideBuff();
            }
        }

    }
}