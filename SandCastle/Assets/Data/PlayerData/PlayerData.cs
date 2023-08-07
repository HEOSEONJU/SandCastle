using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    [System.Serializable]
    public class PlayerData
    {

        public int gem;
        public int gold;
        public int mud;
        public List<CharInfo> havetCharIds;
        public CharInfo fightCharIds;


        public List<bool> CharUnlock;
        
        public List<StageState> StageClear;






        public PlayerData()
        {

            havetCharIds=new List<CharInfo>();
            CharUnlock = new List<bool>();
            gem = 0;
            gold = 0;
            mud = 0;

            CharInfo temp = new CharInfo("character000001");
            havetCharIds.Add(temp);
            fightCharIds = temp;
            CharUnlock.Add(true);
            temp = new CharInfo("character000002");
            havetCharIds.Add(temp);
            CharUnlock.Add(false);
            temp = new CharInfo("character000003");
            havetCharIds.Add(temp);

            CharUnlock.Add(false);
            StageClear = new List<StageState>();
            for (int i = 0; i < 3; i++)
            {
                StageClear.Add(StageState.Lock);
            }
            StageClear[0] = StageState.Unlock;
        }


        public bool GetChar(string id)//획득혹은 돌파하면 true 최대돌파상태면 false
        {
            int findindex = havetCharIds.FindIndex(x => x.id == id);
            if(findindex == -1)
            {
                CharInfo temp = new CharInfo(id);
                havetCharIds.Add(temp);
                return true;
            }
            return havetCharIds[findindex].IncreaseBreak();



        }
        public void GetGem(int value)
        {
            gem += value;
        }
        public void GetGold(int value)
        {
            gold += value;
        }
        public void GetMud(int value)
        {
            mud += value;
        }

    }





    [System.Serializable]
    public class CharInfo
    {
        public string id;
        public int level;
        public int breaKLim;

        public CharInfo(string id,int level=1,int breaklim=1)
        {
            this.id = id;
            this.level = level;
            breaKLim = breaklim;

        }

        public bool IncreaseBreak()
        {
            if(breaKLim>=5)
            {
                return false;
            }

            breaKLim++;
            return true;
        }
    }

}
