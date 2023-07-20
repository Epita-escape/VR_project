using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    //public GameObject coinPrefab;  // 코인 프리팹
    public int coinsToCollect = 5;  // 비활성화할 코인의 개수
    public TextMeshPro messageText;  // 메시지를 표시할 UI 텍스트
    public GameObject door;
    public GameObject box;

    private int collectedCoins = 0;  // 수집한 코인의 개수

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered chest");
        if (other.CompareTag("Coin"))
        {
            Debug.Log("It is a coin");
            //Debug.Log("hi");
            // 상자와 충돌한 코인을 비활성화
            //other.gameObject.SetActive(false);
            Vector3 newPosition = other.gameObject.transform.position;
            newPosition.y=-300;
            other.gameObject.transform.position=newPosition;
            collectedCoins++;
            messageText.text = "Coin : " + collectedCoins;


            if (collectedCoins >= coinsToCollect)
            {
                Debug.Log("Ok tupeuxpastest");
                // 일정 개수의 코인을 수집하면 메시지 표시
                messageText.text = "Congratulations! You collected all the coins!";
                box.SetActive(false);
                door.SetActive(true);
                
            }
        } else {
            Debug.Log("It is not a coin");
        }

    }

    private void Start()
    {
        // 초기 상태 설정
        door.SetActive(false);
        collectedCoins = 0;
//        messageText.text = "Coin : " + collectedCoins;

    }



   /*rivate void ResetCoins()
    {
        // 비활성화된 코인을 다시 활성화하여 초기 상태로 되돌림
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            coin.SetActive(true);
        }
        collectedCoins = 0;
        messageText.text = "";
    }*/
}