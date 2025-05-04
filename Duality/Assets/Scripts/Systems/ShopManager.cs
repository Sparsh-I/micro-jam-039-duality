using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Systems
{
    public class ShopManager : MonoBehaviour
    {
        [Header("Shop UI Elements")] [SerializeField]
        private Button[] shopButtons;

        [SerializeField] private ManaSystem manaSystem;

        private void Start()
        {
            foreach (Button button in shopButtons) button.gameObject.SetActive(false);
            manaSystem.StartManaTimer();
        }

        public void OpenShop()
        {
            foreach (Button button in shopButtons) button.gameObject.SetActive(true);
            manaSystem.StopManaTimer();
        }

        public void CloseShop()
        {
            foreach (Button button in shopButtons) button.gameObject.SetActive(false);
            manaSystem.StartManaTimer();
        }
    }
}
