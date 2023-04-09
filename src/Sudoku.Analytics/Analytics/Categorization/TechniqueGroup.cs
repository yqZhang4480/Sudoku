namespace Sudoku.Analytics.Categorization;

/// <summary>
/// Represents a technique group.
/// </summary>
public enum TechniqueGroup : byte
{
	/// <summary>
	/// Indicates the technique doesn't belong to any group.
	/// </summary>
	None,

	/// <summary>
	/// Indicates the singles technique.
	/// </summary>
	Single,

	/// <summary>
	/// Indicates the locked candidates (LC) technique.
	/// </summary>
	LockedCandidates,

	/// <summary>
	/// Indicates the subset technique.
	/// </summary>
	Subset,

	/// <summary>
	/// Indicates the normal fish technique.
	/// </summary>
	NormalFish,

	/// <summary>
	/// Indicates the complex fish technique.
	/// </summary>
	ComplexFish,

	/// <summary>
	/// Indicates the wing technique.
	/// </summary>
	Wing,

	/// <summary>
	/// Indicates the empty rectangle technique.
	/// </summary>
	EmptyRectangle,

	/// <summary>
	/// Indicates the single digit pattern (SDP) technique.
	/// </summary>
	SingleDigitPattern,

	/// <summary>
	/// Indicates the empty rectangle intersection pair (ERIP) technique.
	/// </summary>
	EmptyRectangleIntersectionPair,

	/// <summary>
	/// Indicates the almost locked candidates (ALC) technique.
	/// </summary>
	AlmostLockedCandidates,

	/// <summary>
	/// Indicates the firework technique.
	/// </summary>
	Firework,

	/// <summary>
	/// Indicates the alternating inference chain (AIC) technique.
	/// </summary>
	AlternatingInferenceChain,

	/// <summary>
	/// Indicates the forcing chains technique.
	/// </summary>
	ForcingChains,

	/// <summary>
	/// Indicates the unique rectangle (UR) technique.
	/// </summary>
	UniqueRectangle,

	/// <summary>
	/// Indicates the unique rectangle plus (UR+) technique.
	/// </summary>
	UniqueRectanglePlus,

	/// <summary>
	/// Indicates the avoidable rectangle (AR) technique.
	/// </summary>
	AvoidableRectangle,

	/// <summary>
	/// Indicates the unique loop (UL) technique.
	/// </summary>
	UniqueLoop,

	/// <summary>
	/// Indicates the extended rectangle (XR) technique.
	/// </summary>
	ExtendedRectangle,

	/// <summary>
	/// Indicates the bi-value universal grave (BUG) technique.
	/// </summary>
	BivalueUniversalGrave,

	/// <summary>
	/// Indicates the Borescoper's deadly pattern technique.
	/// </summary>
	BorescoperDeadlyPattern,

	/// <summary>
	/// Indicates the Qiu's deadly pattern technique.
	/// </summary>
	QiuDeadlyPattern,

	/// <summary>
	/// Indicates the RW's deadly pattern technique.
	/// </summary>
	RwDeadlyPattern,

	/// <summary>
	/// Indicates the unique matrix technique.
	/// </summary>
	UniqueMatrix,

	/// <summary>
	/// Indicates the reverse bi-value universal grave (Reverse BUG) technique.
	/// </summary>
	ReverseBivalueUniversalGrave,

	/// <summary>
	/// Indicates the uniqueness clue cover.
	/// </summary>
	UniquenessClueCover,

	/// <summary>
	/// Indicates the bi-value oddagon technique.
	/// </summary>
	BivalueOddagon,

	/// <summary>
	/// Indicates the chromatic pattern technique.
	/// </summary>
	ChromaticPattern,

	/// <summary>
	/// Indicates the sue de coq (SdC) technique.
	/// </summary>
	SueDeCoq,

	/// <summary>
	/// Indicates the broken wing technique.
	/// </summary>
	BrokenWing,

	/// <summary>
	/// Indicates the ALS chaining-like (ALS-XZ, ALS-XY-Wing, ALS-W-Wing) technique.
	/// </summary>
	AlmostLockedSetsChainingLike,

	/// <summary>
	/// Indicates the extended subset principle technique.
	/// </summary>
	ExtendedSubsetPrinciple,

	/// <summary>
	/// Indicates the death blossom technique.
	/// </summary>
	DeathBlossom,

	/// <summary>
	/// Indicates the SK-Loop technique.
	/// </summary>
	DominoLoop,

	/// <summary>
	/// Indicates the multi-sector locked sets (MSLS) technique.
	/// </summary>
	MultisectorLockedSets,

	/// <summary>
	/// Indicates the exocet technique.
	/// </summary>
	Exocet,

	/// <summary>
	/// Indicates the symmetry technique.
	/// </summary>
	Symmetry,

	/// <summary>
	/// Indicates the technique checked and searched relies on the rank theory.
	/// </summary>
	RankTheory,

	/// <summary>
	/// Indicates the bowman bingo technique.
	/// </summary>
	BowmanBingo,

	/// <summary>
	/// Indicates the pattern overlay method (POM) technique.
	/// </summary>
	PatternOverlay,

	/// <summary>
	/// Indicates the templating technique.
	/// </summary>
	Templating,

	/// <summary>
	/// Indicates the brute force (BF) technique.
	/// </summary>
	BruteForce
}
