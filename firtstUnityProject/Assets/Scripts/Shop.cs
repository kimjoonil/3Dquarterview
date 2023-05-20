using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 기존스크립트가 달라진점은 밑에달라진 부분마다 적어놨으나 매니저 스크립트가 달라진점은 이곳에 적음
일단 각 버튼에 재고 숫자를 적어줬으므로 이텍스트들을 매니저에 퍼블릭으로 명시해 일일이 인스펙터창에서
넣어줘야함 하이라키창에서 텍스트또한 만들어줘야 하는데  이부분은 그냥내가 파일을 통째로 넘기겠음
또한 재고관리를 위해서 마찬가지로 매니저스크립트에 int로 6개의재고를 10개 1개씩넣어줘야함 그리고 
shop 스크립트를 인스펙터창에서 들어가 talk data를 3개로 늘리고 재고가 없을시의 텍스트를넣어줘야함
매니저스크립트에 endstage란 숫자를추가해 스테이지종료를 인식해야해서 매니저에추가해줘야함
 */
public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    public Animator anim;

    // 스테이지숫자가 바뀌는 것과같은 작동원리를 위해 상점의 텍스트를가져옴
    public Text heartText;
    public Text ammoText;
    public Text grenadeText;
    public Text hammerText;
    public Text handText;
    public Text subText;
    public Text talkText;

    /* 재고관리를 위해서 itemobj를6개로늘리고 매니저스크립트와 게임판넬을 가져옴
     * shop 스크립트내에서 구입시 각 아이템을 구별해내기 위해 6개로늘린 itemobj의 배열의 숫자로 아이템을 판별해냄*/
    Player enterPlayer;
    public GameManager manager;
    public GameObject[] itemObj;
    public GameObject gamePanel;

    public int[] itemPrice;
    public Transform[] itemPos;
    public string[] talkData;

    // 재고가 0이됬음을 인지하기위한 숫자 0이되면 재고가 0 근데 각 물건의 재고를 따로따로 판별을 못해서 오류가능성 있음  
    int Inventorymanager = 1;


    public void Enter(Player player)
    {
        endStage();
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    // Update is called once per frame
    public void Exit()
    {
        anim.SetTrigger("doHello");
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }


    // 각스테이지가 끝날때마다 재고와 텍스트를초기화해줌 enter함수에 함수가 들어가있음
    public void endStage()
    {
        if(manager.endStage == 1)
        {
            manager.heartNumber = 10;
            manager.ammoNumber = 10;
            manager.grenadeNumber = 10;
            manager.hammerNumber = 1;
            manager.handNumber = 1;
            manager.subNumber = 1;
            heartText.text = manager.heartNumber + " / 10";
            ammoText.text = manager.ammoNumber + " / 10";
            grenadeText.text = manager.grenadeNumber + " / 10";
            hammerText.text = manager.hammerNumber + " / 1";
            handText.text = manager.handNumber + " / 1";
            subText.text = manager.subNumber + " / 1";
        }
    }

    public void Buy(int index)
    {
        int price = itemPrice[index];
        
        

        if (price > enterPlayer.coin)
        {
            StopCoroutine(Talk());
            StartCoroutine(Talk());
            return;
        }

        // 재고관리 함수
        Inventory(index);

        // 만약 재고가 0임을 인지하면 buy자체를 탈출하기위한 return용 함수 이게없으면 재고가없어도 클릭시 돈 삭제됨
        if (Inventorymanager == 0)
        {
            Inventorymanager = 1;
            return;
        }

        enterPlayer.coin -= price;
        Vector3 ranVec = Vector3.right * Random.Range(-3, 3) + Vector3.forward * Random.Range(-3, 3);
        Instantiate(itemObj[index], itemPos[2].position + ranVec, itemPos[2].rotation);
    }


    // shop 스크립트내에서 구입시 각 아이템을 구별해내기 위해 6개로늘린 itemobj의 배열의 숫자로 아이템을 판별해냄
    //case로 각 아이템의 인덱스를 받아 구별하고 클릭마다 텍스트를 변경해줌 
    // 또한 매니저 스크립트에 각 물건의 재고가 저장되어있는데 그걸 하나씩 깎아줌
    public void Inventory(int index)
    {
        switch (index)
        {
            
            case 0:
                if (manager.heartNumber == 0)
                {
                    StopCoroutine(Talk2());
                    StartCoroutine(Talk2());
                    Inventorymanager = 0;
                    return;
                }
                manager.heartNumber--;
                heartText.text = manager.heartNumber + " / 10";
                break;
            case 1:
                if (manager.ammoNumber == 0)
                {
                    StopCoroutine(Talk2());
                    StartCoroutine(Talk2());
                    Inventorymanager = 0;
                    return;
                }
                manager.ammoNumber--;
                ammoText.text = manager.ammoNumber + " / 10";
                break;
            case 2:
                if (manager.grenadeNumber == 0)
                {
                    StopCoroutine(Talk2());
                    StartCoroutine(Talk2());
                    Inventorymanager = 0;
                    return;
                }
                manager.grenadeNumber--;
                grenadeText.text = manager.grenadeNumber + " / 10";
                break;
            case 3:
                if (manager.hammerNumber == 0)
                {
                    StopCoroutine(Talk2());
                    StartCoroutine(Talk2());
                    Inventorymanager = 0;
                    return;
                }
                manager.hammerNumber--;
                hammerText.text = manager.hammerNumber + " / 1";
                break;
            case 4:
                if (manager.handNumber == 0)
                {
                    StopCoroutine(Talk2());
                    StartCoroutine(Talk2());
                    Inventorymanager = 0;
                    return;
                }
                manager.handNumber--;
                handText.text = manager.handNumber + " / 1";
                break;
            case 5:
                if (manager.subNumber == 0)
                {
                    StopCoroutine(Talk2());
                    StartCoroutine(Talk2());
                    Inventorymanager = 0;
                    return;
                }
                manager.subNumber--;
                subText.text = manager.subNumber + " / 1";
                break;
        }
     }

    IEnumerator Talk()
    {
        talkText.text = talkData[1];
        yield return new WaitForSeconds(2f);
        talkText.text = talkData[0];
    }

    IEnumerator Talk2()
    {
            talkText.text = talkData[2];
            yield return new WaitForSeconds(2f);
            talkText.text = talkData[0];
    }

    }
