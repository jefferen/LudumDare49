using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public CameraShake CameraShake;
    public TMPro.TextMeshPro hpText;
    public TMPro.TextMeshProUGUI YouDied;
    public int MaxHealth, Health;
    public GameObject DmgPopUp;
    public AudioClip audioClip, death;
    SoundManager soundManager;
    public Color Dmg, health;
    bool IsAlive;

    void Start()
    {
        IsAlive = true;
        YouDied.text = "";
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
        Health = MaxHealth;
        UpdateUI();
        InvokeRepeating("ConstantLavaDmg", 2, 2);
    }

    void ConstantLavaDmg()
    {
        --Health;
        PopUp(1);
        UpdateUI();
    }

    public void GainHealth(int hp)
    {
        Health += hp;
        PopUp(hp, false);
        UpdateUI();
    }

    public void DoDamage(int hp)
    {
        StartCoroutine(CameraShake.Tremble(0.15f, 0.1f));
        Health -= hp;
        soundManager.PlayClip(audioClip);
        PopUp(hp);
        UpdateUI();
    }

    void PopUp(int hp, bool dmg = true)
    {
        GameObject g = Instantiate(DmgPopUp, transform.position, Quaternion.identity);
        TMPro.TextMeshPro text = g.GetComponentInChildren<TMPro.TextMeshPro>();
        text.text = hp.ToString();
        if(!dmg) text.color = health;
        else text.color = Dmg;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("WillDoDmg"))
        {
            other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            if(other.gameObject.GetComponent<DealDmg>() != null)
            {
                int dmg = other.gameObject.GetComponent<DealDmg>().DmgToDeal;
                DoDamage(dmg);
            }
        }
    }

    void UpdateUI()
    {
        if (Health <= 0 && IsAlive) Death();
        hpText.text = Health.ToString();
    }


    public void Death()
    {
        IsAlive = false;
        soundManager.PlayClip(death);
        YouDied.text = "You died";
        gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("HelpCam").SetActive(false);
        Invoke("RestartLvl", 3);
    }

    void RestartLvl()
    {
        GameManager.RestartLvl();
    }
}
