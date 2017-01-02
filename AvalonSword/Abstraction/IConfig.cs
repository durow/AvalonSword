/*
 * Author:durow
 * interface of config file
 * Date:2017.01.02
 */

namespace Ayx.AvalonSword
{
    public interface IConfig
    {
        /// <summary>
        /// get or set the config value by string
        /// </summary>
        /// <param name="path">config path</param>
        /// <returns>config value</returns>
        string this[string path] { get; set; }

        /// <summary>
        /// get config value with string,you can use config["path"] instead
        /// </summary>
        /// <param name="path">config path</param>
        /// <returns>config value</returns>
        string Get(string path);

        /// <summary>
        /// get config value with type T
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="path">config path</param>
        /// <returns>config value</returns>
        T Get<T>(string path);

        /// <summary>
        /// set config value,if config path doesn't exist ,do nothing and return false
        /// you can use config["path"] = value instead.
        /// </summary>
        /// <param name="path">config path</param>
        /// <param name="value">config value</param>
        /// <returns>if set is successful</returns>
        void Set(string path, string value);

        /// <summary>
        /// add new config with value
        /// </summary>
        /// <param name="path">config path</param>
        /// <param name="value">config value</param>
        /// <returns>if add is successful</returns>
        void Add(string path, string value);

        /// <summary>
        /// set config value,if not exists it can create a new path and set it with the value
        /// </summary>
        /// <param name="path">config path</param>
        /// <param name="value">config value</param>
        void AddOrSet(string path, string value);

        /// <summary>
        /// delete config path
        /// </summary>
        /// <param name="path">config path</param>
        void Delete(string path);

        /// <summary>
        /// create an empty config file
        /// </summary>
        /// <param name="filename">config filename</param>
        void CreateEmpty();

        void Reload();
    }
}
