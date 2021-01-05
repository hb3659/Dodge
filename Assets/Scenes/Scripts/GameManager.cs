using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // UI 관련 라이브러리
// UI 관련 컴포넌트를 변수로 선언하여 사용 가능
using UnityEngine.SceneManagement;  // 씬 관리 관련 라이브러리
// 게임 도중 씬을 재시작하는 기능 작성 가능

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;     // 게임오버 시 활성화할 텍스트 게임 오브젝트
    // Gameover  Text를 할당
    public Text timeText;               // 생존 시간을 표시할 텍스트 컴포넌트
    // Time Text 게임 오브젝트의 Text 컴포넌트를 할당
    public Text recordText;             // 최고 기록을 표시할 텍스트 컴포넌트
    // Record Text 게임 오브젝트의 Text 컴포넌트를 할당
    
    /*
     *  timeText, recordText == 텍스트 컴포넌트(Text 타입)
     *  gameoverText == 게임 오브젝트(GameObject 타입)
     *      - 텍스트 컴포넌트가 표시하는 텍스트 내용을 변경하고 싶으면
     *        텍스트 컴포넌트를 Text 타입의 변수에 할당, 그리고 Text 타입에
     *        내장된 Text 필드에 접근하여 수정한다.
     *        (Text 타입의 text 필드는 인스펙터 창에서 편집 가능한
     *        텍스트 컴포넌트의 Text 필드)
     *        =======> Time Text 게임 오브젝트와 Record Text 게임 오브젝트의
     *                 텍스트 컴포넌트사 표시하는 내용을 실시간으로 변경하기 위해
     *                 이들에 대한 변수를 Text 타입으로 선언했다.
     *      - 반대로 게임오브 텍스트를 표시하는 Gameover Text 게임 오브젝트는
     *        표시할 텍스트 내용이 변경되지 않는다. Gameover Text 게임 오브젝트는
     *        내용을 변경하지 않고 게임 오브젝트를 비활성화하거나 활성화하는 방식으로만
     *        사용할 것이므로 GameObject 타입으로 선언했다.
     */

    private float surviveTime;  // 생존 시간
    private bool isGameover;    // 게임오버 상태
    // Start is called before the first frame update
    void Start()
    {
        surviveTime = 0;
        isGameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 게임오버가 아닌 동안
        if (!isGameover)
        {
            // 생존 시간 갱신
            surviveTime += Time.deltaTime;
            // 갱신한 생존 시간을 timeText 텍스트 컴포넌트를 이용해 표시
            timeText.text = "Time : " + (int)surviveTime;
        }
        else
        {
            // 게임오버 상태에서 R 키를 누른 경우
            if (Input.GetKeyDown(KeyCode.R))
            {
                // sampleScene 씬을 로드
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    // 현재 게임을 게임오버 상태로 변경하는 메서드
    public void EndGame()
    {
        // 현재 상태를 게임오버 상태로 전환
        isGameover = true;
        // 게임오버 텍스트 게임 오브젝트를 활성화
        gameoverText.SetActive(true);

        // BestTime 키로 저장된 이전까지의 최고 기록 가져오기
        float bestTime = PlayerPrefs.GetFloat("BestTime");

        // 이전가지의 최고 기록보다 현재 생존 기간이 더 크다면
        if(surviveTime > bestTime)
        {
            // 최고 기록 값을 현재 생존 시간 값으로 변경
            bestTime = surviveTime;
            // 변경된 최고 기록을 BestTime 키로 저장
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        // 최고 기록을 recordText 텍스트 컴포넌트를 이용해 표시
        recordText.text = "Best Time: " + (int)bestTime;
    }
}
