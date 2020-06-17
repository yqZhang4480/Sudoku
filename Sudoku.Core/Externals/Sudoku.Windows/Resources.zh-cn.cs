﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SourceDictionary = System.Collections.Generic.IReadOnlyDictionary<string, string>;

namespace Sudoku.Windows
{
	partial class Resources
	{
		/// <summary>
		/// The language source for the globalization string "<c>zh-cn</c>".
		/// </summary>
		/// <remarks>
		/// Here we use reflection to call and use this field, which cannot be recognized by
		/// Roslyn, so we should suppress the complier warning IDE0052.
		/// </remarks>
		[SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "<Pending>")]
		private static readonly SourceDictionary LangSourceZhCn = new Dictionary<string, string>
		{
			// GridProgressResult
			["UnsolvedCells"] = "剩余空格总数：",
			["UnsolvedCandidates"] = "，剩余候选数总数：",

			// StepFinder
			["ProgressAlsWWing"] = "待定数组-W-Wing",
			["ProgressAlsXyWing"] = "待定数组-XY-Wing",
			["ProgressSinglyLinkedAlsXz"] = "待定数组-双强链/环",
			["ProgressDeathBlossom"] = "死亡绽放",
			["ProgressErip"] = "对交空矩形",
			["ProgressMsls"] = "网",
			["ProgressSdc3d"] = "三维融合跨区数组",
			["ProgressSdc"] = "融合跨区数组",
			["ProgressSkLoop"] = "多米诺环",
			["ProgressAic"] = "普通链/区块链",
			["ProgressJe"] = "初级飞鱼导弹",
			["ProgressSe"] = "高级飞鱼导弹",
			["ProgressFrankenSwordfish"] = "Hobiwan 鱼",
			["ProgressXWing"] = "普通鱼",
			["ProgressAlmostLockedPair"] = "欠一数组",
			["ProgressPointing"] = "区块",
			["ProgressBowmanBingo"] = "试数",
			["ProgressBruteForce"] = "计算机试数",
			["ProgressPom"] = "模板叠加",
			["ProgressTemplateSet"] = "模板",
			["ProgressEmptyRectangle"] = "空矩形",
			["ProgressGuardian"] = "守护者",
			["ProgressTurbotFish"] = "双强链",
			["ProgressNakedSingle"] = "出数技巧",
			["ProgressNakedPair"] = "数组",
			["ProgressGsp"] = "宇宙法",
			["ProgressBugType1"] = "全双值格致死解法",
			["ProgressXrType1"] = "拓展矩形",
			["ProgressUlType1"] = "唯一环",
			["ProgressBdpType1"] = "探长致命结构",
			["ProgressQdpType1"] = "淑芬致命结构",
			["ProgressUrType1"] = "唯一矩形/可规避矩形",
			["ProgressUsType1"] = "唯一矩阵",
			["ProgressWWing"] = "不规则 Wing",
			["ProgressXyWing"] = "规则 Wing",
			["Summary"] = "正在统计中…",

			// Separate words
			["Petal"] = "个花瓣",
			["Grouped"] = "区块",
			["Bug"] = "全双值格致死解法",
			["Rectangle"] = "矩形",
			["Avoidable"] = "可规避",
			["Unique"] = "唯一",
			["Hidden"] = "隐性",

			// Techniques
			["FullHouse"] = "Full House",
			["LastDigit"] = "Last Digit",
			["HiddenSingleRow"] = "行排除",
			["HiddenSingleColumn"] = "列排除",
			["HiddenSingleBlock"] = "宫排除",
			["NakedSingle"] = "唯一余数",
			["Pointing"] = "宫区块",
			["Claiming"] = "行列区块",
			["AlmostLockedPair"] = "欠一数对",
			["AlmostLockedTriple"] = "欠一三数组",
			["AlmostLockedQuadruple"] = "欠一四数组",
			["NakedPair"] = "显性数对",
			["NakedPairPlus"] = "显性数对 (+)",
			["LockedPair"] = "死锁数对",
			["HiddenPair"] = "隐性数对",
			["NakedTriple"] = "显性三数组",
			["NakedTriplePlus"] = "显性三数组 (+)",
			["LockedTriple"] = "死锁三数组",
			["HiddenTriple"] = "隐性三数组",
			["NakedQuadruple"] = "显性四数组",
			["NakedQuadruplePlus"] = "显性四数组 (+)",
			["HiddenQuadruple"] = "隐性四数组",
			["XWing"] = "二阶鱼",
			["FinnedXWing"] = "二阶鳍鱼",
			["SashimiXWing"] = "二阶退化鱼",
			["SiameseFinnedXWing"] = "二阶孪生鳍鱼",
			["SiameseSashimiXWing"] = "二阶孪生退化鱼",
			["FrankenXWing"] = "二阶宫内鱼",
			["FinnedFrankenXWing"] = "二阶宫内鳍鱼",
			["SashimiFrankenXWing"] = "二阶宫内退化鱼",
			["SiameseFinnedFrankenXWing"] = "二阶宫内孪生鳍鱼",
			["SiameseSashimiFrankenXWing"] = "二阶宫内孪生退化鱼",
			["MutantXWing"] = "二阶交叉鱼",
			["FinnedMutantXWing"] = "二阶交叉鳍鱼",
			["SashimiMutantXWing"] = "二阶交叉退化鱼",
			["SiameseFinnedMutantXWing"] = "二阶交叉孪生鳍鱼",
			["SiameseSashimiMutantXWing"] = "二阶交叉孪生退化鱼",
			["Swordfish"] = "三阶鱼",
			["FinnedSwordfish"] = "三阶鳍鱼",
			["SashimiSwordfish"] = "三阶退化鱼",
			["SiameseFinnedSwordfish"] = "三阶孪生鳍鱼",
			["SiameseSashimiSwordfish"] = "三阶孪生退化鱼",
			["FrankenSwordfish"] = "三阶宫内鱼",
			["FinnedFrankenSwordfish"] = "三阶宫内鳍鱼",
			["SashimiFrankenSwordfish"] = "三阶宫内退化鱼",
			["SiameseFinnedFrankenSwordfish"] = "三阶宫内孪生鳍鱼",
			["SiameseSashimiFrankenSwordfish"] = "三阶宫内孪生退化鱼",
			["MutantSwordfish"] = "三阶交叉鱼",
			["FinnedMutantSwordfish"] = "三阶交叉鳍鱼",
			["SashimiMutantSwordfish"] = "三阶交叉退化鱼",
			["SiameseFinnedMutantSwordfish"] = "三阶交叉孪生鳍鱼",
			["SiameseSashimiMutantSwordfish"] = "三阶交叉孪生退化鱼",
			["Jellyfish"] = "四阶鱼",
			["FinnedJellyfish"] = "四阶鳍鱼",
			["SashimiJellyfish"] = "四阶退化鱼",
			["SiameseFinnedJellyfish"] = "四阶孪生鳍鱼",
			["SiameseSashimiJellyfish"] = "四阶孪生退化鱼",
			["FrankenJellyfish"] = "四阶宫内鱼",
			["FinnedFrankenJellyfish"] = "四阶宫内鳍鱼",
			["SashimiFrankenJellyfish"] = "四阶宫内退化鱼",
			["SiameseFinnedFrankenJellyfish"] = "四阶宫内孪生鳍鱼",
			["SiameseSashimiFrankenJellyfish"] = "四阶宫内孪生退化鱼",
			["MutantJellyfish"] = "四阶交叉鱼",
			["FinnedMutantJellyfish"] = "四阶交叉鳍鱼",
			["SashimiMutantJellyfish"] = "四阶交叉退化鱼",
			["SiameseFinnedMutantJellyfish"] = "四阶交叉孪生鳍鱼",
			["SiameseSashimiMutantJellyfish"] = "四阶交叉孪生退化鱼",
			["Squirmbag"] = "五阶鱼",
			["FinnedSquirmbag"] = "Finned Squirmbag",
			["SashimiSquirmbag"] = "Sashimi Squirmbag",
			["SiameseFinnedSquirmbag"] = "Siamese Finned Squirmbag",
			["SiameseSashimiSquirmbag"] = "Siamese Sashimi Squirmbag",
			["FrankenSquirmbag"] = "Franken Squirmbag",
			["FinnedFrankenSquirmbag"] = "Finned Franken Squirmbag",
			["SashimiFrankenSquirmbag"] = "Sashimi Franken Squirmbag",
			["SiameseFinnedFrankenSquirmbag"] = "Siamese Finned Franken Squirmbag",
			["SiameseSashimiFrankenSquirmbag"] = "Siamese Sashimi Franken Squirmbag",
			["MutantSquirmbag"] = "Mutant Squirmbag",
			["FinnedMutantSquirmbag"] = "Finned Mutant Squirmbag",
			["SashimiMutantSquirmbag"] = "Sashimi Mutant Squirmbag",
			["SiameseFinnedMutantSquirmbag"] = "Siamese Finned Mutant Squirmbag",
			["SiameseSashimiMutantSquirmbag"] = "Siamese Sashimi Mutant Squirmbag",
			["Whale"] = "六阶鱼",
			["FinnedWhale"] = "Finned Whale",
			["SashimiWhale"] = "Sashimi Whale",
			["SiameseFinnedWhale"] = "Siamese Finned Whale",
			["SiameseSashimiWhale"] = "Siamese Sashimi Whale",
			["FrankenWhale"] = "Franken Whale",
			["FinnedFrankenWhale"] = "Finned Franken Whale",
			["SashimiFrankenWhale"] = "Sashimi Franken Whale",
			["SiameseFinnedFrankenWhale"] = "Siamese Finned Franken Whale",
			["SiameseSashimiFrankenWhale"] = "Siamese Sashimi Franken Whale",
			["MutantWhale"] = "Mutant Whale",
			["FinnedMutantWhale"] = "Finned Mutant Whale",
			["SashimiMutantWhale"] = "Sashimi Mutant Whale",
			["SiameseFinnedMutantWhale"] = "Siamese Finned Mutant Whale",
			["SiameseSashimiMutantWhale"] = "Siamese Sashimi Mutant Whale",
			["Leviathan"] = "七阶鱼",
			["FinnedLeviathan"] = "Finned Leviathan",
			["SashimiLeviathan"] = "Sashimi Leviathan",
			["SiameseFinnedLeviathan"] = "Siamese Finned Leviathan",
			["SiameseSashimiLeviathan"] = "Siamese Sashimi Leviathan",
			["FrankenLeviathan"] = "Franken Leviathan",
			["FinnedFrankenLeviathan"] = "Finned Franken Leviathan",
			["SashimiFrankenLeviathan"] = "Sashimi Franken Leviathan",
			["SiameseFinnedFrankenLeviathan"] = "Siamese Finned Franken Leviathan",
			["SiameseSashimiFrankenLeviathan"] = "Siamese Sashimi Franken Leviathan",
			["MutantLeviathan"] = "Mutant Leviathan",
			["FinnedMutantLeviathan"] = "Finned Mutant Leviathan",
			["SashimiMutantLeviathan"] = "Sashimi Mutant Leviathan",
			["SiameseFinnedMutantLeviathan"] = "Siamese Finned Mutant Leviathan",
			["SiameseSashimiMutantLeviathan"] = "Siamese Sashimi Mutant Leviathan",
			["XyWing"] = "XY-Wing",
			["XyzWing"] = "XYZ-Wing",
			["WxyzWing"] = "WXYZ-Wing",
			["VwxyzWing"] = "VWXYZ-Wing",
			["UvwxyzWing"] = "UVWXYZ-Wing",
			["TuvwxyzWing"] = "TUVWXYZ-Wing",
			["StuvwxyzWing"] = "STUVWXYZ-Wing",
			["RstuvwxyzWing"] = "RSTUVWXYZ-Wing",
			["IncompleteWxyzWing"] = "残缺 WXYZ-Wing",
			["IncompleteVwxyzWing"] = "残缺 VWXYZ-Wing",
			["IncompleteUvwxyzWing"] = "残缺 UVWXYZ-Wing",
			["IncompleteTuvwxyzWing"] = "残缺 TUVWXYZ-Wing",
			["IncompleteStuvwxyzWing"] = "残缺 STUVWXYZ-Wing",
			["IncompleteRstuvwxyzWing"] = "残缺 RSTUVWXYZ-Wing",
			["WWing"] = "W-Wing",
			["MWing"] = "M-Wing",
			["LocalWing"] = "Local Wing",
			["SplitWing"] = "Split Wing",
			["HybridWing"] = "Hybrid Wing",
			["GroupedXyWing"] = "区块 XY-Wing",
			["GroupedWWing"] = "区块 W-Wing",
			["GroupedMWing"] = "区块 M-Wing",
			["GroupedLocalWing"] = "区块 Local Wing",
			["GroupedSplitWing"] = "区块 Split Wing",
			["GroupedHybridWing"] = "区块 Hybrid Wing",
			["UrType1"] = "唯一矩形类型 1",
			["UrType2"] = "唯一矩形类型 2",
			["UrType3"] = "唯一矩形类型 3",
			["UrType4"] = "唯一矩形类型 4",
			["UrType5"] = "唯一矩形类型 5",
			["UrType6"] = "唯一矩形类型 6",
			["HiddenUr"] = "隐性唯一矩形",
			["UrPlus2D"] = "唯一矩形 + 2D",
			["UrPlus2B1SL"] = "唯一矩形 + 2B / 1SL",
			["UrPlus2D1SL"] = "唯一矩形 + 2D / 1SL",
			["UrPlus3X"] = "唯一矩形 + 3X",
			["UrPlus3x1SL"] = "唯一矩形 + 3x / 1SL",
			["UrPlus3X1SL"] = "唯一矩形 + 3X / 1SL",
			["UrPlus3X2SL"] = "唯一矩形 + 3X / 2SL",
			["UrPlus3N2SL"] = "唯一矩形 + 3N / 2SL",
			["UrPlus3U2SL"] = "唯一矩形 + 3U / 2SL",
			["UrPlus3E2SL"] = "唯一矩形 + 3E / 2SL",
			["UrPlus4x1SL"] = "唯一矩形 + 4x / 1SL",
			["UrPlus4X1SL"] = "唯一矩形 + 4X / 1SL",
			["UrPlus4x2SL"] = "唯一矩形 + 4x / 2SL",
			["UrPlus4X2SL"] = "唯一矩形 + 4X / 2SL",
			["UrPlus4X3SL"] = "唯一矩形 + 4X / 3SL",
			["UrPlus4C3SL"] = "唯一矩形 + 4C / 3SL",
			["UrXyWing"] = "唯一矩形 + XY-Wing",
			["UrXyzWing"] = "唯一矩形 + XYZ-Wing",
			["UrWxyzWing"] = "唯一矩形 + WXYZ-Wing",
			["UrSdc"] = "唯一矩形 + 融合跨区数组",
			["ArType1"] = "可规避矩形类型 1",
			["ArType2"] = "可规避矩形类型 2",
			["ArType3"] = "可规避矩形类型 3",
			["ArType5"] = "可规避矩形类型 5",
			["HiddenAr"] = "隐性可规避矩形",
			["ArPlus2D"] = "可规避矩形 + 2D",
			["ArPlus3X"] = "可规避矩形 + 3X",
			["ArXyWing"] = "可规避矩形 + XY-Wing",
			["ArXyzWing"] = "可规避矩形 + XYZ-Wing",
			["ArWxyzWing"] = "可规避矩形 + WXYZ-Wing",
			["ArSdc"] = "可规避矩形 + 融合跨区数组",
			["UlType1"] = "唯一环类型 1",
			["UlType2"] = "唯一环类型 2",
			["UlType3"] = "唯一环类型 3",
			["UlType4"] = "唯一环类型 4",
			["XrType1"] = "拓展矩形类型 1",
			["XrType2"] = "拓展矩形类型 2",
			["XrType3"] = "拓展矩形类型 3",
			["XrType4"] = "拓展矩形类型 4",
			["BugType1"] = "全双值格致死解法类型 1",
			["BugType2"] = "全双值格致死解法类型 2",
			["BugType3"] = "全双值格致死解法类型 3",
			["BugType4"] = "全双值格致死解法类型 4",
			["BugMultiple"] = "全双值格致死解法 + n",
			["BugXz"] = "全双值格致死解法-双强链法则",
			["BugXyzWing"] = "全双值格致死解法-XYZ-Wing",
			["BdpType1"] = "探长致命结构类型 1",
			["BdpType2"] = "探长致命结构类型 2",
			["BdpType3"] = "探长致命结构类型 3",
			["BdpType4"] = "探长致命结构类型 4",
			["QdpType1"] = "淑芬致命结构类型 1",
			["QdpType2"] = "淑芬致命结构类型 2",
			["QdpType3"] = "淑芬致命结构类型 3",
			["QdpType4"] = "淑芬致命结构类型 4",
			["LockedQdp"] = "死锁淑芬致命结构",
			["UsType1"] = "唯一矩阵类型 1",
			["UsType2"] = "唯一矩阵类型 2",
			["UsType3"] = "唯一矩阵类型 3",
			["UsType4"] = "唯一矩阵类型 4",
			["Sdc"] = "融合跨区数组",
			["Sdc3d"] = "三维融合跨区数组",
			["CannibalizedSdc"] = "自噬融合跨区数组",
			["Skyscraper"] = "摩天楼",
			["TwoStringKite"] = "双线风筝",
			["TurbotFish"] = "多宝鱼",
			["EmptyRectangle"] = "空矩形",
			["Guardian"] = "守护者",
			["XChain"] = "同数链",
			["FishyCycle"] = "同数环",
			["XyChain"] = "双值格链",
			["XyCycle"] = "双值格环",
			["XyXChain"] = "首尾异数链",
			["PurpleCow"] = "Purple Cow",
			["DiscontinuousNiceLoop"] = "不连续环",
			["ContinuousNiceLoop"] = "连续环",
			["Aic"] = "普通链",
			["GroupedXChain"] = "同数区块链",
			["GroupedFishyCycle"] = "区块同数环",
			["GroupedXyChain"] = "区块双值格链",
			["GroupedXyCycle"] = "区块双值格环",
			["GroupedXyXChain"] = "首尾异数区块链",
			["GroupedPurpleCow"] = "区块 Purple Cow",
			["GroupedDiscontinuousNiceLoop"] = "区块不连续环",
			["GroupedContinuousNiceLoop"] = "区块连续环",
			["GroupedAic"] = "普通区块链",
			["Erip"] = "对交空矩形",
			["Esp"] = "伪数组",
			["SinglyLinkedAlsXz"] = "待定数组-双强链",
			["DoublyLinkedAlsXz"] = "待定数组-双强环",
			["AlsXyWing"] = "待定数组-XY-Wing",
			["AlsWWing"] = "待定数组-W-Wing",
			["DeathBlossom"] = "死亡绽放",
			["Gsp"] = "宇宙法",
			["Je"] = "初级飞鱼导弹",
			["Se"] = "高级飞鱼导弹",
			["ComplexSe"] = "复杂高级飞鱼导弹",
			["SiameseJe"] = "初级孪生飞鱼导弹",
			["SiameseSe"] = "高级孪生飞鱼导弹",
			["SkLoop"] = "多米诺环",
			["Msls"] = "网",
			["Pom"] = "模板叠加",
			["TemplateSet"] = "模板填数",
			["TemplateDelete"] = "模板删数",
			["BowmanBingo"] = "试数",
			["BruteForce"] = "计算机试数",
		};
	}
}
