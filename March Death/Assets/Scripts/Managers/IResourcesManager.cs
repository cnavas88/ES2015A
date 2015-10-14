using WorldResources;
namespace Managers
{
    
    public interface IResourcesManager
    {

        /// <summary>
        /// Adds the amount
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        void AddAmount(Type type, int amount);
        
        /// <summary>
        /// Adds the amount
        /// </summary>
        /// <param name="other"></param>
        void AddAmount(Resource other);

        /// <summary>
        /// Substracts the amount and returns the remaining
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        int SubstractAmount(Type type, int amount);

        /// <summary>
        /// Substracts the amount and returns the remaining
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        int SubstractAmount(Resource other);

        /// <summary>
        /// Checks the current amount and returns true if is higher or equal to the input amount
        /// and false otherwise
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        bool IsEnough(Type type, int amount);

        /// <summary>
        /// Checks the current amount and returns true if is higher or equal to the input amount
        /// and false otherwise
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        bool IsEnough(Resource other);


    }


}