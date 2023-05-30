using System.Net.Mime;
using System.Reflection.PortableExecutable;
using Line.Messaging;
using Line.Messaging.Webhooks;

namespace p10941_linebot;

public class LineBotApp : WebhookApplication
{
    private readonly LineMessagingClient _messagingClient;

    private static Dictionary<string, string> _pool=new Dictionary<string, string>();
    public LineBotApp(LineMessagingClient lineMessagingClient)
    {
        _messagingClient = lineMessagingClient;
    }

    protected override async Task OnMessageAsync(MessageEvent ev)
    {
        var result = null as List<ISendMessage>;

        switch (ev.Message)
        {
            //文字訊息
            case TextEventMessage textMessage:
            {
                //頻道Id
                var channelId = ev.Source.Id;
                //使用者Id
                var userId = ev.Source.UserId;
                    
                var text =((TextEventMessage)ev.Message).Text;

                if (PoolHasMsg(text))
                {
                    string response = GetResponse(text);
                    result = new List<ISendMessage>
                    {
                        new TextMessage(response)
                    };
                }
                else
                {
                    if (CheckFormat(text))
                    {
                        TeachDog(text);
                    }
                }
                
                //回傳 hellow
                result = new List<ISendMessage>
                {
                    new TextMessage("我愛數媒系")
                };
            }
                break;
        }

        if (result != null)
            await _messagingClient.ReplyMessageAsync(ev.ReplyToken, result);
    }

    private bool PoolHasMsg(string inputMsg)
    {
        return _pool.ContainsKey(inputMsg);
    }

    private string GetResponse(string inputMsg)
    {
        return _pool[inputMsg];
        return "";
    }
    private bool CheckFormat(string inputMsg)
    {
        bool result = false;
        try
        {
            string[] subs = inputMsg.Split(';');
            if (subs.Length == 3)
            {
                if (subs[0] == "貓")
                {
                    return true;
                }
            }
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp);
            throw;
        }
        
        
        return result;
    }

    private void TeachDog(string inputMsg)
    {
        try
        {
            string[] subs = inputMsg.Split(';');
            if (subs.Length == 3)
            {
                if (subs[0] == "貓")
                {
                    _pool.Add(subs[1],subs[2]);
                }
            }
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp);
            throw;
        }
        
        
        
    }
}