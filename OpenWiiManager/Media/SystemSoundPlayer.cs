using Microsoft.Win32;
using OpenWiiManager.Language.Attributes;
using OpenWiiManager.Language.Exceptions;
using OpenWiiManager.Language.Extensions;
using System.Media;

namespace OpenWiiManager.Media
{
    /// <summary>
    /// Plays a system sound defined in the sound control panel
    /// </summary>
    public class SystemSoundPlayer
    {
        public const string SoundSchemeCurrent = ".Current";
        public const string SoundSchemeDefault = ".Default";
        public const string SoundSchemeModified = ".Modified";

        public enum PredefinedSound
        {
            /// <summary>
            /// The sound when the Windows User Account Control popup window is shown
            /// </summary>
            [EnumValue(new[] { ".Default", "WindowsUAC" })]
            WindowsUserAccountControl,

            /// <summary>
            /// Generic system notification
            /// </summary>
            [EnumValue(new[] { ".Default", "Notification.Default" })]
            Notification,

            /// <summary>
            /// NFP transfer complete
            /// </summary>
            [EnumValue(new[] { ".Default", "Notification.Proximity" })]
            NFPComplete,

            /// <summary>
            /// Ongoing NFP transfer
            /// </summary>
            [EnumValue(new[] { ".Default", "ProximityConnection" })]
            NFPTransfer,

            /// <summary>
            /// Incoming email
            /// </summary>
            [EnumValue(new[] { ".Default", "Notification.Mail" })]
            NotificationMail,

            /// <summary>
            /// Incoming instant message
            /// </summary>
            [EnumValue(new[] { ".Default", "Notification.IM" })]
            NotificationInstantMessage,

            /// <summary>
            /// Incoming SMS
            /// </summary>
            [EnumValue(new[] { ".Default", "Notification.SMS" })]
            NotificationSMS,

            /// <summary>
            /// Reminder notification
            /// </summary>
            [EnumValue(new[] { ".Default", "Notification.Reminder" })]
            NotificationReminder,

            /// <summary>
            /// Hardware connected
            /// </summary>
            [EnumValue(new[] { ".Default", "DeviceConnect" })]
            DeviceConnect,

            /// <summary>
            /// Hardware disconnected
            /// </summary>
            [EnumValue(new[] { ".Default", "DeviceDisconnect" })]
            DeviceDisconnect,

            /// <summary>
            /// Error connecting hardware
            /// </summary>
            [EnumValue(new[] { ".Default", "DeviceFail" })]
            DeviceFail,

            /// <summary>
            /// Navigation click
            /// </summary>
            [EnumValue(new[] { "Explorer", "Navigating" })]
            Navigating
        }

        /// <summary>
        /// Plays a predefined system sound inside the given scheme
        /// </summary>
        /// <remarks>
        /// This method throws an exception if the given sound or scheme was not found.
        /// This also applies for sounds which are defined but do not have a sound set.
        /// To mitigate this, use <see cref="TryPlay(PredefinedSound, string)"/> instead!
        /// </remarks>
        /// <param name="sound">The sound to play</param>
        /// <param name="scheme">The scheme to use. Defaults to the currently selected sound scheme. Note: This is not the display name of the sound. To find the value for this, look into HKEY_CURRENT_USER\AppEvents\Schemes\Names.</param>
        /// <seealso cref="Play(string, string, string)"/>
        /// <seealso cref="TryPlay(PredefinedSound, string)"/>
        /// <exception cref="SystemSoundException">Thrown if given sound or scheme could not be found</exception>
        public static void Play(PredefinedSound sound, string scheme = SoundSchemeCurrent)
        {
            var value = (string[]?)sound.GetDefinedValue();
            if (value == null || value.Length != 2)
                throw new ArgumentException(null, nameof(PredefinedSound));
            Play(value[0], value[1], scheme);
        }

        /// <summary>
        /// Plays a system sound identified by an app and sound name inside the given scheme
        /// </summary>
        /// <remarks>
        /// This method throws an exception if the given sound or scheme was not found.
        /// This also applies for sounds which are defined but do not have a sound set.
        /// To mitigate this, use <see cref="TryPlay(string, string, string)"/> instead!
        /// </remarks>
        /// <param name="app">The identifier of the app which the sound belongs to</param>
        /// <param name="sound">The name of the sound</param>
        /// <param name="scheme">The scheme to use. Defaults to the currently selected sound scheme. Note: This is not the display name of the sound. To find the value for this, look into HKEY_CURRENT_USER\AppEvents\Schemes\Names.</param>
        /// <seealso cref="Play(PredefinedSound, string)"/>
        /// <seealso cref="TryPlay(string, string, string)"/>
        /// <exception cref="SystemSoundException">Thrown if given sound or scheme could not be found</exception>
        public static void Play(string app, string sound, string scheme = SoundSchemeCurrent)
        {
            var soundIdentifier = $@"{app}\{sound}\{scheme}";
            var key = $@"AppEvents\Schemes\Apps\{soundIdentifier}";
            using var reg = Registry.CurrentUser.OpenSubKey(key);
            if (reg == null)
                throw new SystemSoundException($"Key for sound {soundIdentifier} not found");
            var soundFile = (string?)reg.GetValue("");
            if (string.IsNullOrEmpty(soundFile))
                throw new SystemSoundException($"Value for sound {soundIdentifier}\\(Default) not found");
            if (!File.Exists(soundFile))
                throw new SystemSoundException($"Sound file {soundFile} not found");

            using var player = new SoundPlayer(soundFile);
            player.Play();
        }

        /// <summary>
        /// Plays a predefined system sound inside the given scheme and fails silently if needed
        /// </summary>
        /// <param name="sound">The sound to play</param>
        /// <param name="scheme">The scheme to use. Defaults to the currently selected sound scheme. Note: This is not the display name of the sound. To find the value for this, look into HKEY_CURRENT_USER\AppEvents\Schemes\Names.</param>
        /// <returns>True if sound playback was successful, otherwise false</returns>
        /// <seealso cref="Play(PredefinedSound, string)"/>
        /// <seealso cref="TryPlay(string, string, string)"/>
        public static bool TryPlay(PredefinedSound sound, string scheme = SoundSchemeCurrent)
        {
            try
            {
                Play(sound, scheme);
                return true;
            }
            catch (SystemSoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Plays a system sound identified by an app and sound name inside the given scheme and fails silently if needed
        /// </summary>
        /// <param name="app">The identifier of the app which the sound belongs to</param>
        /// <param name="sound">The name of the sound</param>
        /// <param name="scheme">The scheme to use. Defaults to the currently selected sound scheme. Note: This is not the display name of the sound. To find the value for this, look into HKEY_CURRENT_USER\AppEvents\Schemes\Names.</param>
        /// <returns>True if sound playback was successful, otherwise false</returns>
        /// <seealso cref="Play(string, string, string)"/>
        /// <seealso cref="TryPlay(PredefinedSound, string)"/>
        public static bool TryPlay(string app, string sound, string scheme = SoundSchemeCurrent)
        {
            try
            {
                Play(app, sound, scheme);
                return true;
            }
            catch (SystemSoundException)
            {
                return false;
            }
        }
    }
}
