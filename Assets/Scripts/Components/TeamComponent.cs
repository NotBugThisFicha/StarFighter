using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponent : MonoBehaviour, ITeamComponent
    {
        public bool IsPlayer => this.isPlayer;

        [SerializeField, ReadOnlyField]
        private bool isPlayer;

        public void SetStatusPlayer(bool status)=> isPlayer = status;
    }
}