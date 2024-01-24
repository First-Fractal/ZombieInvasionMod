﻿using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace ZombieInvasionMod
{
    internal class ZIMGlobalNPC : GlobalNPC
    {
        //function that runs whenever a npc spawns in
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            //loop through every vanilla zombie
            foreach (int zombie in ffVar.zombies.allZombies)
            {
                //check if the current npc is a zombie
                if (npc.type == zombie)
                {
                    //get a random normal zombie from the list
                    int randZom = ffVar.zombies.normalZombies[Main.rand.Next(ffVar.zombies.normalZombies.Length)];

                    //spawn in a new zombie on top of the current one with the reason being a clone
                    NPC.NewNPC(new EntitySource_Misc("Zombie Clone"), (int)npc.position.X, (int)npc.position.Y, randZom);
                }
            }
            base.OnSpawn(npc, source);
        }
    }
}