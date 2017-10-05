using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RaycastShoot : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = .012f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public float recoil;
    public float gunHeatCoef = 5;
    public float gunHeatThreshold = 15;
    public Transform gunEnd;
    public Text heatText;
    public Image progressBar;

    private Camera fpsCam;
    private float nextFire;
    private float nextAudioPlay;
    private RaycastHit hit;
    private ParticleSystem traceParticle;
    private Vector3 rayOrigin;
    private Vector3 direction;
    private AudioSource audioSourceShot;
    private GameObject muzzleFlash;
    private float gunHeat;
    private bool isOverHeat;


    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();

        audioSourceShot = GetComponent<AudioSource>();
        muzzleFlash = GameObject.FindWithTag("MuzzleFlash");
        muzzleFlash.SetActive(false);
        gunHeat = 0;
        isOverHeat = false;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire && !isOverHeat)
        {
            traceParticle = ObjectPooler.current.GetPooledGameObject().GetComponent<ParticleSystem>();
            if (traceParticle == null) return;
            gunHeat++;
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect(muzzleFlash, fireRate * 0.9f));
            audioSourceShot.Play();
            traceParticle.transform.position = gunEnd.transform.position;
            traceParticle.transform.rotation = Quaternion.LookRotation(fpsCam.transform.forward * weaponRange);
            traceParticle.gameObject.SetActive(true);
//            traceParticle.Stop();
//            traceParticle.Play();
            traceParticle.Emit(1);
        }
        else
        {
            CoolDown();
        }

        if (gunHeat >= gunHeatThreshold)
        {
            isOverHeat = true;
        }
        else if (gunHeat <= 0)
        {
            isOverHeat = false;
        }

        

//        Debug.Log(progressBar.rectTransform.sizeDelta);
        progressBar.rectTransform.sizeDelta = new Vector2(
            Mathf.Lerp(progressBar.rectTransform.sizeDelta.x, gunHeat / gunHeatThreshold * 100, 0.2f),
            20.0f
        );
        heatText.text = "Heat: " + gunHeat + ", delta: " + Time.deltaTime;
    }

    private void CoolDown()
    {
        gunHeat = Mathf.Max(0, Mathf.MoveTowards(gunHeat, 0, Time.deltaTime * gunHeatCoef));
    }

    private IEnumerator ShotEffect(GameObject muzzleFlashGameObject, float duration)
    {
        muzzleFlashGameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        muzzleFlash.SetActive(false);
    }
}

//    private float shotDuration;

//    private Vector3 RecoilDirection(Vector3 initial)
//    {
//        return new Vector3(
//            Recoil(initial.x),
//            Recoil(initial.y),
//            Recoil(initial.z)
//        );
//    }

//    private float Recoil(float initial)
//    {
//        return initial * (1 + Random.Range(0.0f, recoil));
//    }
//}

//    private Vector3 bulletDestination;
//            StartCoroutine(ShotParticleWait(shotDuration));
/*rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f));
direction = RecoilDirection(fpsCam.transform.forward);*/

//        shotDuration = 1 / traceParticle.emission.rateOverTime.constant;
//        traceParticle.Stop();
/*if (Physics.Raycast(rayOrigin, direction, out hit, weaponRange))
{
//                bulletDestination = hit.point - gunEnd.position;
    ShootableBox health = hit.collider.GetComponent<ShootableBox>();
    if (health != null)
    {
        health.Damage(gunDamage);
    }
//            if (!traceParticle.isPlaying)
//            {
//                traceParticle.Play();
//            }
//            if (!audioSourceShot.isPlaying)
//            {
//                audioSourceShot.Stop();

//        traceParticle = GetComponentInChildren<ParticleSystem>();
    if (hit.rigidbody != null)
    {
//                    hit.rigidbody.AddForce(-hit.normal * hitForce);
    }
}*/
//            else
//            {
//                bulletDestination = direction * weaponRange;
//            }
//            traceParticle.randomSeed = (uint)Random.Range(0, int.MaxValue);
//            traceParticle.enableEmission = true;
//            Random.InitState((int) Random.value);
//            traceParticle.randomSeed = (uint) Random.value;
//            ParticleSystem.EmitParams paramss = new ParticleSystem.EmitParams();
//            paramss.randomSeed  = (uint)Random.Range(0, int.MaxValue);


//        if (Input.GetButtonUp("Fire1"))
//        {
//            traceParticle.enableEmission = false;
//        }


//    private IEnumerator ShotHitResult(float duration)
//    {
//        yield return new WaitForSeconds(duration);
//    }