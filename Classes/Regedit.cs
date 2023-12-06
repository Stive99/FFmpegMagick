using Microsoft.Win32;

namespace FFmpegMagick.Classes
{
    internal class Regedit
    {
        /// <summary>
        /// Получение уровня UAC
        /// 0 - Отключен - не уведомлять (Never notify).
        /// 1 - Включен - уведомлять, когда приложение пытается внести изменения (Notify me only when apps try to make changes to my computer).
        /// 2 - Включен - уведомлять всегда (Always notify).
        /// </summary>
        /// <returns></returns>
        public static int GetUACLevel()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("EnableLUA");

                        if (value != null)
                        {
                            int enableLUA = Convert.ToInt32(value);
                            return enableLUA;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при попытке прочитать реестр: {ex.Message}");
            }

            return -1; // В случае ошибки или если значение не удалось получить
        }

        public static void Reg()
        {
            string reg = "ffmpegmagick.reg";
            if (File.Exists(reg))
            {
                Utils.Cmd($"regedit /s {reg}");
            }
            else
            {
                MessageBox.Show($"Файл {reg} не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
