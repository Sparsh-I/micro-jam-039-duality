using System;
using System.Diagnostics;
using Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Systems
{
    public enum AttackUpgradeType
    {
        FireRate,
        PelletForce
    };

    public enum DefenceUpgradeType
    {
        MaxHealth,
        RangeSize
    }

    public class ShopManager : MonoBehaviour
    {
        [Header("Shop UI Elements")]
        [SerializeField] private Button attackCard;
        [SerializeField] private Button defenceCard;

        private GameObject _shopUI;
        
        [SerializeField] private ManaSystem manaSystem;

        private AttackUpgradeType _attackUpgrade;
        private DefenceUpgradeType _defenceUpgrade;
        
        [Header("Stat Increments")]
        private double _attackStatIncrement;
        private double _defenceStatIncrement;
        
        [Tooltip("Minimum (inclusive) stat increase for bought cards")] [SerializeField]
        private float statIncrementMin;
        
        [Tooltip("Maximum (exclusive) stat increase for bought cards")] [SerializeField]
        private float statIncrementMax;
        
        [Tooltip("Minimum (inclusive) health increase for bought cards")] [SerializeField]
        private float healthIncrementMin;
        
        [Tooltip("Maximum (exclusive) health increase for bought cards")] [SerializeField]
        private float healthIncrementMax;

        [Header("Price Multipliers")] 
        [SerializeField] private float statPriceMultiplier;
        [SerializeField] private float healthStatPriceMultiplier;
        
        [Header("References")]
        [SerializeField] private PointSystem pointSystem;

        private void Start()
        {
            _shopUI = GameObject.FindGameObjectWithTag("ShopUI");
            _shopUI.SetActive(false);
            
            attackCard.gameObject.SetActive(false);
            defenceCard.gameObject.SetActive(false);
            
            manaSystem.StartManaTimer();
        }

        public void OpenShop()
        {
            _shopUI.SetActive(true);
            
            attackCard.gameObject.SetActive(true);
            defenceCard.gameObject.SetActive(true);

            RandomiseUpgrades();
            
            attackCard.GetComponent<Button>().onClick.RemoveAllListeners();
            attackCard.GetComponent<Button>().onClick.AddListener(ApplyAttackUpgrade);

            defenceCard.GetComponent<Button>().onClick.RemoveAllListeners();
            defenceCard.GetComponent<Button>().onClick.AddListener(ApplyDefenceUpgrade);
            
            manaSystem.StopManaTimer();
        }

        public void CloseShop()
        {
            _shopUI.SetActive(false);
            
            attackCard.gameObject.SetActive(false);
            defenceCard.gameObject.SetActive(false);
            
            manaSystem.StartManaTimer();
        }

        private void RandomiseUpgrades()
        {
            TextMeshProUGUI[] attackCardInfo = attackCard.GetComponentsInChildren<TextMeshProUGUI>();
            string[] attackTexts = RandomiseAttackUpgrade();
            attackCardInfo[0].text = attackTexts[0];
            attackCardInfo[1].text = attackTexts[1];
            
            TextMeshProUGUI[] defenceCardInfo = defenceCard.GetComponentsInChildren<TextMeshProUGUI>();
            string[] defenceTexts = RandomiseDefenceUpgrade();
            defenceCardInfo[0].text = defenceTexts[0];
            defenceCardInfo[1].text = defenceTexts[1];
        }

        private string[] RandomiseAttackUpgrade()
        {
            string[] texts = new string[2];
            
            _attackUpgrade = (AttackUpgradeType) Random.Range(0, 2);
            _attackStatIncrement = Math.Round(Random.Range(statIncrementMin, statIncrementMax), 1);
            
            texts[0] = _attackUpgrade == AttackUpgradeType.FireRate 
                ? "Increase fire rate by " + _attackStatIncrement
                : "Increase pellet force by " + _attackStatIncrement;
            double cost = _attackStatIncrement * statPriceMultiplier;
            texts[1] = Math.Round(cost, 1) + " mana";

            return texts;
        }
        
        private string[] RandomiseDefenceUpgrade()
        {
            string[] texts = new string[2];
            
            _defenceUpgrade = (DefenceUpgradeType) Random.Range(0, 2);
            _defenceStatIncrement = _defenceUpgrade == DefenceUpgradeType.MaxHealth
                ? Math.Round(Random.Range(healthIncrementMin, healthIncrementMax), 0)
                : Math.Round(Random.Range(statIncrementMin, statIncrementMax), 1);
            
            texts[0] = _defenceUpgrade == DefenceUpgradeType.MaxHealth 
                ? "Increase max health by " + _defenceStatIncrement 
                : "Increase range size by " + _defenceStatIncrement;
            
            float multiplier = _defenceUpgrade == DefenceUpgradeType.MaxHealth ? healthStatPriceMultiplier : statPriceMultiplier;
            Debug.Log("Text: " + texts[0]);
            Debug.Log("Multiplier: " + multiplier);
            Debug.Log("StatIncrement: " + _defenceStatIncrement);
            double cost = _defenceStatIncrement * multiplier;
            texts[1] = Math.Round(cost, 1) + " mana";

            return texts;
        }

        public void ApplyAttackUpgrade()
        {
            pointSystem.AddAttackPoint(_attackUpgrade, _attackStatIncrement, attackCard.gameObject);
        }

        public void ApplyDefenceUpgrade()
        {
            pointSystem.AddDefencePoint(_defenceUpgrade, _defenceStatIncrement, defenceCard.gameObject);
        }
    }
}
