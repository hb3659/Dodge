using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;     // 생성할 탄알의 원본 프리팹
    // 탄알을 생성하는 데 사용할 원본 프리팹
    public float spawnRateMin = 0.5f;   // 최소 생성 주기
    // 새 탄알을 생성하는 데 걸리는 시간의 최솟값
    public float spawnRateMax = 3f;     // 최대 생성 주기
    // 새 탄알을 생성하는 데 걸리는 시간의 최댓값

    private Transform target;           // 발사할 대상
    // 조준할 대상 게임 오브젝트의 트랜스폼 컴포넌트
    private float spawnRate;            // 생성 주기
    // 다음 탄알을 생설할 때까지 기다릴 시간
    // spawnRateMin 과 spawnRateMax 사이의 랜덤값으로 설정됨
    private float timeAfterSpawn;       // 최근 생성 시점에서 지난 시간
    // 마지막 탄알 생성 시점부터 흐른 시간을 표시하는 타이머

    // Start is called before the first frame update
    void Start()
    {
        /*
         * 시간에 관한 변수를 초기화. 또한 탄알을 발사할 목표가 될
         * 게임 오브젝트의 트랜스폼 컴포넌트를 찾아서 가져온다.
         */

        // 최근 생성 이후의 누적 시간을 0으로 초기화
        timeAfterSpawn = 0f;
        // 탄알 생성 간격을 spawnRateMin 과 spawnRatemax 사이에서 랜덤 지정
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        // Player_Controller 컴포넌트를 가진 게임 오브젝트를 찾아 조준 대상으로 설정
        target = FindObjectOfType<Player_Controller>().transform;
        /*
         * Player_Controller playerController = FindObjectOfType<Player_Controller>();
         * target = playerController.transform;
         * ===> 같은 코드
         * 
         * FindObjectType() 메서드는 씬에 존재하는 모든 오브젝트를 검색하여 원하는 타입의
         * 오브젝트를 찾아낸다. FindObjectType() 메서드는 처리비용이 크기 때문에
         * Start() 메서드처럼 초기에 한두 번 실행되는 메서드에서만 사용해야 한다.
         * 만약 Update() 메서드에서 FindObjectType()을 실행하면 프로그램이 심각하게 느려진다.
         * 
         * FindObjectType() / FindObjectsType()
         *  - FindObjectType(): 해당 타입의 오브젝트를 하나만 찾는다.
         *  - FindObjectsType() : 해당 타입의 오브젝트를 모두 찾아 배열로 반환한다.
         */
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Time.deltaTime
         *     - Update() 실행 사이의 시간 간격을 알기 위해 사용하는 내장 변수
         *     - 이전 프레임과 현재 프레임 사이의 시간 간격이 자동으로 할당
         *     - 어떤 변수에 Time.deltaTime 값을 계속 누적하면 특정 시점으로부터
         *       시간이 얼마나 흘렀는지 표현할 수 있다.
         * 
         * Instantiate()
         *     - 게임 도중에 오브젝트를 생성할 때 사용하는 메서드
         *     - 원본 오브젝트를 주면 해당 오브젝트를 복제한 오브젝트를 생성한다.
         *     - 원본으로부터 복제 생성된 오브젝트 == 인스턴스 (인스턴스화)
         *     - 인스턴스화 == 원본에서 복제본을 생성하는 행위
         *     - Instantiate(원본, 위치, 회전);
         *     - 위치 == transform.position, 회전 == transform.rotation
         *     - ==> Instantiate(bulletPrefab, transfrom.postion, transform.rotation);
         */

        // timeAfterSpawn 갱신
        timeAfterSpawn += Time.deltaTime;

        // 최근 생성 시점에서부터 누적된 시간이 생성 주기보다 크거나 같다면
        if (timeAfterSpawn >= spawnRate)
        {
            // 누적된 시간을 리셋
            timeAfterSpawn = 0f;

            // bulletPrefab의 복제본을
            // transform.position 위치와 transform.rotation 회전으로 생성
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            // 생성된 bullet 게임 오브젝트의 정며 방향이 target을 향하도록 회전
            bullet.transform.LookAt(target);
            // 다음번 생성 간격을 spawnRateMin, spawnRateMax 사이에서 랜덤 지정
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}
