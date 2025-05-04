using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Systems
{
    public class ShopManager : MonoBehaviour
    {
        [Header("Shop UI Elements")] [SerializeField]
        private Button nextWaveButton;

        [SerializeField] private Button attackCardButton;

        [SerializeField] private Button defenceCardButton;

        private void Start()
        {
            nextWaveButton.gameObject.SetActive(false);
            attackCardButton.gameObject.SetActive(false);
            defenceCardButton.gameObject.SetActive(false);
        }

        public void OpenShop()
        {
            nextWaveButton.gameObject.SetActive(true);
            attackCardButton.gameObject.SetActive(true);
            defenceCardButton.gameObject.SetActive(true);
        }

        public void CloseShop()
        {
            nextWaveButton.gameObject.SetActive(false);
            attackCardButton.gameObject.SetActive(false);
            defenceCardButton.gameObject.SetActive(false);
        }
    }
}
