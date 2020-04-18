using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTile : MonoBehaviour
{
    public TILE_COLOR StartColor = TILE_COLOR.BLUE;

    private int PlayersOnTile = 0;

    private GameObject Tile;
    private SpriteRenderer TileColor;

    private float EXACT_ANIMATION_OFFSET = 1f;
    private float ANIMATION_DURATION = 0.06f;
    private float ReleaseAnimateTime = 0f;
    private float DepressAnimateTime = 0f;

    private float DEPRESS_HEIGHT = -0.1f;
    private float NORMAL_HEIGHT = 0f;

    public enum TILE_COLOR {
        BLUE,
        GREEN,
        YELLOW,
        RED,
        BLACK
    }


    // Start is called before the first frame update
    void Start()
    {
        Tile = transform.Find("Tile").gameObject;
        TileColor = Tile.transform.Find("StateAffordance").GetComponent<SpriteRenderer>();

        SetTileColor(StartColor);
    }

    // Update is called once per frame
    void Update()
    {
        DepressAnimate();
        ReleaseAnimate();
    }

    public void SetTileColor(TILE_COLOR color) {
        switch (color) {
            case TILE_COLOR.BLUE:
                TileColor.color = Color.blue;
                break;
            case TILE_COLOR.GREEN:
                TileColor.color = Color.green;
                break;
            case TILE_COLOR.YELLOW:
                TileColor.color = Color.yellow;
                break;
            case TILE_COLOR.RED:
                TileColor.color = Color.red;
                break;
            case TILE_COLOR.BLACK:
                TileColor.color = Color.black;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter");
        PlayersOnTile += 1;
        if (PlayersOnTile == 1) {
            DepressAnimateTime = ANIMATION_DURATION;
            SetDepressAnimationTime();
            OnDepress();
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("OnTriggerExit");
        PlayersOnTile -= 1;
        if (PlayersOnTile == 0) {
            ReleaseAnimateTime = ANIMATION_DURATION;
            SetReleaseAnimationTime();
            OnRelease();
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
        Debug.Log("DepressAnimate " + DepressAnimateTime);
        // hack so that we can easily know when to snap to 0
        if (DepressAnimateTime < 0) {
            if (DepressAnimateTime > -1f * EXACT_ANIMATION_OFFSET) {
                DepressAnimateTime -= Time.deltaTime;
                Tile.transform.position = new Vector3(Tile.transform.position.x, LerpTimeToPosition(true, DepressAnimateTime + EXACT_ANIMATION_OFFSET));
            } else {
                DepressAnimateTime = 0;
                Tile.transform.position = new Vector3(Tile.transform.position.x, DEPRESS_HEIGHT);
            }
        }
    }

    public virtual void ReleaseAnimate() {
        Debug.Log("ReleaseAnimate " + ReleaseAnimateTime);
        if (ReleaseAnimateTime < 0) {
            if (ReleaseAnimateTime > -1f * EXACT_ANIMATION_OFFSET) {
                ReleaseAnimateTime -= Time.deltaTime;
                Tile.transform.position = new Vector3(Tile.transform.position.x, LerpTimeToPosition(false, ReleaseAnimateTime + EXACT_ANIMATION_OFFSET));
            } else {
                ReleaseAnimateTime = 0;
                Tile.transform.position = new Vector3(Tile.transform.position.x, NORMAL_HEIGHT);
            }
        }
    }

    public virtual void OnStayDepress() { }

    public virtual void OnDepress() { SetTileColor(TILE_COLOR.GREEN); }

    public virtual void OnRelease() { SetTileColor(TILE_COLOR.BLUE); }
}
