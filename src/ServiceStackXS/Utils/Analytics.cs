using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceStack;

namespace ServiceStackXS
{
	public static class Analytics
	{
		const string serviceStackStatsAddRefUrl = "https://servicestack.net/stats/addref/record?Name={0}";
		const string serviceStackStatsUpdateRefUrl = "https://servicestack.net/stats/updateref/record?Name={0}";

		public static void SubmitAnonymousAddReferenceUsage(string languageName)
		{
			if (languageName == null)
			{
				return;
			}
			Task.Run(() =>
			{
				try
				{
					serviceStackStatsAddRefUrl.Fmt(languageName.ToLower()).GetStringFromUrl();
				}
				catch (Exception)
				{
					//do nothing
				}
			});
		}

		public static void SubmitAnonymousUpdateReferenceUsage(string languageName)
		{
			if (languageName == null)
			{
				return;
			}
			Task.Run(() =>
			{
				try
				{
					serviceStackStatsUpdateRefUrl.Fmt(languageName.ToLower()).GetStringFromUrl();
				}
				catch (Exception)
				{
					//do nothing
				}
			});
		}
	}
}

