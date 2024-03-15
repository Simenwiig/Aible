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
        static bool changeCurrencyAmount;

        private void Start()
        {
            numberOfCoins = 0;
            coinsText.text = numberOfCoins.ToString();
            currency = PlayerPrefs.GetInt("currency", 0);
        }
           
        private void Update()
        {
            if (changeCoinAmount)
            {
                coinsText.text = numberOfCoins.ToString();
                changeCoinAmount = false;
            }

            if (changeCurrencyAmount)
            {
                StartCoroutine(CoinsToCurrency());
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
            changeCurrencyAmount = true;  
        }

        IEnumerator CoinsToCurrency()
        {
            changeCurrencyAmount = false;

            int coins = numberOfCoins;

            for (int i = 0; i < coins; i++)
            {
                currency++;
                numberOfCoins--;
                currencyText.text = currency.ToString();
                coinsText.text = numberOfCoins.ToString();
                yield return new WaitForSeconds(0.1f);
            }

            PlayerPrefs.SetInt("currency", currency);
        }
    }
}