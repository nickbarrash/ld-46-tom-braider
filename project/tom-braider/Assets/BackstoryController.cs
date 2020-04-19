using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackstoryController : MonoBehaviour {
    public List<GameObject> StoryText;

    public float AnimationStartDelay;
    public float TextAppearDuration;
    public float AppearedPauseDuration;
    public float TextFadeDuration;
    public float FadedPauseDuration;

    private float AnimationStartTime = 0f;
    private float TextAppearTime = 0f;
    private float AppearedPauseTime = 0f;
    private float TextFadeTime = 0f;
    private float FadePauseTime = 0f;

    private int AnimatingIndex;

    private UIManager manager;

    // Start is called before the first frame update
    void Start() {
        ResetAnimation();
        manager = FindObjectOfType<UIManager>();
    }

    private float FadeInT() {
        return Mathf.InverseLerp(TextAppearDuration, 0, TextAppearTime);
    }

    private float FadeOutT() {
        return Mathf.InverseLerp(0, TextFadeDuration, TextFadeTime);
    }

    private void SetTextAlpha(int index, float alpha) {
        StoryText[index].GetComponent<TMP_Text>().alpha = alpha;
    }

    // Update is called once per frame
    void Update() {
        Animate();
    }

    public void Animate() {
        if (AnimatingIndex < StoryText.Count) {
            if (AnimationStartTime > 0f) {
                AnimationStartTime -= Time.deltaTime;
                if (AnimationStartTime <= 0f) {
                    TextAppearTime = TextAppearDuration - AnimationStartTime;
                    AnimationStartTime = 0f;
                }
            } else if (TextAppearTime > 0f) {
                TextAppearTime -= Time.deltaTime;
                SetTextAlpha(AnimatingIndex, FadeInT());
                if (TextAppearTime <= 0f) {
                    AppearedPauseTime = AppearedPauseDuration - TextAppearTime;
                    TextAppearTime = 0f;
                }
            } else if (AppearedPauseTime > 0f) {
                AppearedPauseTime -= Time.deltaTime;
                if (AppearedPauseTime <= 0f) {
                    TextFadeTime = TextFadeDuration - AppearedPauseTime;
                    AppearedPauseTime = 0f;
                }
            } else if (TextFadeTime > 0f) {
                TextFadeTime -= Time.deltaTime;
                SetTextAlpha(AnimatingIndex, FadeOutT());
                if (TextFadeTime <= 0f) {
                    FadePauseTime = FadedPauseDuration - TextFadeTime;
                    TextFadeTime = 0f;
                }
            } else if (FadePauseTime > 0f) {
                FadePauseTime -= Time.deltaTime;
                if (FadePauseTime <= 0f) {
                    TextAppearTime = TextAppearDuration - FadePauseTime;
                    FadePauseTime = 0f;
                    AnimatingIndex++;
                }
            }
        }

        if (AnimatingIndex >= StoryText.Count) {
            PlayGame();
        }
    }

    public void ResetAnimation() {
        for (int i = 0; i < StoryText.Count; i++) {
            SetTextAlpha(i, 0f);
        }
    }

    public void StartAnimation() {
        Debug.Log("here");
        AnimatingIndex = 0;
        if (AnimationStartDelay == 0) {
            TextAppearTime = TextAppearDuration;
        } else {
            AnimationStartTime = AnimationStartDelay;
        }
    }

    public void PlayGame() {
        manager.SwitchToPanel(2);
    }
}
