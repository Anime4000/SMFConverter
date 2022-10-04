using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smfc
{
	class Program
	{
		static int Main(string[] args)
		{
			int EXIT_SUCCESS = 0;
			int EXIT_FAILURE = 1;

			bool FClose = false;
			bool FPlay = true;
			string fileout = null;
			int id = 0;

			Console.Write("Company of Heroes Sound Converter - Compiled on {0}\n", Globals.BuildDate);
			Console.Write("Version: {0} ({1} build) by Anime4000\n\n", Globals.Version, Globals.CPUArch);

			if (args.Length == 0)
			{
				Print(3, "No file detected, please drag and drop the .smf file to smfc.exe\n");
				System.Threading.Thread.Sleep(5000);
				return EXIT_FAILURE;
			}

			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].Contains("-h"))
				{
					Console.WriteLine("Usage: smfc.exe <input|-h> [-c] [-p 0|1] [-o output]");
					Console.WriteLine();
					Console.WriteLine("Options:");
					Console.WriteLine("    input       Company of Heroes sound file (*.smf)");
					Console.WriteLine("    -h          Display this help screen.");
					Console.WriteLine("    -c          Exit after converting.");
					Console.WriteLine("    -p bool     Play converted audio. (default 1)");
					Console.WriteLine("    -o name     Save converted file to specific path instead of save ");
					Console.WriteLine("                in working folder. It will ignore file extension.");
					Console.WriteLine();
					Console.WriteLine("Example: smfc.exe \"C:\\coh_combat.smf\" -c -p 0 -o \"D:\\My Stuff\\music\"");
					Console.WriteLine();
					Console.WriteLine("smfc will try detect the .smf file structure, either can be save in wav or mp3");
					return EXIT_SUCCESS;
				}

				if (!args[i].Contains("-"))
					id = i;

				if (args[i].Contains("-c"))
					FClose = true;

				if (args[i].Contains("-p") && args[++i].Contains("0"))
					FPlay = false;

				if (args[i].Contains("-o"))
					fileout = args[++i];
			}

			try
			{
				string output = "";
				string ext = "";

				if (fileout == null)
					output = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(args[id]), System.IO.Path.GetFileNameWithoutExtension(args[id]));
				else
					if (fileout.Contains(":\\"))
						output = fileout;
					else
						output = System.IO.Path.GetFileNameWithoutExtension(fileout);

				byte[] ori = System.IO.File.ReadAllBytes(args[id]);
				byte[] mod = new byte[ori.Length - 12];
				byte[] riff = new byte[] { 82, 73, 70, 70 };
				byte[] valid = new byte[] { 102, 115, 115, 109 };
				byte[] check = new byte[4];

				Print(0, String.Format("Validating {0}\n", System.IO.Path.GetFileName(args[id])));
				Buffer.BlockCopy(ori, 0, check, 0, 4); 
				if (!valid.SequenceEqual(check))
				{
					Print(3, String.Format("File {0} is not valid! Exiting...\n", System.IO.Path.GetFileName(args[id])));
					System.Threading.Thread.Sleep(3000);
					return EXIT_FAILURE;
				}
				else
				{
					Print(1, String.Format("File {0} is valid!\n", System.IO.Path.GetFileName(args[id])));
				}


				Print(0, String.Format("Checking format {0}\n", System.IO.Path.GetFileName(args[id])));
				Buffer.BlockCopy(ori, 12, check, 0, 4);
				if (riff.SequenceEqual(check))
				{
					ext = ".wav";
					Print(0, String.Format("File {0} detect as Wav format\n", System.IO.Path.GetFileName(args[id])));
				}
				else
				{
					ext = ".mp3";
					Print(0, String.Format("File {0} detect as MP3 format\n", System.IO.Path.GetFileName(args[id])));
				}

				Print(0, String.Format("Building {0}\n", output + ext));
				Buffer.BlockCopy(ori, 12, mod, 0, mod.Length);

				Print(0, String.Format("Writing {0}\n", output + ext));
				System.IO.File.WriteAllBytes(output + ext, mod);

				if (FPlay)
				{
					Print(0, String.Format("Opening {0} converted file\n", output + ext));
					System.Diagnostics.Process proc = new System.Diagnostics.Process();
					proc.StartInfo.FileName = output + ext;
					proc.Start();
				}

				if (FClose)
				{
					Print(1, "Done!\n");
					System.Threading.Thread.Sleep(3000);
					return EXIT_SUCCESS;
				}

				Print(1, "Done! Press ANY key to exit\n"); Console.ReadKey();
				return EXIT_SUCCESS;
			}
			catch (Exception ex)
			{
				Print(3, ex.Message);
				Console.ReadKey();
				return EXIT_FAILURE;
			}
		}

		static void Print(int type, string message)
		{
			switch (type)
			{
				case 0:
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write("[");
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.Write("info");
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write("] " + message);
					break;
				case 1:
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write("[");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(" ok ");
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write("] " + message);
					break;
				case 2:
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write("[");
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write("warn");
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write("] " + message);
					break;
				default:
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write("[");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("erro");
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write("] " + message);
					break;
			}
		}
	}
}
