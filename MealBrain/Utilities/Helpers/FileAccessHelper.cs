namespace MealBrain.Utilities.Helpers
{
    /// <summary>
    /// This class provides a helper utility to get the app sandbox for the given system and combine it with the filename.
    /// </summary>
    public class FileAccessHelper
    {
        /// <summary>
        /// Helper method that combines the app sandbox directory on the local
        /// filesystem with the given filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetLocalFilePath(string filename)
        {
            return Path.Combine(FileSystem.AppDataDirectory, filename);
        }
    }
}
