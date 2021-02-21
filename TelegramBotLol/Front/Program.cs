using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBotLol.Back;
using RiotSharp.Misc;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;

namespace TelegramBotLol.Front
{
    class Program
    {
        static ITelegramBotClient botClient;
        static void Main()
        {
            botClient = new TelegramBotClient("Your-bot-api-key");
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}, my username is @{me.Username}"
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.OnCallbackQuery += Bot_OnCallbackQuery;

            botClient.StartReceiving();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            botClient.StopReceiving();
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                if (e.Message.Text[0] != '/')
                {
                    Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}, an user name is {e.Message.Chat.Username}.");

                    List<Region> rlist = new List<Region>();

                    LOLInfo info = new LOLInfo(e.Message.Text, true);

                    string message = $"{e.Message.Text} has been found in this regions:";

                    bool flagFound = false;

                    for (int i = 0; i < 10; i++)
                    {
                        if (info.summonerInfos[i].name != "Unknown")
                        {
                            rlist.Add((Region)i);
                            message += (Region)i + " ";
                            flagFound = true;
                        }
                    }
                    message += "\n Please select region";

                    if (flagFound)
                    {
                        var inlineKeybord = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("RU","ru"),
                            InlineKeyboardButton.WithCallbackData("EUW","euw"),
                            InlineKeyboardButton.WithCallbackData("EUNE","eune"),
                            InlineKeyboardButton.WithCallbackData("BR","br"),

                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("NA","na"),
                            InlineKeyboardButton.WithCallbackData("KR","kr"),
                            InlineKeyboardButton.WithCallbackData("OCE","oce"),
                            InlineKeyboardButton.WithCallbackData("TR","tr")
                        }
                    }
                        );


                        await botClient.SendTextMessageAsync(
                         chatId: e.Message.Chat,
                         text: message,
                         replyMarkup: inlineKeybord
                         );

                    }
                    else
                    {
                        Console.WriteLine("\nSummoner not found\n");
                        message = " Summoner not found";

                        await botClient.SendTextMessageAsync(
                         chatId: e.Message.Chat,
                         text: message
                         );

                    }
                }

            }
        }

        static async void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            string summonerName = message.Text.Substring(0, message.Text.IndexOf(' '));
            string summonerinfo = "";

            switch (e.CallbackQuery.Data)
            {
                case "ru":
                    {

                        LOLInfo info = new LOLInfo(summonerName, false, Region.Ru);

                        summonerinfo =
                            "Nickname:" + info.summonerInfos[0].name +
                          "\nLevel:" + info.summonerInfos[0].level +
                          "\nRegion:" + info.summonerInfos[0].region +
                          "\nSolo Rank:" + info.summonerInfos[0].soloRank +
                          "\nFlex Rank:" + info.summonerInfos[0].flexRank +
                          "\nSolo Ranked Winrate:" + info.summonerInfos[0].soloWinrate + "%" +
                          "\nFlex Ranked Winrate:" + info.summonerInfos[0].flexWinrate + "%";
                        break;
                    }

                case "euw":
                    {
                        LOLInfo info = new LOLInfo(summonerName, false, Region.Euw);

                        summonerinfo =
                            "Nickname:" + info.summonerInfos[0].name +
                          "\nLevel:" + info.summonerInfos[0].level +
                          "\nRegion:" + info.summonerInfos[0].region +
                          "\nSolo Rank:" + info.summonerInfos[0].soloRank +
                          "\nFlex Rank:" + info.summonerInfos[0].flexRank +
                          "\nSolo Ranked Winrate:" + info.summonerInfos[0].soloWinrate + "%" +
                          "\nFlex Ranked Winrate:" + info.summonerInfos[0].flexWinrate + "%";
                        break;
                    }

                case "eune":
                    {
                        LOLInfo info = new LOLInfo(summonerName, false, Region.Eune);

                        summonerinfo =
                            "Nickname:" + info.summonerInfos[0].name +
                          "\nLevel:" + info.summonerInfos[0].level +
                          "\nRegion:" + info.summonerInfos[0].region +
                          "\nSolo Rank:" + info.summonerInfos[0].soloRank +
                          "\nFlex Rank:" + info.summonerInfos[0].flexRank +
                          "\nSolo Ranked Winrate:" + info.summonerInfos[0].soloWinrate + "%" +
                          "\nFlex Ranked Winrate:" + info.summonerInfos[0].flexWinrate + "%";
                        break;
                    }

                case "br":
                    {
                        LOLInfo info = new LOLInfo(summonerName, false, Region.Br);

                        summonerinfo =
                            "Nickname:" + info.summonerInfos[0].name +
                          "\nLevel:" + info.summonerInfos[0].level +
                          "\nRegion:" + info.summonerInfos[0].region +
                          "\nSolo Rank:" + info.summonerInfos[0].soloRank +
                          "\nFlex Rank:" + info.summonerInfos[0].flexRank +
                          "\nSolo Ranked Winrate:" + info.summonerInfos[0].soloWinrate + "%" +
                          "\nFlex Ranked Winrate:" + info.summonerInfos[0].flexWinrate + "%";
                        break;
                    }

                case "na":
                    {
                        LOLInfo info = new LOLInfo(summonerName, false, Region.Na);

                        summonerinfo =
                            "Nickname:" + info.summonerInfos[0].name +
                          "\nLevel:" + info.summonerInfos[0].level +
                          "\nRegion:" + info.summonerInfos[0].region +
                          "\nSolo Rank:" + info.summonerInfos[0].soloRank +
                          "\nFlex Rank:" + info.summonerInfos[0].flexRank +
                          "\nSolo Ranked Winrate:" + info.summonerInfos[0].soloWinrate + "%" +
                          "\nFlex Ranked Winrate:" + info.summonerInfos[0].flexWinrate + "%";
                        break;
                    }

                case "kr":
                    {
                        LOLInfo info = new LOLInfo(summonerName, false, Region.Kr);

                        summonerinfo =
                            "Nickname:" + info.summonerInfos[0].name +
                          "\nLevel:" + info.summonerInfos[0].level +
                          "\nRegion:" + info.summonerInfos[0].region +
                          "\nSolo Rank:" + info.summonerInfos[0].soloRank +
                          "\nFlex Rank:" + info.summonerInfos[0].flexRank +
                          "\nSolo Ranked Winrate:" + info.summonerInfos[0].soloWinrate + "%" +
                          "\nFlex Ranked Winrate:" + info.summonerInfos[0].flexWinrate + "%";
                        break;
                    }

                case "oce":
                    {
                        LOLInfo info = new LOLInfo(summonerName, false, Region.Oce);

                        summonerinfo =
                            "Nickname:" + info.summonerInfos[0].name +
                          "\nLevel:" + info.summonerInfos[0].level +
                          "\nRegion:" + info.summonerInfos[0].region +
                          "\nSolo Rank:" + info.summonerInfos[0].soloRank +
                          "\nFlex Rank:" + info.summonerInfos[0].flexRank +
                          "\nSolo Ranked Winrate:" + info.summonerInfos[0].soloWinrate + "%" +
                          "\nFlex Ranked Winrate:" + info.summonerInfos[0].flexWinrate + "%";
                        break;
                    }

                case "tr":
                    {
                        LOLInfo info = new LOLInfo(summonerName, false, Region.Tr);

                        summonerinfo =
                            "Nickname:" + info.summonerInfos[0].name +
                          "\nLevel:" + info.summonerInfos[0].level +
                          "\nRegion:" + info.summonerInfos[0].region +
                          "\nSolo Rank:" + info.summonerInfos[0].soloRank +
                          "\nFlex Rank:" + info.summonerInfos[0].flexRank +
                          "\nSolo Ranked Winrate:" + info.summonerInfos[0].soloWinrate + "%" +
                          "\nFlex Ranked Winrate:" + info.summonerInfos[0].flexWinrate + "%";
                        break;
                    }
                default:
                    break;
            }

            await botClient.EditMessageTextAsync(e.CallbackQuery.Message.Chat.Id.ToString(), e.CallbackQuery.Message.MessageId, summonerinfo);

            await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
        }
    }
}
