using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MonsterController : MonoBehaviour
{
    public float movementSpeed = 3f;  // 몬스터 이동 속도
    public float detectionRadius = 10f;  // 플레이어 감지 반경

    private Transform playerCamera;  // 플레이어 카메라의 Transform 컴포넌트
    private Vector3 movementDirection;  // 몬스터 이동 방향
    private Rigidbody rb;  // 몬스터 Rigidbody 컴포넌트

    private bool isGameOver = false;


    private void Start()
    {
        playerCamera = Camera.main.transform;
        rb = GetComponent<Rigidbody>();

        // 초기 이동 방향 설정
        SetRandomMovementDirection();
    }

    private void Update()
    {
        // 몬스터 이동
        Move();

        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, playerCamera.position);

        // 플레이어가 일정 반경 안에 있을 때 몬스터 추적
        
        if (distanceToPlayer <= detectionRadius)
        {
            ChasePlayer();
        }
        

         // 1인칭 시점의 플레이어가 몬스터와 충돌하면 게임 오버
        if (!isGameOver && Vector3.Distance(transform.position, playerCamera.position) <= 1f)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        // 게임 오버 처리
        isGameOver = true;
        Debug.Log("Game Over!");
        // 게임 오버 시 원하는 동작을 여기에 추가할 수 있습니다.
    }

    private void Move()
    {
        // 벽과의 충돌 감지를 위해 이동 벡터를 계산
        Vector3 movementVector = movementDirection * movementSpeed * Time.deltaTime;

        // 몬스터 이동
        rb.MovePosition(rb.position + movementVector);
    }

    private void ChasePlayer()
    {
        // 플레이어 방향으로 몬스터를 회전시킵니다.
        transform.LookAt(playerCamera);

        // 플레이어를 향해 이동합니다.
        rb.MovePosition(rb.position + transform.forward * movementSpeed * Time.deltaTime);
    }

    private void SetRandomMovementDirection()
    {
        // 랜덤한 이동 방향 설정
        movementDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 벽과 충돌한 경우 이동 방향을 반대로 설정하여 벽을 통과하지 않도록 함
    if (collision.gameObject.CompareTag("Wall"))
    {
        // 현재 y축 방향을 유지한 새로운 이동 방향 벡터를 계산
        Vector3 newMovementDirection = new Vector3(-movementDirection.x, 0f, -movementDirection.z);
        movementDirection = newMovementDirection.normalized;
    }

      // 1인칭 시점의 플레이어와 충돌하면 게임 오버
      /*
    if (collision.gameObject.CompareTag("Player"))
    {
        GameOver();
    }
    */

       
    }

    private void GoToNextScene()
    {
        // 다음 씬으로 이동
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No next scene available. Game Over!");
            // 다음 씬이 없을 경우 게임 오버 또는 다른 동작을 수행할 수 있습니다.
        }
    }
}