using System;
using Verse;
using Verse.Sound;
using RimWorld;
using Verse.AI;
using MorrowRim_Telvanni;

namespace MorrowRim_TelvanniSpiders
{
    public class Projectile_Spider : Projectile_Explosive
    {

        private static readonly RecordDef spiderThrowTracker = DefDatabase<RecordDef>.GetNamed("MorrowRim_TelvanniSpidersThrown");

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            if (GetPawnKind(def, out PawnKindDef spiderKind))
            {
                Pawn newPawn = PawnGenerator.GeneratePawn(spiderKind, Faction.OfPlayer);
                PawnUtility.TrySpawnHatchedOrBornPawn(newPawn, this);
                AlbinoSpiderUtility.PlaySpiderSound(newPawn);

                if (launcher is Pawn p)
                {
                    IncrementRecord(p);
                }

                if (intendedTarget != null && intendedTarget.Pawn != null)
                {
                    Job job = new Job(JobDefOf.AttackMelee, intendedTarget);
                    newPawn.jobs.StartJob(job);
                }
            }
            landed = true;
            Destroy(DestroyMode.Vanish);
        }

        private bool GetPawnKind(ThingDef parent, out PawnKindDef toSpawn)
        {
            CreateAtProperties props = CreateAtProperties.Get(parent);
            if (props != null && props.pawnKindToSpawn != null)
            {
                toSpawn = props.pawnKindToSpawn;
                return true;
            }
            toSpawn = null;
            return false;
        }

        private void IncrementRecord(Pawn pawn)
        {
            if (spiderThrowTracker != null)
            {
                pawn.records.Increment(spiderThrowTracker);
            }
        }
    }
}