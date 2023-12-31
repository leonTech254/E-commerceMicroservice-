using DBconnection_namespace;
using RewardModel_namespace;
using RewwardIService_namespace;

namespace RewardService_namespace

{
	public class RewardService:Ireward
	{
		private readonly DBconn _dBconn;
		public RewardService(DBconn dBconn) {
		_dBconn = dBconn;
		}

		public RewardModel getuserReward(string userid)
		{
			List<RewardModel> rewards = _dBconn.rewards.ToList();
			RewardModel reward = rewards.FirstOrDefault(e=>e.userId==userid);
			return reward;
			
		}

		public string rewardPoints(RewardModel reward)
		{

			RewardModel dbRewards = getuserReward(reward.userId);
			if(dbRewards != null)
			{
				int initialPoints = dbRewards.userpoints;
				initialPoints += reward.userpoints;
				dbRewards.userpoints = initialPoints;
				_dBconn.rewards.Update(reward);
			}else
			{
				_dBconn.rewards.Add(reward);
			}

			_dBconn.SaveChanges();
			return "Added points successfully";
		}
	}
}