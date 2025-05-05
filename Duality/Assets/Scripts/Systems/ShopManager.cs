using System;
using Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
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
        private float _attackStatIncrement;
        private float _defenceStatIncrement;
        
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
            attackCardInfo[0].text = RandomiseAttackUpgrade()[0];
            attackCardInfo[1].text = RandomiseAttackUpgrade()[1];
            
            TextMeshProUGUI[] defenceCardInfo = defenceCard.GetComponentsInChildren<TextMeshProUGUI>();
            defenceCardInfo[0].text = RandomiseDefenceUpgrade()[0];
            defenceCardInfo[1].text = RandomiseDefenceUpgrade()[1];
        }

        private string[] RandomiseAttackUpgrade()
        {
            string[] texts = new string[2];
            
            _attackUpgrade = (AttackUpgradeType) Random.Range(0, 2);
            _attackStatIncrement = Random.Range(statIncrementMin, statIncrementMax);
            
            texts[0] = _attackUpgrade == AttackUpgradeType.FireRate 
                ? "Increase fire rate by " + Math.Round(_attackStatIncrement, 1)
                : "Increase pellet force by " + Math.Round(_attackStatIncrement, 1);
            float cost = _attackStatIncrement * statPriceMultiplier;
            texts[1] = Math.Round(cost, 1) + " mana";

            return texts;
        }
        
        private string[] RandomiseDefenceUpgrade()
        {
            string[] texts = new string[2];
            
            _defenceUpgrade = (DefenceUpgradeType) Random.Range(0, 2);
            _defenceStatIncrement = _defenceUpgrade == DefenceUpgradeType.MaxHealth
                ? Random.Range(healthIncrementMin, healthIncrementMax)
                : Random.Range(statIncrementMin, statIncrementMax);
            
            texts[0] = _defenceUpgrade == DefenceUpgradeType.MaxHealth 
                ? "Increase max health by " + Math.Round(_defenceStatIncrement, 0) 
                : "Increase range size by " + Math.Round(_defenceStatIncrement, 1);
            
            float multiplier = _defenceUpgrade == DefenceUpgradeType.MaxHealth ? healthStatPriceMultiplier : statPriceMultiplier;
            
            float cost = _defenceStatIncrement * multiplier;
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
