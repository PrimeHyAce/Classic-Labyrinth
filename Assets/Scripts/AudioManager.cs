using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Toggle bgmToggle;
    [SerializeField] Toggle sfxToggle;

    // Start is called before the first frame update
    void Start()
    {
        var dbBgm = PlayerPrefs.GetFloat("BGM_ATTENUATION",0);
        var volBgm = DbToVol(dbBgm);
        mixer.SetFloat("BGM_ATTENUATION",dbBgm);
        bgmSlider.SetValueWithoutNotify(volBgm);
        // check if bgm is muted
        if(dbBgm == -80)
        {
            bgmToggle.SetIsOnWithoutNotify(true);
        }

        var dbSfx = PlayerPrefs.GetFloat("SFX_ATTENUATION",0);
        var volSfx = DbToVol(dbSfx);
        mixer.SetFloat("SFX_ATTENUATION",dbSfx);
        sfxSlider.SetValueWithoutNotify(volSfx);  
        // check if sfx is muted
        if(dbSfx == -80)
        {
            sfxToggle.SetIsOnWithoutNotify(true);
        }     
    }

    private void OnEnable() {
        bgmSlider.onValueChanged.AddListener(SetBgmVol);
        sfxSlider.onValueChanged.AddListener(SetSfxVol);
        bgmToggle.onValueChanged.AddListener(SetBgmMute);
        sfxToggle.onValueChanged.AddListener(SetSfxMute);
    }

    private void OnDisable() {
        bgmSlider.onValueChanged.RemoveListener(SetBgmVol);
        sfxSlider.onValueChanged.RemoveListener(SetSfxVol);
        bgmToggle.onValueChanged.RemoveListener(SetBgmMute);
        sfxToggle.onValueChanged.RemoveListener(SetSfxMute);
        PlayerPrefs.Save();
    }

    private void SetBgmVol(float vol)
    {
        var db = VolToDB(vol);
        mixer.SetFloat("BGM_ATTENUATION",db);
        PlayerPrefs.SetFloat("BGM_ATTENUATION",db);
    }

    private void SetSfxVol(float vol)
    {
        var db = VolToDB(vol);
        mixer.SetFloat("SFX_ATTENUATION",db);
        PlayerPrefs.SetFloat("SFX_ATTENUATION",db);
    }

    public void SetBgmMute(bool isMute)
    {
        if(isMute)
        {
            mixer.SetFloat("BGM_ATTENUATION",-80);
            bgmSlider.SetValueWithoutNotify(0);
        }
        else
        {
            var db = PlayerPrefs.GetFloat("BGM_ATTENUATION",0);
            var vol = DbToVol(db);
            mixer.SetFloat("BGM_ATTENUATION",db);
            bgmSlider.SetValueWithoutNotify(vol);
        }
    }

    public void SetSfxMute(bool isMute)
    {
        if(isMute)
        {
            mixer.SetFloat("SFX_ATTENUATION",-80);
            sfxSlider.SetValueWithoutNotify(0);
        }
        else
        {
            var db = PlayerPrefs.GetFloat("SFX_ATTENUATION",0);
            var vol = DbToVol(db);
            mixer.SetFloat("SFX_ATTENUATION",db);
            sfxSlider.SetValueWithoutNotify(vol);
        }
    }

    private float DbToVol(float db)
    {
        return Mathf.Pow(10, db/20);
    }

    private float VolToDB(float vol)
    {
        if(vol==0)
            return -80;

        return 20 * Mathf.Log10(vol);
    }
}