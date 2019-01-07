using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShoot : MonoBehaviour {

    public GameObject bullet;

    public GameObject Bullet_Pos;

    [SerializeField]
    private float fireRate = 0.2f;
    private float cooldown = 0f;

    [SerializeField]
    private float grass = 0f;
    [SerializeField]
    private float milk = 1.0f;

    private IEnumerator m_CurrentCoroutine = null;

    void Start()
    {
        SimplePool.Preload(bullet, 30);
    }

	void Update ()
    {
        if (!Flags.instance.canFire)
            return;

        if (cooldown > 0f)  
            cooldown -= Time.deltaTime;

        if (milk < 1.0f)
        {
            if (m_CurrentCoroutine == null)
            {
                if (grass >= (1 - milk) && grass > 0f)
                {
                    m_CurrentCoroutine = AddMilkSlowly(1 - milk);
                    grass -= (1 - milk);
                    StartCoroutine(m_CurrentCoroutine);
                }
                else if (grass > 0f)
                {
                    m_CurrentCoroutine = AddMilkSlowly(grass);
                    grass = 0f;
                    StartCoroutine(m_CurrentCoroutine);
                }
            }
        }

        if (CrossPlatformInputManager.GetButton("Fire1") && cooldown <= 0f && milk > 0f)
        {
            SimplePool.Spawn(bullet, Bullet_Pos.transform.position,
                Quaternion.Euler(Camera.main.transform.eulerAngles.x - 12.906f,
                Bullet_Pos.transform.rotation.eulerAngles.y, 0));
            AudioManager.instance.PlaySound2D("tir");
            cooldown = fireRate;
            milk -= 0.05f;
            milk = Mathf.Clamp(milk, 0f, 1f);
        }
    }

    IEnumerator AddMilkSlowly(float addMilk)
    {
        float duration = .2f;
        float smoothness = 0.01f;
        float progress = 0f;
        float currentMilk = milk;
        float endMilk = currentMilk + addMilk;

        if (endMilk > 1f)
            endMilk = 1f;

        while (progress < 1.05)
        {
            milk = Mathf.Lerp(milk, endMilk, progress);
            progress += smoothness / duration;
            if (milk < currentMilk)
                endMilk = currentMilk - milk;
            yield return new WaitForSeconds(smoothness);
        }
        milk = Mathf.Clamp(milk, 0f, 1f);
        m_CurrentCoroutine = null;
        yield return null;
    }

    IEnumerator AddGrassSlowly(float addGrass)
    {
        float duration = .2f;
        float smoothness = 0.01f;
        float progress = 0f;
        float currentGrass = grass;
        float endGrass = currentGrass + addGrass;

        if (endGrass > 1f)
            endGrass = 1f;

        while (progress < 1.05)
        {
            grass = Mathf.Lerp(grass, endGrass, progress);
            progress += smoothness / duration;
            if (grass < currentGrass)
                endGrass = currentGrass - grass;
            yield return new WaitForSeconds(smoothness);
        }
        grass = Mathf.Clamp(grass, 0f, 1f);
        m_CurrentCoroutine = null;
        yield return null;
    }

    public float GetMilk()
    {
        return milk;
    }

    public IEnumerator AddMilk(float value)
    {
        while (m_CurrentCoroutine != null)
            yield return null;
        m_CurrentCoroutine = AddMilkSlowly(value);
        StartCoroutine(m_CurrentCoroutine);
        yield return null;
    }

    public float GetGrass()
    {
        return grass;
    }

    public IEnumerator AddGrass(float value)
    {
        while (m_CurrentCoroutine != null)
            yield return null;
        m_CurrentCoroutine = AddGrassSlowly(value);
        StartCoroutine(m_CurrentCoroutine);
        yield return null;
    }
}
