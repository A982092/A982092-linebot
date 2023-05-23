using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Line.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace p10941_linebot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        private readonly LineBotConfig _lineBotConfig;
        
        public LineBotController(IServiceProvider serviceProvider)
        {
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _httpContext = _httpContextAccessor.HttpContext;
            _lineBotConfig = new LineBotConfig();
            _lineBotConfig.channelSecret = "34df25bef93677eaf3024d4bff007328";
            _lineBotConfig.accessToken = "+s3F8nixsDlaE58WTz04yzwMDZVmK76aWKcNSBiOUVoPIZYUS3AVaoqDnw9i3G3ZvZ51Wxp2BYHbo4O2940u9F3KBpOFV3SuFkZIYzZX9zgGWqD/ZoUo3xx/Wxfyqncs6vX8x2dVpLH/Lz266DHi5AdB04t89/1O/w1cDnyilFU=";
        }
        
        //完整的路由網址就是 https://xxx/api/linebot/run
        [HttpPost("run")]
        public async Task<IActionResult> Post()
        {
            try
            {
                var events = await _httpContext.Request.GetWebhookEventsAsync(_lineBotConfig.channelSecret);
                var lineMessagingClient = new LineMessagingClient(_lineBotConfig.accessToken);
                var lineBotApp = new LineBotApp(lineMessagingClient);
                await lineBotApp.RunAsync(events);
            }
            catch (Exception ex)
            {
                //需要 Log 可自行加入
                //_logger.LogError(JsonConvert.SerializeObject(ex));
            }
            return Ok();
        }
    }
}
