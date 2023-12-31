using RewardModel_namespace;

namespace RewwardIService_namespace
{
	public interface Ireward
	{
		String rewardPoints(RewardModel reward);
		RewardModel getuserReward(String userid);

	}

}