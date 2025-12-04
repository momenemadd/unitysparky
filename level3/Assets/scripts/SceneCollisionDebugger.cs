using UnityEngine;

// Temporary scene diagnostic helper — remove after debugging
public class SceneCollisionDebugger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("SceneCollisionDebugger: Starting diagnostics");

        // Players
        var players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log($"SceneCollisionDebugger: Found {players.Length} GameObject(s) with tag 'Player'");
        foreach (var p in players)
        {
            var col = p.GetComponent<Collider2D>();
            var rb = p.GetComponent<Rigidbody2D>();
            var stats = p.GetComponent<PlayerStats>();
            Debug.Log($"Player: name={p.name}, active={p.activeSelf}, layer={p.layer} ({LayerMask.LayerToName(p.layer)}), hasCollider={(col!=null)}, isTrigger={(col!=null?col.isTrigger:false)}, hasRigidbody={(rb!=null)}, rbType={(rb!=null?rb.bodyType.ToString():"none")}, hasPlayerStats={(stats!=null)}");
        }

        // Check TeslaCoil prefabs in scene
        var coils = FindObjectsOfType<TeslaCoil>();
        Debug.Log($"SceneCollisionDebugger: Found {coils.Length} TeslaCoil(s) in scene");
        foreach (var coil in coils)
        {
            var prefab = coil.electricBoltPrefab;
            Debug.Log($"TeslaCoil '{coil.name}' boltPrefab={(prefab!=null?prefab.name:"null")}");
            if (prefab != null)
            {
                var pcol = prefab.GetComponent<Collider2D>();
                var prb = prefab.GetComponent<Rigidbody2D>();
                Debug.Log($"BoltPrefab '{prefab.name}': hasCollider={(pcol!=null)}, isTrigger={(pcol!=null?pcol.isTrigger:false)}, hasRigidbody={(prb!=null)}, layer={prefab.layer} ({LayerMask.LayerToName(prefab.layer)})");

                // Compare layer collision settings between each player and this bolt prefab
                foreach (var p in players)
                {
                    bool ignore = Physics2D.GetIgnoreLayerCollision(p.layer, prefab.layer);
                    Debug.Log($"Layer collision Player({LayerMask.LayerToName(p.layer)}) <-> Bolt({LayerMask.LayerToName(prefab.layer)}): ignore={ignore}");
                }
            }
        }

        if (players.Length == 0)
        {
            Debug.Log("SceneCollisionDebugger: No GameObject with tag 'Player' found in this scene.");
        }

        Debug.Log("SceneCollisionDebugger: Diagnostics complete — remove this script when finished.");
    }
}
