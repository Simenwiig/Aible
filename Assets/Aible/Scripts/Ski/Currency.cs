using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Currency
{
    public class Currency : MonoBehaviour
    {
        public static int numberOfCoins;

        public static int currency;

        public TextMeshProUGUI coinsText;
        public TextMeshProUGUI currencyText;

        static bool changeCoinAmount = true;

        private void Start()
        {
            numberOfCoins = 0;
            coinsText.text = numberOfCoins.ToString();
        }
           
        private void Update()
        {
            if (changeCoinAmount)
            {
                coinsText.text = numberOfCoins.ToString();
                changeCoinAmount = false;
            }
        }

        public static void AddCoins(int coins)
        {
            numberOfCoins += coins;
            changeCoinAmount = true;
        }

        public static void ResetCoins()
        {
            numberOfCoins = 0;
            currency = 0;
            PlayerPrefs.SetInt("currency", currency);
            changeCoinAmount = true;
        }

        public static void AddCoinsToCurrency()
        {
            currency += numberOfCoins;
            PlayerPrefs.SetInt("currency", currency);
        }
    }
}