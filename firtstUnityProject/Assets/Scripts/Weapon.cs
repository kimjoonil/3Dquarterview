using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range1, Range2 }
    public Type type;
    public int damage;
    public float rate;
    public int maxAmmo;
    public int curAmmo;

    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if ((type == Type.Range2 || type == Type.Range1) && curAmmo > 0)
        {
            curAmmo--;
            StartCoroutine("Shot");
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        // if문으로 Type을 확인해 권총인지 기관단총인지 확인합니다.
        if (type == Type.Range2)
        {
            // for문을 이용해 기관단총을 샷건처럼 탄이퍼져서 6발이 동시에 나가게 했습니다. 
            // 현재는 튜토리얼에만 있는 비활성화 코드
            for (int index = 0; index < 6; index++)
            {
                GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
                //bulletPos.rotation의 y값을 1씩 증가시킴으로서 탄환이 부채꼴 모양으로 발사되게 했습니다.
                bulletPos.Rotate(0, 1, 0, 0);
                Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
                bulletRigid.velocity = bulletPos.forward * 50;
            }
            //for문이 끝나면 틀어진 bulletPos.rotation값을 다시 초기화 시켜줍니다.
            bulletPos.rotation = new Quaternion(0, 0, 0, 0);

            yield return null;

            GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
            Rigidbody caseRigid = intantCase.GetComponent<Rigidbody>();
            Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
            caseRigid.AddForce(caseVec, ForceMode.Impulse);
            caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
        }

        else if (type == Type.Range1)  // 원래있던 권총 샷
        {

            GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = bulletPos.forward * 50;

            yield return null;

            GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
            Rigidbody caseRigid = intantCase.GetComponent<Rigidbody>();
            Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
            caseRigid.AddForce(caseVec, ForceMode.Impulse);
            caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
        }
    }
}
