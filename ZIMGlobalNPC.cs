using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

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
                    //check if the current zombie is a clone. If not, then give it a clone
                    if (source is EntitySource_Misc misc && misc.Context.Equals("Zombie Clone"))
                    {
                    }
                    else
                    {
                        //make it spawn in 8 zombies
                        for (int i = 0; i < 8; i++) 
                        {
                            //set up the variables for the weighted RNG
                            int chance = Main.rand.Next(101);
                            int[] zombieGroup = ffVar.zombies.normalZombies;

                            //if it's a blood moon, it will always be a blood zombie
                            if (Main.bloodMoon)
                            {
                                zombieGroup = ffVar.zombies.bloodZombies;
                            } else
                            {

                                //in pre hardmode, youre more likely to bump into normal zombies
                                if (!Main.hardMode)
                                {
                                    if (chance >= 50 && chance < 90)
                                        zombieGroup = ffVar.zombies.variantZombies;
                                    else if (chance >= 90 && chance < 96)
                                        zombieGroup = ffVar.zombies.bloodZombies;
                                    else if (chance >= 96 && chance < 99)
                                        zombieGroup = ffVar.zombies.specialZombies;
                                    else if (chance >= 99 && chance < 100)
                                        zombieGroup = ffVar.zombies.hardmodeZombies;
                                }
                                else
                                {
                                    //in hardmode, youre more likely to bump into most of the zombies
                                    if (chance >= 20 && chance < 40)
                                        zombieGroup = ffVar.zombies.variantZombies;
                                    else if (chance >= 40 && chance < 65)
                                        zombieGroup = ffVar.zombies.bloodZombies;
                                    else if (chance >= 65 && chance < 90)
                                        zombieGroup = ffVar.zombies.specialZombies;
                                    else if (chance >= 90 && chance < 100)
                                        zombieGroup = ffVar.zombies.hardmodeZombies;
                                }
                            }

                            //get a random normal zombie from the list
                            int randZom = zombieGroup[Main.rand.Next(zombieGroup.Length)];

                            //spawn in a new zombie on top of the current one with the reason being a clone
                            int index = NPC.NewNPC(new EntitySource_Misc("Zombie Clone"), (int)npc.position.X, (int)npc.position.Y, randZom);

                            //make the npc mean nothing in the npc slots
                            Main.npc[index].npcSlots = 0;
                        }
                    }
                }
            }
            base.OnSpawn(npc, source);
        }
    }
}
