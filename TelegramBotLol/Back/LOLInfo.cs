using System;
using System.Collections.Generic;
using RiotSharp;
using RiotSharp.Endpoints.LeagueEndpoint;
using RiotSharp.Misc;


namespace TelegramBotLol.Back
{


    class LOLInfo
    {
        const string apiKey = "RGAPI-397c11da-3b1f-4126-9965-033e4cf7c418";
        public struct SummonerInfo
        {
            public SummonerInfo(SummonerInfo si)
            {
                name = si.name;
                region = si.region;
                level = si.level;
                soloWinrate = si.soloWinrate;
                flexWinrate = si.flexWinrate;
                soloRank = si.soloRank;
                flexRank = si.flexRank;
            }
            public SummonerInfo(string name)
            {
                this.name = name;
                this.region = name;
                this.level = name;
                this.soloWinrate = name;
                this.flexWinrate = name;
                this.soloRank = name;
                this.flexRank = name;
            }
            public string name { get; set; }
            public string region { get; set; }
            public string level { get; set; }
            public string soloWinrate { get; set; }
            public string flexWinrate { get; set; }
            public string soloRank { get; set; }
            public string flexRank { get; set; }
        }

        public List<SummonerInfo> summonerInfos;

        public LOLInfo(string SummonerName, bool quickSearch, Region region = 0)
        {
            summonerInfos = new List<SummonerInfo>();
            if (quickSearch)
            {
                for (int i = 0; i < 19; i++)
                {
                    summonerInfos.Add(GetName(SummonerName, (Region)i));
                }
            }
            else
            {
                summonerInfos.Add(GetInfo(SummonerName, region));
            }
        }

        public SummonerInfo GetInfo(string SummonerName, Region region)
        {
            SummonerInfo summonerInfo = new SummonerInfo("Unknown");

            string summonername;

            var api = RiotApi.GetDevelopmentInstance(apiKey);

            summonername = SummonerName;

            try
            {
                //General info about account
                var summoner = api.Summoner.GetSummonerByNameAsync(region, summonername).Result;
                summonerInfo.name = summoner.Name;
                summonerInfo.region = summoner.Region.ToString();
                summonerInfo.level = summoner.Level.ToString();
                var accountid = summoner.AccountId;

                Console.WriteLine("Name:" + summonerInfo.name);
                Console.WriteLine("Region:" + summonerInfo.region);
                Console.WriteLine("Level:" + summonerInfo.level);

                try
                {
                    List<LeagueEntry> rank = api.League.GetLeagueEntriesBySummonerAsync(summoner.Region, summoner.Id).Result;

                    if (rank[0].QueueType == "RANKED_SOLO_5x5")
                    {
                        try
                        {
                            //Getting players ranks
                            Console.WriteLine("Rank solo:" + rank[0].Tier + " " + rank[0].Rank);
                            summonerInfo.soloRank = rank[0].Tier + " " + rank[0].Rank;
                            Console.WriteLine("Rank flex:" + rank[1].Tier + " " + rank[1].Rank);
                            summonerInfo.flexRank = rank[1].Tier + " " + rank[1].Rank;
                        }
                        catch
                        {
                            Console.WriteLine("\nProblems with solorank\n");
                        }

                        try
                        {
                            //Getting players winrates
                            float sWinrate = (float)rank[0].Wins / ((float)rank[0].Wins + (float)rank[0].Losses);
                            sWinrate *= 100;
                            Console.WriteLine("Solo ranked winrate:" + Math.Round(sWinrate, 1) + "%");
                            summonerInfo.soloWinrate = Math.Round(sWinrate, 1).ToString();

                            float fWinrate = (float)rank[1].Wins / ((float)rank[1].Wins + (float)rank[1].Losses);
                            fWinrate *= 100;
                            Console.WriteLine("Flex ranked winrate:" + Math.Round(fWinrate, 1) + "%");
                            summonerInfo.flexWinrate = Math.Round(fWinrate, 1).ToString();
                        }
                        catch
                        {
                            Console.WriteLine("\nProblems with winrates\n");
                        }
                    }
                    else
                    {
                        try
                        {
                            //Getting players ranks
                            Console.WriteLine("Rank flex:" + rank[0].Tier + " " + rank[0].Rank);
                            summonerInfo.flexRank = rank[0].Tier + " " + rank[0].Rank;
                            Console.WriteLine("Rank solo:" + rank[1].Tier + " " + rank[1].Rank);
                            summonerInfo.soloRank = rank[1].Tier + " " + rank[1].Rank;
                        }
                        catch
                        {
                            Console.WriteLine("\nProblems with solorank\n");
                        }

                        try
                        {
                            //Getting players winrates
                            float fWinrate = (float)rank[0].Wins / ((float)rank[0].Wins + (float)rank[0].Losses);
                            fWinrate *= 100;
                            Console.WriteLine("Flex ranked winrate:" + Math.Round(fWinrate, 1) + "%");
                            summonerInfo.flexWinrate = Math.Round(fWinrate, 1).ToString();

                            float sWinrate = (float)rank[1].Wins / ((float)rank[1].Wins + (float)rank[1].Losses);
                            sWinrate *= 100;
                            Console.WriteLine("Solo ranked winrate:" + Math.Round(sWinrate, 1) + "%");
                            summonerInfo.soloWinrate = Math.Round(sWinrate, 1).ToString();
                        }
                        catch
                        {
                            Console.WriteLine("\nProblems with winrates\n");
                        }
                    }
                }
                catch
                {

                    Console.WriteLine("\nProblems with rank\n");
                    return summonerInfo;
                }

                return summonerInfo;
            }
            catch
            {
                Console.WriteLine("\nPlayer not found (or smth else)\n");
                return summonerInfo;
            }
        }

        public SummonerInfo GetName(string SummonerName, Region region)
        {
            SummonerInfo summonerInfo = new SummonerInfo("Unknown");


            string summonername;

            var api = RiotApi.GetDevelopmentInstance(apiKey);

            summonername = SummonerName;

            try
            {
                var summoner = api.Summoner.GetSummonerByNameAsync(region, summonername).Result;
                summonerInfo.name = summoner.Name;
                summonerInfo.region = summoner.Region.ToString();
                Console.WriteLine($"{summonerInfo.name} has been found in {summonerInfo.region}");
            }
            catch
            {
                return summonerInfo;
            }
            return summonerInfo;
        }
    }
}
