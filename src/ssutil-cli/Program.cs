using System;

namespace ssutil_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionSet = UtilCliOptions.GetInstance().DefaultOptionSet;
            if (args == null || args.Length == 0)
            {
                optionSet.WriteOptionDescriptions(Console.Out);
                return;
            }
            optionSet.Parse(args);
            CmdMode currentMode = ProcessModeHandler.GetMode(UtilCliOptions.GetInstance().Options);
            if (currentMode == CmdMode.Invalid)
            {
                Console.WriteLine("Arguments provided are invalid.");
                optionSet.WriteOptionDescriptions(Console.Out);
                return;
            }

            Console.ReadLine();
        }
    }
}
