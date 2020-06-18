namespace Api.Models
{
    /// <summary>
    /// Describes methods to update an instance of T by using properties of an instance of G.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="G"></typeparam>
    public interface IUpdatableFrom<out T, in G>
        where T : class
        where G : class
    {
        /// <summary>
        /// Apply the desired changes to the current object.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        T ApplyChanges(G from);
    }

    /// <summary>
    /// Describes methdos to update and instance of T by using another instance of T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUpdatableFrom<T> : IUpdatableFrom<T, T>
        where T : class
    {
    }
}