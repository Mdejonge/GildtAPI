using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GildtAPI.DAO;
using GildtAPI.Model;

namespace GildtAPI.Controllers
{
    class RewardsController : Singleton<RewardsController>
    {
        public async Task<Reward[]> GetAllRewards()
        {
            return await RewardsDAO.Instance.GetAllRewards();
        }

        public async Task<Reward> GetRewardByName(string rewardName)
        {
            return await RewardsDAO.Instance.GetRewardByName(rewardName);
        }

        public async Task<Reward> GetRewardById(int rewardId)
        {
            return await RewardsDAO.Instance.GetRewardById(rewardId);
        }

        public async Task<Reward[]> GetUserRewards(int count, int id)
        {
            return await RewardsDAO.Instance.GetUserRewards(count, id);
        }

        public async Task<bool> CreateReward(string name, string description)
        {
            return await RewardsDAO.Instance.CreateReward(name, description);
        }

        public async Task<int> EditReward(int rewardId, string name, string description)
        {
            return await RewardsDAO.Instance.EditReward(rewardId, name, description);
        }
    }
}
