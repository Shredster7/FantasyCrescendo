using System.Collections.Generic;
using UnityEngine;
using HouraiTeahouse.Events;

namespace HouraiTeahouse.SmashBrew {

    /// <summary>
    /// A EventHandler for spawning characters at the start of the match
    /// </summary>
    public class Spawn : EventHandlerBehaviour<MatchStartEvent> {

        [SerializeField, Tooltip("The spawn points for each of the characters")]
        private Transform[] _spawnPoints;

        /// <summary>
        /// Spawns players when the match begins.
        /// </summary>
        /// <param name="startEventArgs"></param>
        protected override void OnEvent(MatchStartEvent startEventArgs) {
            var i = 0;
            IEnumerator<Player> activePlayers = Player.ActivePlayers.GetEnumerator();
            while (i < _spawnPoints.Length && activePlayers.MoveNext()) {
                Player player = activePlayers.Current;
                Character runtimeCharacter = player.Spawn(_spawnPoints[i]);
                i++;
                if (runtimeCharacter == null)
                    continue;

                //TODO: Fix this hack, get netplay working
                runtimeCharacter.gameObject.SetActive(true);
                runtimeCharacter.name = string.Format("Player {0} ({1})", player.PlayerNumber + 1, player.SpawnedCharacter.name);
                EventManager.Publish(new PlayerSpawnEvent {Player = player, PlayerObject = runtimeCharacter.gameObject});
            }
        }

    }

}