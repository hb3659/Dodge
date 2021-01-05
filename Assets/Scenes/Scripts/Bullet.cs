using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody bulletRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // 게임 오브젝트에서 Rigidbody 컴포넌트를 찾아 bulletRigidbody 에 할당
        bulletRigidbody = GetComponent<Rigidbody>();
        // 리지드바디의 속도 = 앞쪽 방향 * 리동 속력
        bulletRigidbody.velocity = transform.forward * speed;
        // 탄알은 앞쪽으로 초당 speed 만큼 이동
        /* transform.forward 는 현재 게임 오브젝트의 앞쪽 방향(Z축 방향)을 나타내는
         * Vector3 타입 변수이다
         * 
         * 트랜스폼 컴포넌트는 게임 오브젝트의 위치, 크기, 회전을 담당하는 컴포넌트이다.
         * 따라서 게임 오브젝트가 하나씩 가지고 있도록 강제되어 있으며 가장 자주 사용되는
         * 컴포넌트이다.(트랜스폼 컴포넌트가 없으면 3D 공간에서 위치를 가질 수 없기 때문)
         * 
         * 유니티의 C# 스크립트들은 자신의 게임 오브젝트의 트랜스폼 컴포넌트를 코드 상에서 
         * transform 변수로 즉시 접근할 수 있도록 구성되었다. 따라서 트랜스폼 컴포넌트는
         * GetComponent<Transform>() 등을 사용하여 직접 찾아오는 과정을 거칠 필요가 없다.
         * 
         * transform == 변수 / Transform == 타입
         */

        Destroy(gameObject, 3f);
        /*
         * Destroy() == 입력한 오브젝트를 파괴
         * gameObject == 자신의 게임 오브젝트를 가리키는 변수
         * 
         * 충돌 이벤트 메서드
         *      - 게임 오브젝트는 자신이 충돌한 사실을 알 수 없다.
         *      - 대신 충돌했음을 알려주는 메시지가 A와 B에 보내진다.
         *      - 충돌 메시지를 통해 게임 오브젝트와 해당 게임 오브젝트에 
         *        추가된 컴포넌트들은 충돌 사실을 알게 되고 충돌에 대응하는 메서드를 실행
         *      - 충돌의 종류에 따라 OnTriggerEnter / OnCollisionEnter 메시지를 받는다.
         *      
         * OnCollision 계열 : 일반 충돌
         *      - 일반적인 콜라이더를 가진 두 게임 오브젝트가 충돌할 때 자동으로 실행
         *      - 충돌한 두 콜라이더는 서로 통괴하지 않고 밀어낸다.
         *      
         *      - OnCollisionEnter(Collision collision) : 충돌한 순간
         *      - OnCollisionStay(Collision collision) : 충돌하는 동안
         *      - OnCollisionExit(Collision collision) : 충돌했다가 분리되는 순간
         * 
         * OnTrigger 계열 : 트리거 충돌
         *      - 충돌한 두 게임 오브젝트의 콜라이더 중 최소 하나가 트리거 콜라이더라면 자동으로 실행
         *      - 이 경우 두 게임 오브젝트가 충돌했을 때 서로 그래도 통과
         *      
         *      - OnTriggerEnter(Collider other) : 충돌한 순간
         *      - OnTriggerStay(Collider other) : 충돌하는 순간
         *      - OnTriggerExit(Collider other) : 충돌했다가 분리되는 순간
         */
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // 충돌한 상대방 게임 오브젝트가 Player 태그를 가진 경우
        if(other.tag == "Player")
        {
            // 상대방 게임 오브젝트에서 PlayerController 컴포넌트 가져오기
            Player_Controller playerController = other.GetComponent<Player_Controller>();

            // 상대방으로부터 PlayerController 컴포넌트를 가져오는데 성공했다면
            if(playerController != null)
            {
                playerController.Die();
            }
        }
    }
}
