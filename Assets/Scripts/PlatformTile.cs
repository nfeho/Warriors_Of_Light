using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlatformTile : Tile
{
    public Sprite[] m_Sprites;
    public Sprite m_Preview;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        return base.StartUp(position, tilemap, go);
    }

    // This refreshes itself and other PlatformTiles that are orthogonally and diagonally adjacent
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        for (int yd = -1; yd <= 1; yd++)
            for (int xd = -1; xd <= 1; xd++)
            {
                Vector3Int position = new Vector3Int(location.x + xd, location.y + yd, location.z);
                if (HasPlatformTile(tilemap, position))
                    tilemap.RefreshTile(position);
            }
    }
    // This determines which sprite is used based on the PlatformTiles that are adjacent to it and rotates it to fit the other tiles.
    // As the rotation is determined by the PlatformTile, the TileFlags.OverrideTransform is set for the tile.
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(location, tilemap, ref tileData);
        int mask = HasPlatformTile(tilemap, location + new Vector3Int(0, 1, 0)) ? 1 : 0;
        mask += HasPlatformTile(tilemap, location + new Vector3Int(1, 0, 0)) ? 2 : 0;
        mask += HasPlatformTile(tilemap, location + new Vector3Int(0, -1, 0)) ? 4 : 0;
        mask += HasPlatformTile(tilemap, location + new Vector3Int(-1, 0, 0)) ? 8 : 0;
        int index = GetIndex((byte)mask);
        if (index >= 0 && index < m_Sprites.Length)
        {
            tileData.sprite = m_Sprites[index];
            tileData.color = Color.white;
            var m = tileData.transform;
            m.SetTRS(Vector3.zero, GetRotation((byte)mask), Vector3.one);
            tileData.transform = m;
            tileData.flags = TileFlags.LockTransform;
            tileData.colliderType = ColliderType.None;
        }
        else
        {
            Debug.LogWarning("Not enough sprites in PlatformTile instance");
        }
    }
    // This determines if the Tile at the position is the same PlatformTile.
    private bool HasPlatformTile(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }
    // The following determines which sprite to use based on the number of adjacent PlatformTiles
    private int GetIndex(byte mask)
    {
        /*switch (mask)
        {
            case 0:
            case 5:
            case 15: return 0;
            case 4:
            case 10:
            case 14: return 1;
            case 8:
            case 12: return 2;
            case 13: return 3;
            case 9: return 4;
            case 1:
            case 11: return 5;
            case 3: return 6;
            case 7: return 7;
            case 2:
            case 6: return 8;
        }
        return -1;*/

        switch (mask)
        {
            case 0b0000:
            case 0b1011:
            case 0b1111: return 0;
            case 0b1100:
            case 0b0110: return 3;
            case 0b0010:
            case 0b1000:
            case 0b1010:
            case 0b1110: return 1;
            case 0b0011:
            case 0b0111:
            case 0b1001:
            case 0b1101: return 2;
        }
        return 0;
    }
    // The following determines which rotation to use based on the positions of adjacent PlatformTiles
    private Quaternion GetRotation(byte mask)
    {
        switch (mask)
        {
            case 0b0011:
            case 0b0111: return Quaternion.Euler(0f, 180f, 0f);
            case 0b0110: return Quaternion.Euler(0f, 0f, 90f);
        }
        return Quaternion.Euler(0f, 0f, 0f);
    }
#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a PlatformTile Asset
    [MenuItem("Assets/Create/PlatformTile")]
    public static void CreatePlatformTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Platform Tile", "New Platform Tile", "Asset", "Save Platform Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<PlatformTile>(), path);
    }
#endif
}
