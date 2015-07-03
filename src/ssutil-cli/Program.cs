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
                Console.WriteLine();
                Console.WriteLine("Create new reference. Eg ssutil \"<URL>\" -file <PATH> -lang <lang>");
                Console.WriteLine("Update existing reference, eg ssutil \"<PATH>\"");
                Console.WriteLine();
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

            try
            {
                ProcessModeHandler.Process(UtilCliOptions.GetInstance().Options);
                if (currentMode == CmdMode.AddReference ||
                    currentMode == CmdMode.AddRefWithLang ||
                    currentMode == CmdMode.AddReferenceWithPath ||
                    currentMode == CmdMode.AddReferenceWithPathAndLang)
                {
                    Console.WriteLine("Successfully added new file.");
                }

                if (currentMode == CmdMode.UpdateReference)
                {
                    Console.WriteLine("Successfully updated " + UtilCliOptions.GetInstance().Options[UtilCliOptions.DEFAULT]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to complete operation. " + e.Message);
                optionSet.WriteOptionDescriptions(Console.Out);
            }
            
        }
    }
}
