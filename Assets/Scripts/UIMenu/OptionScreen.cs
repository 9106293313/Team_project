using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
public class OptionScreen : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog, controlReverseTog , chargeControlReverseTog;

    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;

    public TMP_Text resolutionLabel;

    public AudioMixer theMixer;

    public TMP_Text masterLabel, musicLabel, sfxLabel;
    public Slider masterSlider, musicSlider, sfxSlider;

    public GameObject OptionMenuAnimation;
    public AudioSource ScrollSound;
    public AudioSource ScrollSound2;

    IEnumerator PlayOptionMenuAnimation()
    {
        ScrollSound.Play();
        OptionMenuAnimation.SetActive(true);
        OptionMenuAnimation.GetComponent<Animator>().SetTrigger("PlayAm");
        yield return new WaitForSeconds(1f);
        OptionMenuAnimation.SetActive(false);
    }
    IEnumerator PlayOptionMenuAnimation2()
    {
        ScrollSound2.Play();
        OptionMenuAnimation.SetActive(true);
        OptionMenuAnimation.GetComponent<Animator>().SetTrigger("PlayAm2");

        float endPauseTime = Time.realtimeSinceStartup + 0.5f;
        yield return new WaitWhile(() => Time.realtimeSinceStartup < endPauseTime); //自訂義的waitTime，不被timescale影響

        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(PlayOptionMenuAnimation());
    }
    public void DisableMenu()
    {
        StartCoroutine(PlayOptionMenuAnimation2());
    }
    void Start()
    {
        

        fullscreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        bool foundRes = false;
        for(int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedResolution = i;
                UpdateResLabel();
            }
        }

        /*if(foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedResolution = resolutions.Count - 1;

            UpdateResLabel();
        }*/

        float vol = 0f;
        theMixer.GetFloat("MasterVol", out vol);
        masterSlider.value = vol;
        theMixer.GetFloat("MusicVol", out vol);
        musicSlider.value = vol;
        theMixer.GetFloat("SFXVol", out vol);
        sfxSlider.value = vol;

        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
    }

    void Update()
    {
        if(GameObject.FindWithTag("Player"))
        {
            if(controlReverseTog.isOn==true)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().ReverseControlNum = -1;
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().ReverseControlNum = 1;
            }
        }

        if (GameObject.FindWithTag("Player"))
        {
            if (chargeControlReverseTog.isOn == true)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().chargeReverseControlNum = -1;
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().chargeReverseControlNum = 1;
            }
        }
    }

    public void ResLeft()
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }
        UpdateResLabel();
    }
    public void ResRight()
    {
        selectedResolution++;
        if(selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }
    public void ApplyGraphic()
    {
        //Screen.fullScreen = fullscreenTog.isOn;
        if(vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenTog.isOn);
    }

    public void SetMasterVol()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }
    public void SetMusicVol()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }
    public void SetSFXVol()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SFXVol", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }
}



[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}


