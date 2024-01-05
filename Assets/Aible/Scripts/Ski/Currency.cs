using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Currency
{
    public class Currency : MonoBehaviour
    {
        public static int numberOfCoins;

        public TextMeshProUGUI coinsText;

        static bool changeCoinAmount = true;

        private void Awake()
        {
            numberOfCoins = PlayerPrefs.GetInt("numberOfCoins", 0);
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
            PlayerPrefs.SetInt("numberOfCoins", numberOfCoins);
            changeCoinAmount = true;
        }

        public static void ResetCoins()
        {
            numberOfCoins = 0;
            PlayerPrefs.SetInt("numberOfCoins", numberOfCoins);
            changeCoinAmount = true;
        }
    }
}