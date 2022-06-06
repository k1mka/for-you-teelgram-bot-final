using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;



namespace TelegramBotExperiments
{

    class Program
    {
        private static ITelegramBotClient bot = new TelegramBotClient("5539229150:AAEKqeMyt1oAPfdN471CDGGiEEXtaETu8Wc");

        private static string[] stringArray = { "Если тебе вдруг станет грустно, помни всегда, " +
                    "что я рядом и я всегда тебя поддержу.", "Ты самая лучшая!", "Улыбнись!",
                "По скорее возвращайся в Харьков", "Когда я вижу твою улыбку, мне хочеться взлететь до неба!",
                "Твоей фигуре могут позавидовать даже супер модели!", "Твоя улыбка может сделать мир лучше, я постараюсь сделать чтобы ты чаще улыбалась",
                "Твоя улыбка может сделать мир лучше, я постараюсь сделать чтобы ты чаще улыбалась",
                "Ты всегда выглядишь, словно сказочная принцесса.",
                "Сегодня я задумался о том, что пора бы начать тебя ревновать.",
                "Быть такой красивой как ты – страшное преступление!", "У всех девушки как девушки, а у меня БОГИНЯ!",
                  "Ты ангел во плоти, простой человек не может быть так прекрасен.", "Твой образ я видел во сне, с тех пор я не хочу просыпаться.",
                  "С тобою тепло и хорошо в любую погоду.", "Ум и красота несовместимы – но ты исключение из правил.",
                  "Всегда помни, ты особенная!", "Если тебе вдруг станет грустно, помни всегда, что я рядом и я всегда тебя поддержу.",
                  "Ты маленькая ведьма, ты околдовала меня своей красотой.", "Ты оооочень красивая", "Хочу тебя обнимать",
                   "Создатель этого бота тебя очень любит", "Меня просили передать, что ты очень милая!", "Ты котик!",};





        /// handle all events from Telegram here
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            Console.WriteLine(update.Type);

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                await HandleMessage(botClient, update);
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                await HandleCallbackQuesty(botClient, update);
            }
        }

        private static Task HandleCallbackQuesty(ITelegramBotClient botClient, Update update)
        {
            throw new NotImplementedException();
        }

        private static async Task HandleMessage(ITelegramBotClient botClient, Update update)
        {
            var message = update.Message;
            if (message.Text != null)
            {
                if (message.Text.ToLower() == "/start")
                {
                    Random random = new Random();

                    await botClient.SendTextMessageAsync(message.Chat, "Привет, пупсик!", replyMarkup: GetButtons());
                }

                else if (message.Text.ToLower() == "click here 💗")
                {

                    Random random = new Random();

                    await botClient.SendTextMessageAsync(message.Chat, stringArray[new Random().Next(0, stringArray.Length)]);
                }

            }
        }

        private static ReplyKeyboardMarkup GetButtons()
        {
            var rkm = new ReplyKeyboardMarkup(new KeyboardButton[][]
                {

                new KeyboardButton[]
                {
                  new KeyboardButton("click here 💗")}
                }
                );

            rkm.ResizeKeyboard = true;

            return rkm;
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            });
        }


        static void Main(string[] args)
        {

            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}