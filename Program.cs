using System.Diagnostics;
using System.Reflection;

namespace FFmpegMagick
{
	internal static class Program
	{
		/// <summary>
		///	Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Для настройки конфигурации приложения, например, установки высоких параметров DPI или шрифта по умолчанию,
			// см. https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			Application.ThreadException += new ThreadExceptionEventHandler(Exception);

			Process pr = RI();
			if (pr != null)
			{
				MessageBox.Show("Приложение уже запущено!", "Приложение запущено");
			}
			else
			{
				Application.Run(new Form1());
			}
		}

		static void Exception(object sender, ThreadExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.ToString(), "Непредвиденная хуйня");
		}

		private static Process RI()
		{
			Process current = Process.GetCurrentProcess();
			Process[] pr = Process.GetProcessesByName(current.ProcessName);
			foreach (Process i in pr)
			{
				if (i.Id != current.Id)
				{
					if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
					{
						return i;
					}
				}
			}
			return null;
		}
	}
}