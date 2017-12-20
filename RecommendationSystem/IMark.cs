namespace RecommendationSystem
{
    public interface IMark<TUser, TThing>
    {

        /// <summary>
        /// User who rated
        /// </summary>
        /// <returns></returns>
        TUser User { get; }

        /// <summary>
        /// Thing of evaluate
        /// </summary>
        /// <returns></returns>
        TThing Thing { get; }

        /// <summary>
        /// Getting numeric representation of mark
        /// </summary>
        /// <example>
        /// If we have a like-system, 
        /// it possible to return 1 if thing has like and 0 if not
        /// </example>
        /// <returns></returns>
        int GetNumber();

        /// <summary>
        /// Getting numeric representation of difference between two marks
        /// </summary>
        /// <remarks>
        /// Difference between numbers is an absolute value, so uint is used
        /// </remarks>
        /// <example>
        /// If we have a ten-point scale, and the estimates are 10 and 8,
        /// we need to return the difference, that is 2
        /// </example>
        /// <param name="another"></param>
        /// <returns></returns>
        ushort GetDifference(IMark<TUser, TThing> another);

    }
}