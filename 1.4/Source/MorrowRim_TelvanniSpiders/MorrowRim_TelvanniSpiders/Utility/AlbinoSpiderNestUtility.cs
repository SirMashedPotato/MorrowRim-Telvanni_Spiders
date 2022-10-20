using RimWorld;
using Verse;

namespace MorrowRim_TelvanniSpiders
{
    public static class AlbinoSpiderNestUtility
    {
        public static Thing SpawnTunnels(int hiveCount, Map map, bool spawnAnywhereIfNoGoodCell = false, bool ignoreRoofedRequirement = false, string questTag = null)
        {
            IntVec3 loc;

            if (!RCellFinder.TryFindRandomCellNearTheCenterOfTheMapWith(delegate (IntVec3 x)
            {
                if (!x.Standable(map) || x.Fogged(map))
                {
                    return false;
                }
                bool result = false;
                int num = GenRadial.NumCellsInRadius(3f);
                for (int j = 0; j < num; j++)
                {
                    IntVec3 c = x + GenRadial.RadialPattern[j];
                    if (c.InBounds(map))
                    {
                        TerrainDef terrain = c.GetTerrain(map);
                        if (terrain != null && terrain.affordances.Contains(TerrainAffordanceDefOf.Diggable))
                        {
                            result = true;
                            break;
                        }
                    }
                }
                return result;
            }, map, out loc))
            {
                return null;
            }

            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.MorrowRim_AlbinoSpiderNestSpawner, null), loc, map, WipeMode.FullRefund);
            QuestUtility.AddQuestTag(thing, questTag);
            for (int i = 0; i < hiveCount - 1; i++)
            {
                loc = loc.RandomAdjacentCell8Way().RandomAdjacentCell8Way();
                if (loc.IsValid)
                {
                    thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.MorrowRim_AlbinoSpiderNestSpawner, null), loc, map, WipeMode.FullRefund);
                    QuestUtility.AddQuestTag(thing, questTag);
                }
            }
            return thing;
        }
    }
}