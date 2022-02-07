public class Singleton<T> where T : class, new()
{
    #region Fields

    private static T instance;

    #endregion



    #region Properties

    public static T Instance => instance ?? (instance = new T());

    #endregion



    #region Ctor

    protected Singleton()
    {
    }

    #endregion
}