using System;
using System.Reflection;
using PUrify;

namespace Purify
{
	public static class Purifier
	{

		private static bool isMono;

		static Purifier()
		{
		    isMono = typeof (Uri).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic) == null;
		}


		public static void Purify(this Uri uri)
		{
		    IPurifier purifier;
		    if (isMono)
                purifier = new PurifierMono();
		    else
                purifier = new PurifierDotNet();

            purifier.Purify(uri);
		}
	}
}

