using UnityEngine;


namespace BookHero.Utility
{
    public class WaitForTween : CustomYieldInstruction
    {
        private readonly int m_Id;

        public override bool keepWaiting => LeanTween.isTweening(m_Id);

        public WaitForTween(int id) => m_Id = id;
    }
}