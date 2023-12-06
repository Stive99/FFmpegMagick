namespace FFmpegMagick.Classes
{
    internal class ArgsProgram
    {
        public static void Args()
        {
            string[] args = Environment.GetCommandLineArgs();
            foreach (string arg in args)
            {
                if (arg.EndsWith("/update"))
                {
                    Updater.CheckUpdate();
                }
                else if (arg.EndsWith("/test"))
                {
                    Utils.Cmd("ffplay \"F:\\Download\\Без названия (5).mp4\"");
                    Environment.Exit(0);
                }
            }
        }
    }
}
