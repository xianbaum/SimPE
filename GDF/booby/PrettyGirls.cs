using System;
using System.Drawing;
using Microsoft.Win32;

namespace booby;

public class PrettyGirls
{
	internal static Random grl = new Random();

	internal static int picta;

	internal static bool pervymode = false;

	internal static string[] taglines = new string[85]
	{
		"Apple (c) Copyright 1767, Sir Issac Newton.", "Good bye world: the last program you write in C.", "Stop, or I'll say 'stop' again! - British police", "Why not is a perfectly good reason.", "(A)bort (R)etry (P)retend this never happened . . .", "(D)inner not ready: (A)bort, (R)etry, (P)izza", "8 out of 10 people suffer from haemorrhoids. 2 enjoy them.", "A program is used to turn data into error messages.", "A language is just a dialect with a gun.", "All wiyht.  Rho sritched mg kegtops awound?",
		"Always forgive your enemies.  They -HATE- that!", "An animal with two humps is a camel, how about women ...?", "Any job worth doing is worth complaining about.", "Apathy Error: Don't bother striking any key.", "As a computer, I find your faith in technology amusing.", "Back Up My Hard Drive? I Can't Find The Reverse Switch!", "Backup not found: (A)bort (R)etry (P)anic", "Bad command or filename. Go stand in the corner!", "Best way to be useful - stay out of the way.", "Civilization and profits go hand in hand.",
		"Complex problems have simple, easy to understand, wrong answers.", "Crashware is the software that took 3 minutes to write.", "Do what you can, with what you have, where you are.", "DOOR (n): something you throw Windows out of.", "Eagles may soar, but weasels aren't sucked into jet engines", "Everybody lies; but it doesn't matter much since nobody listens.", "Famous Human mistakes: Hiroshima 45, Chernobyl 86, Windows Vista", "Fatal Error Using Mouse:  (B)ury, (R)eplace, (F)eed to Snake?", "File not found. Should I fake it? (Y/N)", "Gee, dad. I wish you'd let mum drive, it's more exciting.",
		"Get lost... If you can't do that, try Google Maps.", "Get Rich by Mail Order -- Send $50 for Details.", "Given any problem containing N equations, there will be N+1 unknowns.", "I Did NOT Feed Dog Food To Your Sister On Our Date!", "Idiot (id-ee-it) n.- One who disagrees with you.", "if (OS=='Vista') { HangSystem(); }", "If at first you don't succeed, put it out for beta test.", "If Idiots could fly this would be an Airport.", "If only women came with pull-down menus and on-line help", "If vegetarians eat vegetables, what do humanitarians eat?",
		"If you choose not to decide, you still have made a choice.", "If you had it all where would you put it?", "If you live long enough, it WILL kill you...", "It's not an optical illusion. It just looks like one.", "MacIntosh: Computer With Training Wheels You Can't Remove", "Make it as simple as possible, but no simpler.", "Math Problems? Call 800-(10x)-[sin(xy)/2.362x]", "Money talks: mine always say goodbye.", "Murphy's Paradox: Doing it the hard way is always easier.", "My cat is radioactive: it has 18 semi-lives.",
		"Never underestimate the power of human stupidity.", "Not ready; error reading user's mind.", "Pentium Sum: 1+1 = 1.94765419", "Please Tell Me if you Don't Get This Message", "Police Station toilet stolen. Detectives have nothing to go on...", "Press every key once to continue, press any key twice to reboot", "Runtime error 100: Replace user and press any key to continue...", "Sorry, a fatal error has occurred. You're dead.", "System halted - Strike any user to continue...", "Support bacteria - it's the only culture some people have!",
		"Support free software: write it yourself !", "Syntax Error in KITCHEN.H: COFFEE not found.", "SYNTAX: Why not? They tax anything else!", "Sysop ('sih sop) n. The guy laughing at your typing.", "System error - Press F13 to continue...", "Teamwork is essential, it allows you to blame someone else.", "The generation of random numbers is too important to be left to chance", "The important thing is not to stop questioning.", "The mother of idiots is always pregnant.", "To understand recursion, we must first understand recursion.",
		"Two most common elements in the universe: Hydrogen and Stupidity.", "Unable to locate Coffee -- Operator Halted!", "Upgrade definition:  Remove OLD bugs,  Put new ones in!", "User (n): technical term used by programmers: see idiot", "Vuja De: The feeling you've never been here before.", "We all live in a yellow subroutine...", "What if Volunteer Firefighters didn't volunteer?", "When all else fails read docs.", "When in danger or in doubt, run in circles, scream and shout!", "When in doubt make it sound convincing.",
		"Windows Err 015: Unable to exit Windows. Try the door.", "Windows Err 02B: Multitasking attempted: system confused.", "Xerox your life! If you lose it, you still have a copy.", "You would if you could but you can't so you won't.", "Your E-Mail has been returned due to insufficient voltage."
	};

	public static bool PervyMode
	{
		get
		{
			return false;
		}
		set
		{
			pervymode = false;
		}
	}

	public static Image WetGirl => null;

	public static Image pnBoobs => null;

	public static Image Knockers => null;

	public static Image BikiniBabe => null;

	public static Image PrittyBabe => null;

	public static Image PrettyJan => null;

	public static Image GoldenGirl => null;

	public static Image NiceGirl => null;

	public static Image BowPeep => null;

	public static Image Mindy => null;

	public static Image BadGirl => null;

	public static Image NiceNurse => null;

	public static Image NiceScamp => null;

	public static Image Fairy => null;

	public static Image ToplessBikini => null;

	public static Image Babydoll => null;

	public static Image LovelyLydia => null;

	public static Image ShearBikini => null;

	public static Image June => null;

	public static Image WildThing => null;

	public static Image Celest => null;

	public static Image Kirsten => null;

	public static Image BikiniBeach => null;

	public static Image Stunner => null;

	public static Image PurpleShades => null;

	public static Image Sorrowful => null;

	public static Image HippyGirl => null;

	public static Image NakedJane => null;

	public static Image TopNotch => null;

	public static Image SheerBlouse => null;

	public static Image BabyOilGirl => null;

	public static Image FuzzyBlonde => null;

	public static Image XmasGirl => null;

	public static Image Majia => null;

	public static Image Zoey => null;

	public static Image Samantha => null;

	public static Image RedDevil => null;

	public static Image PinkBikini => null;

	public static Image Ginger => null;

	public static Image Alice => null;

	public static Image KittyGirl => null;

	public static Image ChainedGirl => null;

	public static Image WindyGirl => null;

	public static Image Felicity => null;

	public static Image Amber => null;

	public static Image BathTime => null;

	public static Image Whitethong => null;

	public static Image Daisy => null;

	public static Image Ella => null;

	public static Image Class2 => null;

	public static Image GodessIsis => null;

	public static Image Bursting => null;

	public static Image OrangeBikini => null;

	public static Image Valentine => null;

	public static Image Ballerina => null;

	public static Image Schoolie => null;

	public static Image Girlies => null;

	public static Image Virgin => null;

	public static Image April => null;

	public static Image Angie => null;

	public static Image Clair => null;

	public static Image Christy => null;

	public static Image Carol => null;

	public static Image Crystal => null;

	public static Image Virginb => null;

	public static Image Leslie => null;

	public static Image Danny => null;

	public static Image Debra => null;

	public static Image PussyBath => null;

	public static Image PoisenIvy => null;

	public static Image Bevely => null;

	public static Image Virginc => null;

	public static Image GiJane => null;

	public static Image Maria => null;

	public static Image Sheryl => null;

	public static Image Vivian => null;

	public static Image Helen => null;

	public static Image RandomGirl => null;

	public static Image RandomChick => null;

	public static Image RandomSheila => null;

	public static Image RandomWoman => null;

	public static Image RandomLady => null;

	public static string TagLine
	{
		get
		{
			picta = grl.Next(0, taglines.Length);
			return taglines[picta];
		}
	}

	public static bool IsTitsInstalled()
	{
		RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Sims2EP10.exe", writable: false);
		if (registryKey == null)
		{
			return false;
		}
		object value = registryKey.GetValue("Installed", 1);
		if (Convert.ToInt32(value.ToString()) == 1)
		{
			return true;
		}
		return false;
	}

	public static bool IsAngelsInstalled()
	{
		RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Sims2AnN.exe", writable: false);
		if (registryKey == null)
		{
			return false;
		}
		object value = registryKey.GetValue("Installed", 1);
		if (Convert.ToInt32(value.ToString()) == 1)
		{
			return true;
		}
		return false;
	}
}
