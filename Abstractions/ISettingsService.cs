﻿namespace MAUIBrowser.Abstractions
{
    /// <summary>
    /// Implementation of work with application settings
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Getting settings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        Task<T> GetSettings<T>(string key, T defaultValue);

        /// <summary>
        /// Saving settings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SaveSettings<T>(string key, T value);
    }
}
