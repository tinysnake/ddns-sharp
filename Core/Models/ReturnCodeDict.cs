using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Core.Models
{
    public class ReturnCodeDict
    {
        static ReturnCodeDict()
        {
            dict = new Dictionary<int, string>();
            dict.Add(-1, "登陆失败");
            dict.Add(-2, "API使用超出限制");
            dict.Add(-3, "不是合法代理");
            dict.Add(-4, "不在代理名下");
            dict.Add(-7, "无权使用此接口");
            dict.Add(-8, "登录失败次数过多，账号被暂时封禁");
            dict.Add(-99, "此功能暂停开放，请稍候重试");
            dict.Add(1, "操作成功");
            dict.Add(2, "只允许POST方法");
            dict.Add(3, "未知错误");
            dict.Add(6, "用户ID错误 (仅用于代理接口)");
            dict.Add(7, "用户不在您名下 (仅用于代理接口)");
        }

        private static Dictionary<int, string> dict;

        public static string GetMessage(int code)
        {
            if (dict.ContainsKey(code))
                return dict[code];
            else
                return "未知代码: " + code;
        }

        public static bool IsCommonCode(int code)
        {
            return dict.ContainsKey(code);
        }
    }
}
