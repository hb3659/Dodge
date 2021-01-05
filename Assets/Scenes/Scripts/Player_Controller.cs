using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody playerRigidbody;   // 이동에 사용할 리지드바디 컴포넌트
    /*
     *  접근한정자를 private 으로 설정하여 인스펙터 창에서
     *  PlayerController 를 봤을 때 이전의 Player Rigidbody 필드가
     *  더 이상 표시되지 않는다
     *  ====> 드래그 & 드롭으로 리지드바디 컴포넌트를 변수 playerRigidbody 에
     *        할당할 수 없다
     */
    public float speed = 8f;    // 이동 속력
    // Start is called before the first frame update
    void Start()
    {
        /*
         *  Start() 메서드를 이용하여 게임이 시작될 때 변수 playerRigidbody 에
         *  리지드바디 컴포넌트의 참조를 할당하도록 수정
         *  
         *  GetComponent() ==> 원하는 타입의 컴포넌트를 자신의 게임 오브젝트에서
         *                     찾아오는 메서드(<>로 가져올 타입을 받는다)
         *  
         */
        // 게임 오브젝트에서 Rigidbody 컴포넌트를 찾아 playerRigidbody 에 할당
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         *  Update() 메서드는 1초에 수십번씩 실행
         *  따라서 Update() 내부의 다음 코드도 1초에 수십번씩 실행
         *  
         *  Input.GetKey() => 실행될 때 해당 키를 누르고 있으면 true,
         *                    그렇지 않으면 false 를 반환
         *  
         *  Input.GetKeyDown() => 해당 키를 누르는 순간 true,
         *                      그 외에는 false 반환
         *  
         *  Input.GetKeyUP() => 해당 키를 누르다가 손을 떼는 순간 true,
         *                      그 외에는 false 반환
         *                      
         *  ===> Input.GetKeyDown() 과 Input.GetKeyUP() 은
         *       해당 키를 누르는 동안에도 false를 반환
         */
        /*if(Input.GetKey(KeyCode.UpArrow) == true)
        {
            // 위쪽 방향키 입력이 감지된 경우 z 방향 힘 주기
            playerRigidbody.AddForce(0f, 0f, speed);
        }

        if(Input.GetKey(KeyCode.DownArrow) == true)
        {
            // 아래쪽 방향키 입력이 감지된 경우 -z 방향 힘 주기
            playerRigidbody.AddForce(0f, 0f, -speed);
        }

        if(Input.GetKey(KeyCode.RightArrow) == true)
        {
            // 오른쪽 방향키 입력이 감지된 경우 x 방향 힘 주기
            playerRigidbody.AddForce(speed, 0f, 0f);
        }

        if(Input.GetKey(KeyCode.LeftArrow) == true)
        {
            // 왼쪽 방향키 입력이 감지된 경우 -x 방향 힘 주기
            playerRigidbody.AddForce(-speed, 0f, 0f);
        }*/

        // 수평축과 수직축의 입력값을 감지하여 저장
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        // 실제 이동 속도를 입력값과 이동 속력을 사용해 결정
        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        // Vector3 속도를 (xSpeed, 0, zSpeed) 로 생성
        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);
        // 리지드바디의 속도에 newVelocity 할당
        playerRigidbody.velocity = newVelocity;

        /*
         *   Input.GetAxis() => 어떤 축에 대한 입력값을 숫자로 반환하는 메서드
         *      ex) float Input.GetAxis(string axisName); ===> 축(Axis)의 이름을 받는다
         *    - 축의 음의 방향에 대응되는 버튼을 누름 : -1.0
         *    - 아무것도 누르지 않음 : 0
         *    - 축의 양의 방향에 대응되는 버튼을 누름 : +1.0
         *   
         *   기본 설정으로 추가되어 있는 Horizontal 축과 Vertical 축의 대응 입력키와
         *   출력되는 입력 값은...
         *      - Horizontal 축의 경우
         *          - Horizontal(수평) 축에 대응되는 키
         *              - 음의 방향 : 왼쪽 방향키, A 키
         *              - 양의 방향 : 오른쪽 방향키, D 키
         *          - Input.GetAxis("Horizontal") 의 출력값
         *              - 왼쪽 방향키 또는 A 키를 누름 : -1.0
         *              - 아무것도 누르지 않음 : 0
         *              - 오른쪽 방향키 또는 D 키를 누름 : +1.0
         *      
         *      - Vertical 축의 경우
         *          - Vertical (수직) 축에 대응되는 키
         *              - 음의 방향 : 아래쪽 방향키, S 키
         *              - 양의 방향 : 위쪽 방향키, W 키
         *          - Input.GetAxis("Vertical") 의 출력값
         *              - 아래쪽 방향키 또는 S 키를 누름 : -1.0
         *              - 아무것도 누르지 않음 : 0
         *              - 위쪽 방향키 또는 W 키를 누름 : +1.0
         *   
         *   newVelocity => X,Y,Z 방향으로의 속도를 나타내기 위한 변수
         *   
         *   리지드바디의 velocity 변수로 현재 속도를 알 수 있으며,
         *   반대로 해당 변수에 새로운 값을 할당하여 현재 속도를 변경할 수 있다
         *   
         *   Vector3  ==> 원소 x,y,z 를 가지는 타입
         *                위치, 크기, 속도, 방향 등을 나타낼 때 주로 사용
         *   
         *   AddForce() 와 velocity 의 차이
         *      - AddForce() 메서드 => 힘을 누적하고 속력을 점진적으로 증가
         *                             (관성이 생김)
         *      - velocity => 이전 속도를 지우고 새로운 속도를 사용
         *                    (관성을 무시하고 속도가 즉시 변경)
         *                    
         *   GetAxis() 메서드에 입력한 Horizontal 축과 Vertical 축은 무엇이며 왜 사용하는가?
         *   =====> 입력키 커스터마이제이션을 구현하기 위해
         *      - Input.GetAxis("Horizontal") 이 실행될 때의 입력값 감지 과정
         *          - 1. 입력 매니저에서 Horizontal 축을 찾음
         *          - 2. Horizontal 축에 대응되는 버튼(왼쪽 방향키, a, 오른쪽 방향키, d)들로
         *               현재 입력을 검사 -> 감지된 입력값 반환
         *   
         *   GetAxis() 메서드의 출력겂은 어째서 true, false 가 아닌 숫자인가?
         *   =====> 조이스틱 같은 다양한 입력 장치에 대응하기 위해
         */
    }

    public void Die()
    {
        /*
         *  gameObject?
         *   - 컴포넌트 입장에서 자신이 추가된 게임 오브젝트를 가르키는 변수
         *   - GameObject 타입의 변수이며 컴포넌트들의 기반 클래스인 MonoBehaviour 에서 제공
         *   
         *   - 모든 컴포넌트는 gameObject 변수를 이용해 자신이 사용 중인
         *     게임 오브젝트(자신의 게임 오브젝트)에 접근 가능
         *   
         *   - GameObject ==> 타입       gameObject ==> 변수
         *   
         *  setActive()
         *   - 게임 오브젝트 활성화 / 비활성화
         */
        gameObject.SetActive(false);

        // 씬에 존재하는 GameManager 타입의 오브젝트를 찾아서 가져오기
        GameManager gameManager = FindObjectOfType<GameManager>();
        // 가져온 GameManager 오브젝트의 EndGame() 메서드 실행
        gameManager.EndGame();
    }
}
