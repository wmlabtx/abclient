// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Manager.cs" company="wmlab.org">
//   Murad Ismayilov
// </copyright>
// <summary>
//   Defines the Manager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ABClient.Profile
{
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Класс, отвечающий за работу с профайлами.
    /// </summary>
    internal static class Manager
    {
        /// <summary>
        /// Конвертация старых профайлов в новые.
        /// </summary>
        internal static void ConversionOldProfiles()
        {
            // Получаем список старых профайлов в текущей папке клиента
            var directoryInfo = new DirectoryInfo(Application.StartupPath);
            var fileList = directoryInfo.GetFiles(AppConsts.OldProfilesMask, SearchOption.TopDirectoryOnly);
            if (fileList.Length == 0)
            {
                return;
            }

            if (MessageBox.Show(
                AppConsts.AskConvertOldProfiles,
                Helpers.Versions.ProductNameShortVersion,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            foreach (var oldProfileFileInfo in fileList)
            {
                var oldProfile = new Config(oldProfileFileInfo.FullName);
            }
        }
    }
}
