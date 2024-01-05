using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Currency
{
    public class CoinPickup : MonoBehaviour
    {
        [SerializeField] ParticleSystem normalCoin;
        [SerializeField] ParticleSystem grabCoin;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Player")
            {
                Currency.AddCoins(1);
                StartCoroutine(GrabCoin());
            }
        }

        IEnumerator GrabCoin()
        {
            Destroy(normalCoin.gameObject);
            yield return new WaitForSeconds(0.01f);
            grabCoin.Play();

            yield return new WaitForSeconds(1.5f);

            Destroy(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Currency.AddCoins(1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Currency.ResetCoins();
            }
        }
    }
}