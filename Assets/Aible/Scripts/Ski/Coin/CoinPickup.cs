using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Currency
{
    public class CoinPickup : MonoBehaviour
    {
        [SerializeField] ParticleSystem normalCoin;
        [SerializeField] ParticleSystem grabCoin;
        bool isGrabed;

        private void OnTriggerEnter(Collider other)
        {
            if ((other.transform.tag == "Player" || other.transform.tag == "PlayerBodyPart" )&& !isGrabed)
            {
                Currency.AddCoins(1);
                StartCoroutine(GrabCoin());
            }
        }

        IEnumerator GrabCoin()
        {
            isGrabed = true;
            Destroy(normalCoin.gameObject);
            yield return new WaitForSeconds(0.01f);
            grabCoin.Play();

            yield return new WaitForSeconds(1.5f);

            Destroy(gameObject);
        }
    }
}