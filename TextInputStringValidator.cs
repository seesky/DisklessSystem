using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace NoDiskSystem
{
    class TextInputStringValidator
    {

        #region  验证输入字符串为数字
        /// <summary>  
        /// 验证输入字符串为数字  
        /// </summary>  
        /// <param name="strln">输入字符</param>  
        /// <returns>返回一个bool类型的值</returns>  
        public static bool IsNumber(string strln)
        {
            return Regex.IsMatch(strln, "^([0]|([1-9]+\\d{0,}?))(.[\\d]+)?$");
        }
        #endregion



        /// <summary>  
        /// 验证输入字符串为英文字母数字组成的字符串  
        /// </summary>  
        /// <param name="strln">输入字符</param>  
        /// <returns>返回一个bool类型的值</returns>  
        public static bool IsEnglishWithNum(string strln) {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9_]+$");
            if (!reg.IsMatch(strln))
            {

                return false;
            }
            else {
                return true;
            }
        }

        /// <summary>  
        /// 判断用户输入是否为日期  
        /// </summary>  
        /// <param name="strln"></param>  
        /// <returns></returns>  
        /// <remarks>  
        /// 可判断格式如下（其中-可替换为/，不影响验证)  
        /// YYYY | YYYY-MM | YYYY-MM-DD | YYYY-MM-DD HH:MM:SS | YYYY-MM-DD HH:MM:SS.FFF  
        /// </remarks>  
        public static bool IsDateTime(string strln)
        {
            if (null == strln)
            {
                return false;
            }
            string regexDate = @"[1-2]{1}[0-9]{3}((-|\/|\.){1}(([0]?[1-9]{1})|(1[0-2]{1}))((-|\/|\.){1}((([0]?[1-9]{1})|([1-2]{1}[0-9]{1})|(3[0-1]{1})))( (([0-1]{1}[0-9]{1})|2[0-3]{1}):([0-5]{1}[0-9]{1}):([0-5]{1}[0-9]{1})(\.[0-9]{3})?)?)?)?$";
            if (Regex.IsMatch(strln, regexDate))
            {
                //以下各月份日期验证，保证验证的完整性  
                int _IndexY = -1;
                int _IndexM = -1;
                int _IndexD = -1;
                if (-1 != (_IndexY = strln.IndexOf("-")))
                {
                    _IndexM = strln.IndexOf("-", _IndexY + 1);
                    _IndexD = strln.IndexOf(":");
                }
                else
                {
                    _IndexY = strln.IndexOf("/");
                    _IndexM = strln.IndexOf("/", _IndexY + 1);
                    _IndexD = strln.IndexOf(":");
                }
                //不包含日期部分，直接返回true  
                if (-1 == _IndexM)
                    return true;
                if (-1 == _IndexD)
                {
                    _IndexD = strln.Length + 3;
                }
                int iYear = Convert.ToInt32(strln.Substring(0, _IndexY));
                int iMonth = Convert.ToInt32(strln.Substring(_IndexY + 1, _IndexM - _IndexY - 1));
                int iDate = Convert.ToInt32(strln.Substring(_IndexM + 1, _IndexD - _IndexM - 4));
                //判断月份日期  
                if ((iMonth < 8 && 1 == iMonth % 2) || (iMonth > 8 && 0 == iMonth % 2))
                {
                    if (iDate < 32)
                        return true;
                }
                else
                {
                    if (iMonth != 2)
                    {
                        if (iDate < 31)
                            return true;
                    }
                    else
                    {
                        //闰年  
                        if ((0 == iYear % 400) || (0 == iYear % 4 && 0 < iYear % 100))
                        {
                            if (iDate < 30)
                                return true;
                        }
                        else
                        {
                            if (iDate < 29)
                                return true;
                        }
                    }
                }
            }
            return false;
        }


        #region 验证手机号
        /// <summary>  
        /// 验证输入字符串为18位的手机号码  
        /// </summary>  
        /// <param name="source"></param>  
        /// <returns></returns>  
        public static bool IsMobile(string strln)
        {
            return Regex.IsMatch(strln, @"^1[0123456789]\d{9}$", RegexOptions.IgnoreCase);
        }

        #endregion


        /// <summary>  
        /// 验证身份证是否有效  
        /// </summary>  
        /// <param name="strln"></param>  
        /// <returns></returns>  
        public static bool IsIDCard(string strln)
        {
            if (strln.Length == 18)
            {
                bool check = IsIDCard18(strln);
                return check;
            }
            else if (strln.Length == 15)
            {
                bool check = IsIDCard15(strln);
                return check;
            }
            else
            {
                return false;
            }
        }

        /// <summary>  
        /// 验证输入字符串为18位的身份证号码  
        /// </summary>  
        /// <param name="strln">输入的字符</param>  
        /// <returns></returns>  
        public static bool IsIDCard18(string strln)
        {
            long n = 0;
            if (long.TryParse(strln.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(strln.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(strln.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = strln.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = strln.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != strln.Substring(17, 1).ToLower())
            {
                return false;//校验码验证  
            }
            return true;//符合GB11643-1999标准  
        }
        /// <summary>  
        /// 验证输入字符串为15位的身份证号码  
        /// </summary>  
        /// <param name="strln">输入的字符</param>  
        /// <returns></returns>  
        public static bool IsIDCard15(string strln)
        {
            long n = 0;
            if (long.TryParse(strln, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(strln.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = strln.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            return true;//符合15位身份证标准  
        }


        /// <summary>  
        /// 验证输入字符串为电话号码  
        /// </summary>  
        /// <param name="P_str_phone">输入字符串</param>  
        /// <returns>返回一个bool类型的值</returns>  
        public static bool IsPhone(string strln)
        {
            return Regex.IsMatch(strln, @"(^(\d{2,4}[-_－—]?)?\d{3,8}([-_－—]?\d{3,8})?([-_－—]?\d{1,7})?$)|(^0?1[35]\d{9}$)");
            //弱一点的验证：  @"\d{3,4}-\d{7,8}"           
        }


        /// <summary>  
        /// 验证是否是有效邮箱地址  
        /// </summary>  
        /// <param name="strln">输入的字符</param>  
        /// <returns></returns>  
        public static bool IsEmail(string strln)
        {
            return Regex.IsMatch(strln, @"^([\w-\.]+)@((
[0−9]1,3\.[0−9]1,3\.[0−9]1,3\.)|(([\w−]+\.)+))([a−zA−Z]2,4|[0−9]1,3)(
?)$");
        }
        /// <summary>  
        /// 验证是否是有效传真号码  
        /// </summary>  
        /// <param name="strln">输入的字符</param>  
        /// <returns></returns>  
        public static bool IsFax(string strln)
        {
            return Regex.IsMatch(strln, @"^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$");
        }
        /// <summary>  
        /// 验证是否只含有汉字  
        /// </summary>  
        /// <param name="strln">输入的字符</param>  
        /// <returns></returns>  
        public static bool IsOnllyChinese(string strln)
        {
            return Regex.IsMatch(strln, @"^[\u4e00-\u9fa5]+$");
        }  

    }
}
