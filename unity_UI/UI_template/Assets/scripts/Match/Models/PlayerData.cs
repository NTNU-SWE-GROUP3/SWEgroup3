public class PlayerData
{
    public int level;		    // 玩家等級

    /* Normal */
	public int experience;		// 一般場經驗值
	public int streak;		    // 連勝場次
	public int total_match;	    // 總遊戲場數
	public int total_win;		// 總勝場數

    /* Rank */
	public int rank;		    // 排位
	public int rank_XP;		    // 排位場經驗值
	public int rank_streak;	    // 排位場連勝場數

    /* Game */
	public int[] skill = new int[3];	    // 技能
	public int[] clothes = new int[6];		// 服裝
	public int coin;		    // 金幣
}