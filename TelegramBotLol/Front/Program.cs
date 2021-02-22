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

            //Console.WriteLine("Press any key to exit");
            //Console.ReadKey();

            //botClient.StopReceiving();
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}, an user name is {e.Message.Chat.Username}.");

                switch (e.Message.Text)
                {
                    case string message1 when message1[0] != '/':
                        {

                            await botClient.SendTextMessageAsync(
                         chatId: e.Message.Chat,
                         text: "Starting search"
                         );

                            List<InlineKeyboardButton> row_1_list = new List<InlineKeyboardButton>();
                            List<InlineKeyboardButton> row_2_list = new List<InlineKeyboardButton>();

                            LOLInfo info = new LOLInfo(e.Message.Text, true);

                            string message = $"{e.Message.Text} has been found in this regions:";

                            bool flagFound = false;
                            int foundRegions = 0;

                            for (int i = 0; i < 10; i++)
                            {
                                if (info.summonerInfos[i].name != "Unknown")
                                {
                                    InlineKeyboardButton button = new InlineKeyboardButton() { CallbackData = $"{(Region)i}".ToLower(), Text = $"{(Region)i}" }; ;
                                    if (foundRegions < 5)
                                    {
                                        row_1_list.Add(button);
                                    }
                                    else
                                    {
                                        row_2_list.Add(button);
                                    }


                                    message += (Region)i + " ";
                                    flagFound = true;
                                    foundRegions++;
                                }
                            }

                            List<InlineKeyboardButton[]> list = new List<InlineKeyboardButton[]>();

                            if (foundRegions < 8)
                            {
                                list.Add(row_1_list.ToArray());
                            }
                            else
                            {
                                list.Add(row_1_list.ToArray());
                                list.Add(row_2_list.ToArray());
                            }


                            message += "\n Please select region";

                            if (flagFound)
                            {
                                var inline = new InlineKeyboardMarkup(list);

                                await botClient.SendTextMessageAsync(
                                 chatId: e.Message.Chat,
                                 text: message,
                                 replyMarkup: inline
                                 );
                            }
                            else
                            {
                                Console.WriteLine("\nSummoner not found\n");
                                message = " Summoner not found";

                                await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id.ToString(),
                                    text: message);
                            }

                            break;
                        }


                    case string message1 when message1 == "/start":
                        {
                            await botClient.SendTextMessageAsync(
                         chatId: e.Message.Chat,
                         text: "Hi!\nWrite summoner's name and I will give info :-)"
                         );
                            break;
                        }

                    default:
                        break;
                }
            }
        }

        static async void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {

            string summonerName = e.CallbackQuery.Message.Text.Substring(0, e.CallbackQuery.Message.Text.IndexOf(' '));
            string message = "empty";

            switch (e.CallbackQuery.Data)
            {
                case "ru":
                    {

                        LOLInfo info = new LOLInfo(summonerName, false, Region.Ru);

                        message =
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

                        message =
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

                        message =
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

                        message =
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

                        message =
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

                        message =
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

                        message =
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

                        message =
                            "Nickname:" + info.summonerInfos[0].name +
                          "\nLevel:" + info.summonerInfos[0].level +
                          "\nRegion:" + info.summonerInfos[0].region +
                          "\nSolo Rank:" + info.summonerInfos[0].soloRank +
                          "\nFlex Rank:" + info.summonerInfos[0].flexRank +
                          "\nSolo Ranked Winrate:" + info.summonerInfos[0].soloWinrate + "%" +
                          "\nFlex Ranked Winrate:" + info.summonerInfos[0].flexWinrate + "%";
                        break;


                    }

                case "lan":
                    {
                        LOLInfo info = new LOLInfo(summonerName, false, Region.Lan);

                        message =
                            "Nickname:" + info.summonerInfos[0].name +
                          "\nLevel:" + info.summonerInfos[0].level +
                          "\nRegion:" + info.summonerInfos[0].region +
                          "\nSolo Rank:" + info.summonerInfos[0].soloRank +
                          "\nFlex Rank:" + info.summonerInfos[0].flexRank +
                          "\nSolo Ranked Winrate:" + info.summonerInfos[0].soloWinrate + "%" +
                          "\nFlex Ranked Winrate:" + info.summonerInfos[0].flexWinrate + "%";

                        break;
                    }

                case "las":
                    {
                        LOLInfo info = new LOLInfo(summonerName, false, Region.Las);

                        message =
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

            await botClient.EditMessageTextAsync(chatId: e.CallbackQuery.Message.Chat.Id.ToString(),
               messageId: e.CallbackQuery.Message.MessageId,
               text: message
                );

            await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
        }
    }
}
