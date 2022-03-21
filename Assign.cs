//我承认这里的代码写的不好，但是暂时找不到更好的办法了
//到这里，游戏规则已经写死了，所以这是这个项目不好的地方。
//I admited the code here was very bad, but I cant find a way better than this.
//The code here means game rules cant change again,thats the bad.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfKillen
{
    internal class Assign
    {

        //分配玩家角色
        public int[] AssignPlayerRole(int mode, int[] playerArr)
        {

            if (mode == 6)
            {
                //分配女巫
                if (playerArr[0] != 3) { playerArr = SixPlayersWhoWitch(playerArr); }
                //分配猎人
                if (playerArr[0] != 4) { playerArr = SixPlayersWhoHunter(playerArr); }
                //分配白痴
                if(playerArr[0] != 7) { playerArr = SixPlayersWhoStupid(playerArr); }
                //分配平民
                if(playerArr[0] != 2) { playerArr = SixPlayersWhoVillager(playerArr); }
                //分配狼人
                if(playerArr[0]!=1) { playerArr = SixPlayersWhoWolf(playerArr, false); }
                else { playerArr = SixPlayersWhoWolf(playerArr, true); }
                return playerArr;
            }
            else if(mode == 10)
            {
                if (playerArr[0] != 5) { playerArr = TenPlayersWhoProphet(playerArr); }
                //分女巫
                if (playerArr[0] != 3) { playerArr = TenPlayersWhoWitch(playerArr); }
                //分配猎人
                if (playerArr[0] != 4) { playerArr = TenPlayersWhoHunter(playerArr); }
                //分配守卫
                if (playerArr[0] != 7) { playerArr = TenPlayersWhoGuard(playerArr); }
                //分配村民
                if (playerArr[0] != 2) { playerArr = TenPlayersWhoVillager(playerArr, false); }
                else { playerArr = TenPlayersWhoWolf(playerArr, true); }
                //狼人
                if (playerArr[0] != 1) { playerArr = TenPlayersWhoWolf(playerArr, false); }
                else { playerArr = TenPlayersWhoWolf(playerArr, true); }
                return playerArr;
            }
            return playerArr;
        }
        int temp;
        private int[] SixPlayersWhoWitch(int[] playerArr)
        {
            Random random = new Random();

            for (; ; )
            {
                temp = random.Next(2, playerArr.Length + 1);//生成随机数 生成随机数很迷。。。拖慢了效率，但我不想改
                temp--;//数组的特性 有0下标
                if (playerArr[temp] == 0 && temp < 6)
                {
                    playerArr[temp] = 3;//三代表着女巫
                    break;
                }
                else
                {
                    continue;//如果没有分配到，就继续循环
                }
            }
            return playerArr;
        }

        private int[] SixPlayersWhoHunter(int[] playerArr)
        {
            Random random = new Random();
            for (; ; )
            {
                temp = random.Next(2, playerArr.Length + 1);//生成随机数
                temp--;
                if ((playerArr[temp] == 0 && temp < 6) || (playerArr[temp] != 3 && temp < 6))//排除已经出现的3和没有赋值的0
                {
                    playerArr[temp] = 4;//4代表着猎人
                    break;
                }
                else
                {
                    continue;//如果没有分配到，就继续循环
                }
            }
            return playerArr;
        }

        private int[] SixPlayersWhoStupid(int[] playerArr)
        {
            Random random = new Random();
            for (; ; )
            {
                temp = random.Next(2, playerArr.Length + 1);//生成随机数
                temp--;
                if ((playerArr[temp] == 0 && temp < 6) || (playerArr[temp] != 3 && playerArr[temp] != 4 && temp < 6))
                {
                    playerArr[temp] = 7;//7代表着白痴
                    break;
                }
                else
                {
                    continue;//如果没有分配到，就继续循环
                }
            }
            return playerArr;
        }

        private int[] SixPlayersWhoVillager(int[] playerArr)
        {
            Random random = new Random();
            for (; ; )
            {
                temp = random.Next(2, playerArr.Length + 1);//生成随机数
                temp--;
                if ((playerArr[temp] == 0 && temp < 6) || (playerArr[temp] != 3 && playerArr[temp] != 4 && playerArr[temp] != 7 && temp < 6))
                {
                    playerArr[temp] = 2;//2代表着平民
                    break;
                }
                else
                {
                    continue;//如果没有分配到，就继续循环

                }
            }
            return playerArr;
        }

        private int[] SixPlayersWhoWolf(int[] playerArr,bool isPlayerWolf)
        {
            Random random = new Random();
            if (isPlayerWolf)
            {
                for (; ; )
                {

                    temp = random.Next(2, playerArr.Length + 1);//生成随机数
                    temp--;

                    if ((playerArr[temp] == 0 && temp < 6) || (playerArr[temp] != 3 && playerArr[temp] != 4 && playerArr[temp] != 7 && playerArr[temp] != 2 && playerArr[temp] != 1 && temp < 6))//这里是已经分配了一个的情况
                    {
                        playerArr[temp] = 1;//1代表着狼人
                        break;
                    }
                    else
                    {
                        continue;
                    }

                }
                return playerArr;
            }
            else
            {
                for (int i = 0; i < 6; i++)//这是玩家不是狼人的情况
                {
                    if(playerArr[i]==0)
                    {
                        playerArr[i] = 1;
                    }
                }
                return playerArr;
            }

        }


        //-----------------十人------------------
        //这里是分配预言家的
        private int[] TenPlayersWhoProphet(int[] playerArr)
        {
            Random r = new Random();
            int arrSubscript = r.Next(2, 10);
            playerArr[arrSubscript] = 5;
            return playerArr;
        }

        //女巫的
        private int[] TenPlayersWhoWitch(int[] playerArr)
        {
            Random r = new Random();
            int temp;
            for(; ; )
            {
                temp = r.Next(2, 10);
                if(playerArr[temp] == 0 || playerArr[temp] != 5)
                {
                    playerArr[temp] = 3;
                    break;
                }
                continue;
            }
            return playerArr;
        }

        //猎人的
        private int[] TenPlayersWhoHunter(int[] playerArr)
        {
            Random r = new Random();
            int temp;
            for (; ; )
            {
                temp = r.Next(2, 10);
                if (playerArr[temp] == 0 || (playerArr[temp] != 5 && playerArr[temp]!=3))
                {
                    playerArr[temp] =4;
                    break;
                }
                continue;
            }
            return playerArr;
        }

        //守卫的
        private int[] TenPlayersWhoGuard(int[] playerArr)
        {
            Random r = new Random();
            int temp;
            for (; ; )
            {
                temp = r.Next(2, 10);
                if (playerArr[temp] == 0 || (playerArr[temp] != 5 && playerArr[temp] != 3 && playerArr[temp] != 4))
                {
                    playerArr[temp] = 7;
                    break;
                }
                continue;
            }
            return playerArr;
        }

        //村名的
        private int[] TenPlayersWhoVillager(int[] playerArr,bool isPlayerVillager)
        {
            Random r = new Random();
            int VillagerNum = 3;
            if (isPlayerVillager) { VillagerNum--; }
            int temp;
            for(int j = 0; j<VillagerNum; j++)
            {
                if (j == 0)
                {
                    temp = r.Next(1, 10);
                    if (playerArr[temp] == 0 || (playerArr[temp] != 5 && playerArr[temp] != 3 && playerArr[temp] != 4 && playerArr[temp] != 7))
                    {
                        playerArr[temp] = 2;
                    }
                    else
                    {
                        j--;
                    }
                }
                else
                {
                    temp = r.Next(1, 10);
                    if(playerArr[temp] == 0 || (playerArr[temp] != 5 && playerArr[temp] != 3 && playerArr[temp] != 4 && playerArr[temp] != 7&&playerArr[temp]!=2))
                    {
                        playerArr[temp] = 2;
                    }
                    else
                    {
                        j--;
                    }
                }
            }
            return playerArr;
        }

        //狼人的
        private int[] TenPlayersWhoWolf(int[] playerArr, bool isPlayerWolf)
        {
            Random r = new Random();
            int VillagerNum = 3;
            if (isPlayerWolf) { VillagerNum--; }
            int temp;
            for (int j = 0; j < VillagerNum; j++)
            {
                if (j == 0)
                {
                    temp = r.Next(1, 10);
                    if (playerArr[temp] == 0 || (playerArr[temp] != 5 && playerArr[temp] != 3 && playerArr[temp] != 4 && playerArr[temp] != 7&& playerArr[temp] != 2))
                    {
                        playerArr[temp] = 1;
                    }
                    else
                    {
                        j--;
                    }
                }
                else
                {
                    temp = r.Next(1, 10);
                    if (playerArr[temp] == 0 || (playerArr[temp] != 5 && playerArr[temp] != 3 && playerArr[temp] != 4 && playerArr[temp] != 7 && playerArr[temp] != 2 && playerArr[temp] != 1))
                    {
                        playerArr[temp] = 1;
                    }
                    else
                    {
                        j--;
                    }
                }
            }
            return playerArr;
        }

    } 
}


