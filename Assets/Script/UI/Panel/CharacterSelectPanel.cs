using Knight.Tools;
using Knight.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPanel : UIPanel
{
    Transform content;
    Toggle[] toggles;
    string selectedChar;
    Button btn_start;
    Button btn_back;
    CharacterInfo charactersInfo;
    RenderTexture texture;
    Camera _camera;
    private void Start()
    {
        content = transform.FindChildByName("Content");
        btn_start = transform.FindChildByName("Start").GetComponent<Button>();
        btn_back = transform.FindChildByName("Back").GetComponent<Button>();
        btn_start.onClick.AddListener(ClickStart);
        btn_back.onClick.AddListener(ClickBack);
        _camera = new GameObject().AddComponent <Camera>();
        _camera.orthographic = true;
        _camera.orthographicSize = 10;
        _camera.cullingMask = LayerMask.GetMask("CharacterSelect");
        texture = Resources.Load<RenderTexture>("Texture/CharacterSelect");
        //texture = _camera.targetTexture;
        _camera.targetTexture = texture;
        GetCharacterInfo();
        InitToggles();
        InitCharacterCard();
        InitCharacterPrefab();
    }
    private void GetCharacterInfo()
    {
        charactersInfo = Resources.Load<CharacterInfo>("Data/CharacterInfo");
    }
    private void ClickBack()
    {
        UIManager.Instance.DisplayPanel("StartMenu");
    }

    private void ClickStart()
    {
        if (selectedChar == null) return;
        Debug.Log(selectedChar);
        SceneLoader.Instance.LoadScene("GamePlay");
        UIManager.Instance.HideAllPanel();
    }

    private void InitToggles()
    {
        toggles = new Toggle[charactersInfo.characters.Count];
        Transform toggle = content.GetChild(0);
        for (int i = 0; i < toggles.Length; i++)
        {
            GameObject _obj = Instantiate(toggle.gameObject);
            _obj.name = charactersInfo.characters[i].name;
            Toggle _toggle = _obj.GetComponent<Toggle>();
            toggles[i] = _toggle;
            _toggle.onValueChanged.AddListener(OnToggle);
            _obj.transform.SetParent(content);
            _obj.SetActive(true);
            Debug.Log(toggles[i].name);
        }
    }
    private void OnToggle(bool arg0)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn) selectedChar = toggles[i].name;
        }
    }
    private void InitCharacterCard()
    {
        int j;
        for (int i = 0; i < charactersInfo.characters.Count; i++)
        {
            int x = i % 5;
            int y = i / 5;
            RawImage raw = toggles[i].GetComponentInChildren<RawImage>();
            raw.texture = this.texture;
            raw.uvRect = new Rect() { y = x / 5f, x = y / 5f, width = 0.2f, height = 0.2f };
            SetCharacterText(toggles[i].transform, charactersInfo.characters[i]);
        }
    }
    private void SetCharacterText(Transform _tf, CharacterInfo.info info)
    {
        for (int i = 0; i < _tf.childCount; i++)
        {
            Text child = _tf.GetChild(i).GetComponent<Text>();
            if (child == null) continue;
            if (child.name == "HP") child.text = child.name + " " + info.character.hp;
            if (child.name == "Armor") child.text = child.name + " " + info.character.armor;
            if (child.name == "Sheild") child.text = child.name + " " + info.character.sheild;
            if (child.name == "MoveSpeed") child.text = child.name + " " + info.character.moveSpeed;
            if (child.name == "RotatoSpeed") child.text = child.name + " " + info.character.rotatoSpeed;
            if (child.name == "Name") child.text = info.name;
        }

    }
    private void InitCharacterPrefab()
    {
        for (int i = 0; i < charactersInfo.characters.Count; i++)
        {
            int x = i % 5;
            int y = i / 5;
            GameObject _obj = Instantiate(charactersInfo.characters[i].prefab);
            SetObjectLayer(_obj, 10);
            _obj.transform.parent = _camera.transform;
            _obj.transform.position = _camera.ViewportToWorldPoint(Vector3.up * (x / 5f + 0.1f) + Vector3.right * (y / 5f + 0.1f)) + Vector3.forward * 5;

        }
    }
    private void SetObjectLayer(GameObject obj, int layer)
    {
        if (obj == null) return;
        obj.layer = layer;
        int count = obj.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            var child = obj.transform.GetChild(i);
            SetObjectLayer(child.gameObject, layer);
        }
    }
}
