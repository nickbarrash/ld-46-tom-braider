using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTile : MonoBehaviour
{
    public List<EffectOnDepress> DepressEffects;
    public List<EffectOnStayDepress> StayDepressEffects;

    private int PlayersOnTile = 0;

    private GameObject Tile;
    private SpriteRenderer TileColor;

    private float EXACT_ANIMATION_OFFSET = 1f;
    private float ANIMATION_DURATION = 0.06f;
    private float ReleaseAnimateTime = 0f;
    private float DepressAnimateTime = 0f;

    private float DEPRESS_HEIGHT = -0.1f;
    private float NORMAL_HEIGHT = 0f;

    public bool StatefulColor = false;
    public Color UntouchedColor = Color.white;
    public Color TriggeredColor = Color.white;

    public Color DepressedColor = Color.white;
    public Color UnpressedColor = Color.white;

    public enum COLOR_STATE {
        // 1 time use settings = priority
        UNTOUCHED,
        TRIGGERED,

        // State based on if trigger is depressed
        DEPRESSED,
        UNPRESSED,
    }


    // Start is called before the first frame update
    void Start()
    {
        Tile = transform.Find("Tile").gameObject;
        TileColor = Tile.transform.Find("StateAffordance").GetComponent<SpriteRenderer>();

        if (StatefulColor) {
            SetTileColor(UntouchedColor);
        } else {
            SetTileColor(UnpressedColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DepressAnimate();
        ReleaseAnimate();
    }

    public void SetTileColor(Color color) {
        TileColor.color = color;
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("OnTriggerEnter");
        PlayersOnTile += 1;
        if (PlayersOnTile == 1) {
            DepressAnimateTime = ANIMATION_DURATION;
            SetDepressAnimationTime();
            OnDepress();
            foreach(EffectOnDepress effect in DepressEffects) {
                effect.Trigger();
            }
            foreach(EffectOnStayDepress effect in StayDepressEffects) {
                effect.AddDepress(other);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        //Debug.Log("OnTriggerExit");
        PlayersOnTile -= 1;
        if (PlayersOnTile == 0) {
            ReleaseAnimateTime = ANIMATION_DURATION;
            SetReleaseAnimationTime();
            OnRelease();
            foreach (EffectOnStayDepress effect in StayDepressEffects) {
                effect.RemoveDepress(other);
            }
        }
    }

    public virtual void SetDepressAnimationTime() {
        DepressAnimateTime = LerpPositionToTime(true);
        Debug.Log("Set DepressAnimateTime " + DepressAnimateTime);
    }

    public virtual void  SetReleaseAnimationTime() {
        ReleaseAnimateTime = LerpPositionToTime(false);
        Debug.Log("Set ReleaseAnimateTime " + ReleaseAnimateTime);
    }

    public virtual float LerpPositionToTime(bool IsDepress) {
        float t = Mathf.InverseLerp(DEPRESS_HEIGHT, NORMAL_HEIGHT, Tile.transform.position.y);
        if (!IsDepress) {
            t = 1f - t;
        }
        return t * ANIMATION_DURATION - EXACT_ANIMATION_OFFSET;
    }

    public virtual float LerpTimeToPosition(bool IsDepress, float AnimateTime) {
        float t = Mathf.InverseLerp(ANIMATION_DURATION, 0, AnimateTime);
        if (!IsDepress) {
            t = 1f - t;
        }
        return Mathf.Lerp(NORMAL_HEIGHT, DEPRESS_HEIGHT, t);
    }

    public virtual void DepressAnimate() {
        // hack so that we can easily know when to snap to 0
        if (DepressAnimateTime < 0) {
            if (DepressAnimateTime > -1f * EXACT_ANIMATION_OFFSET) {
                DepressAnimateTime -= Time.deltaTime;
                Tile.transform.localPosition = new Vector3(Tile.transform.localPosition.x, LerpTimeToPosition(true, DepressAnimateTime + EXACT_ANIMATION_OFFSET));
            } else {
                DepressAnimateTime = 0;
                Tile.transform.localPosition = new Vector3(Tile.transform.localPosition.x, DEPRESS_HEIGHT);
            }
        }
    }

    public virtual void ReleaseAnimate() {
        if (ReleaseAnimateTime < 0) {
            if (ReleaseAnimateTime > -1f * EXACT_ANIMATION_OFFSET) {
                ReleaseAnimateTime -= Time.deltaTime;
                Tile.transform.localPosition = new Vector3(Tile.transform.localPosition.x, LerpTimeToPosition(false, ReleaseAnimateTime + EXACT_ANIMATION_OFFSET));
            } else {
                ReleaseAnimateTime = 0;
                Tile.transform.localPosition = new Vector3(Tile.transform.localPosition.x, NORMAL_HEIGHT);
            }
        }
    }

    public virtual void OnDepress() {
        if (StatefulColor) {
            SetTileColor(TriggeredColor);
        } else {
            SetTileColor(DepressedColor);
        }
    }

    public virtual void OnRelease() {
        if (!StatefulColor) {
            SetTileColor(UnpressedColor);
        }
    }
}
