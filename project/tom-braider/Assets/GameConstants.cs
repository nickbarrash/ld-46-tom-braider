using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    public static float PLAYER_SPEED_MULTIPLE = 5.0f;
    public static float PLAYER_FOLLOW_DISTANCE = 5.0f;

    public static LayerMask CHARACTER_LAYER = LayerMask.GetMask("Character");

    public static bool IsCharacterLayer(int layer) {
        return CHARACTER_LAYER == (CHARACTER_LAYER | (1 << layer));
    }
}
